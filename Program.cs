class deldir
{
    static ConsoleColor color_directory = ConsoleColor.Magenta;
    static ConsoleColor color_folder = ConsoleColor.Green;
    static ConsoleColor color_file = ConsoleColor.Yellow;
    static ConsoleColor color_warning = ConsoleColor.Red;
    static List<string> total_folders = new List<string>();
    static List<string> total_files = new List<string>();
    static bool Quiet = false;
    static bool Information = false;

    static void Main (string[] args)
    {
        //All this code is to get data from my Argument Parser.
        //more info abount this code at:
        //https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V2.0
        bool Help = parseArgsBool(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        Quiet = parseArgsBool(args, new string[]{ "-q" , "--quiet"}, "don't print folder/file tree");
        Information = parseArgsBool(args, new string[]{ "-i" , "--information"}, "display extra information");
        bool BaseDir = parseArgsBool(args, new string[]{ "-b" , "--basedirectory"}, "delete the base directory");
        bool FolderOnly = parseArgsBool(args, new string[]{ "--fol" , "--foldersonly"}, "delete only folders");
        bool FileOnly = parseArgsBool(args, new string[]{ "--fil" , "--filesonly"}, "delete only files");
        List<string> Argument = parseArgs(args, "path", "change the path");
        help_message("usage","deldir");
        help_message("description","Directory eraser");
        help_message("information","created by https://github.com/000Daniel");
        if (Help)
        {
            help_message("call",string.Empty);
        }

        //this software finds all files and folders in this order:
        //1. find all folders in the base directory <add to list>
        //2. go inside each folder and look for all files <add to list>
        //3. now look for more folders and repeat
        //4. find all files in base directory <add to list>
        var CurrentDir = "";
        try
        {
            CurrentDir = Directory.GetCurrentDirectory();
        }
        catch
        {
            Console.ForegroundColor = color_warning;
            Console.WriteLine("Error! program executed at an invalid directory."); 
            Console.ResetColor();
            Environment.Exit(0);
        }
        bool onlyOneFile = false;
        if (Argument.Count() > 0) 
        {
            foreach (string arg in Argument)
            {
            //default/base path = the path that this software is executed at.
            //this code allows the user to choose a path different than the
            //base path, thanks to my Argument Parser(more on this later). 
                if (arg.IndexOf("/") == 0)
                {
                    if (Directory.Exists(arg))
                    {
                        CurrentDir = arg;
                    }
                    else
                    {
                        if (File.Exists(arg))
                        {
                            total_files.Add(CurrentDir + "/" + arg);
                            onlyOneFile = true;
                        }
                        else
                        {
                            Console.ForegroundColor = color_warning;
                            Console.WriteLine("Error! directory or file missing."); 
                            Console.ResetColor();
                            Environment.Exit(0);
                        }
                    }
                }
                else if (Directory.Exists(CurrentDir + "/" + arg))
                {
                    CurrentDir = CurrentDir + "/" + arg;
                }
                else
                {
                    if (File.Exists(CurrentDir + "/" + arg))
                    {
                        total_files.Add(CurrentDir + "/" + arg);
                        onlyOneFile = true;
                    }
                    else
                    {
                        Console.ForegroundColor = color_warning;
                        Console.WriteLine("Error! directory or file missing."); 
                        Console.ResetColor();
                        Environment.Exit(0);
                    }
                }
            }
        }
        //'onlyOneFile' means that the user chose to delete a single file, the software would
        //skip all the folder and files checks until the deletion.
        //with 'onlyOneFile' the software would delete only that one file and nothing else.
        if (!onlyOneFile)
        {
        //these lists contain all files and folders with full paths.
        //the next code, does what was mentioned earlier with finding all the files and folders.
        //Note: it looks in the base directory OR the directory that the user chose.
            string[] allFiles = Directory.GetFiles(CurrentDir);
            string[] allFolders = Directory.GetDirectories(CurrentDir);

            Console.ForegroundColor = color_directory;
            if (Quiet)
            {
                Console.WriteLine(CurrentDir);
            }
            else
            {
                Console.WriteLine(CurrentDir + "\n");
            }

            foreach (string folder in allFolders)
            {
                Console.ForegroundColor = color_folder;
                length_of_dir = 0;
                string folder_name = folder.Substring(folder.LastIndexOf("/") + 1);
                if (!Quiet)
                {
                    if (Information)
                    {
                        Console.WriteLine(folder);
                    }
                    else
                    {
                        Console.WriteLine(folder_name);
                    }
                }
                total_folders.Add(folder);

                allFiles = Directory.GetFiles(CurrentDir + "/" + folder_name);
                Console.ForegroundColor = color_file;
                foreach (string file in allFiles)
                {
                    if (!Quiet)
                    {
                        if (Information)
                        {
                            Console.WriteLine("  ╚═══" + file);
                        }
                        else
                        {
                            Console.WriteLine("  ╚═══" + file.Substring(file.LastIndexOf("/") + 1));
                        }
                    }
                    total_files.Add(file);
                }

                researchFolder(CurrentDir,folder_name);
            }

            allFiles = Directory.GetFiles(CurrentDir);
            Console.ForegroundColor = color_file;
            foreach (string file in allFiles)
            {
            if (!Quiet)
            {
                    if (Information)
                    {
                        Console.WriteLine(file);
                    }
                    else
                    {
                        Console.WriteLine(file.Substring(file.LastIndexOf("/") + 1));
                    }
                }
                total_files.Add(file);
            }
        }
        //this message asks whether the user wants to delete all files and folders, and lists
        //how many will be deleted.
        //if the user types 'yes' the software will delete the files/folders
        if (FolderOnly && !FileOnly)
        {
            total_files = new List<string>();
        }
        if (FileOnly && !FolderOnly)
        {
            total_folders = new List<string>();
        }
        Console.ForegroundColor = color_warning;
        Console.WriteLine(string.Format("\nYou are about to delete {0} Files and {1} Folders.",total_files.Count(),total_folders.Count()));
        if (BaseDir)
        {
            Console.WriteLine("And about to delete this directory: " + CurrentDir);
        }
        Console.ResetColor();
        Console.Write("Are you sure? [yes/N]");
        string userInput = Console.ReadLine().ToString();
        
        if (!string.IsNullOrEmpty(userInput) && userInput.ToLower() == "yes")
        {
            foreach (string total_file in total_files)
            {
                if (!Quiet)
                    Console.WriteLine("Deleting file: " + total_file);
                try
                {
                    File.Delete(total_file);
                }
                catch
                {
                    Console.ForegroundColor = color_warning;
                    Console.WriteLine(string.Format("Error! couldn't delete '{0}'.",total_file)); 
                    Console.ResetColor();
                }
            }

            for (int i=total_folders.Count() -1; i >= 0; i--)
            {
                if (!Quiet)
                    Console.WriteLine("Deleting folder: " + total_folders[i]);
                try
                {
                    Directory.Delete(total_folders[i]);
                }
                catch
                {
                    Console.ForegroundColor = color_warning;
                    Console.WriteLine(string.Format("Error! couldn't delete '{0}'.",total_folders[i])); 
                    Console.ResetColor();
                }
            }
            if (BaseDir)
        {
            if (!Quiet)
                Console.WriteLine("Deleting base directory: " + CurrentDir);
            Directory.Delete(CurrentDir);
        }
        }
        Console.ResetColor();
        Console.WriteLine("");
        Environment.Exit(0);
    }


        //this function looks for folders and files INSIDE a folder.
        //this function will also loop multiple times until it finds all files and folders.
    static int length_of_dir = 0;
    static void researchFolder(string CurrentDir,string folder_name) {
        length_of_dir += 3;
        string[] allFolders = Directory.GetDirectories(CurrentDir + "/" + folder_name);
        foreach (string folder in allFolders)
        {
            total_folders.Add(folder);

            string wireString = "";
            for (int i = 1; i < length_of_dir; i++) {
                wireString = wireString + " " ;
            }

            Console.ForegroundColor = color_folder;
            string next_folder_name = folder.Substring(folder.LastIndexOf("/") + 1);
            if (!Quiet)
            {
                if (Information)
                {
                    Console.WriteLine(wireString + "╚═══" + folder);
                }
                else
                {
                    Console.WriteLine(wireString + "╚═══" + next_folder_name);
                }
            }
            
            Console.ForegroundColor = color_file;
            string[] allFiles = Directory.GetFiles(CurrentDir + "/" + folder_name);

            allFiles = new string[]{};
            allFiles = Directory.GetFiles(CurrentDir + "/" + folder_name + "/" + next_folder_name);
            foreach (string file in allFiles)
            {
                total_files.Add(file);
                if (!Quiet)
                {
                    if (Information)
                    {
                        Console.WriteLine(wireString + "  ╚═══" + file);
                    }
                    else
                    {
                        Console.WriteLine(wireString + "  ╚═══" + file.Substring(file.LastIndexOf("/") + 1));
                    }
                }
            }
            
            researchFolder(CurrentDir + "/" + folder_name,next_folder_name);
        }
    }



        //every code after this point is unrelated to the deleting funtions of this software.
        //the next code is my Argument Parser. it is necessary so this software will know what the
        //user wrote.
        //Example: deldir Desktop/NewFolder
        //this code will know that the 'Desktop/NewFolder' is a directory(string) and it will pass it
        //back to the Main() function.
        //learn more at:
        //https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V2.0
    static bool parseArgsBool(string[] args, string[] flags, string desc)
    {
        string message_flags = "";
        usage_flags = usage_flags + string.Format("[{0}] ",flags.First());
        foreach (string flag in flags)
        {
            all_flags_list.Add(flag);
            if (flag != flags.Last()) 
            {
                message_flags = message_flags + flag + ", ";
            }
            else 
            {
                message_flags = message_flags + flag + " ";
            }
        }
        string message = message_flags;
            for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
            message = message + desc;
            ops_args.Add(message);
        foreach (string argument in args)
        {
            foreach (string flag in flags)
            {
                if (argument == flag)
                {
                    return true;
                }
            }
        }
        return false;
    }
    static List<string> parseArgs(string[] args, string name,string desc)
    {
        List<string> all_arguments_list = new List<string>();
        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);
        foreach (string argument in args) 
        {
            if (!all_flags_list.Contains(argument))
            {
                all_arguments_list.Add(argument);
            }
        }
        return all_arguments_list;
    }
    static List<string> all_flags_list = new List<string>();
    static string usage_flags = "";
    static string usage = "Program";
    static string description = "";
    static List<string> pos_args = new List<string>();
    static List<string> ops_args = new List<string>();
    static List<string> more_info = new List<string>();
    static void help_message(string operation, string message)
    {
        if (operation == "usage" && !string.IsNullOrEmpty(message))
        {
            usage = message;
        }
        if (operation == "description" && !string.IsNullOrEmpty(message))
        {
            description = "\n" + message;
        }
        if (operation == "positional argument" && !string.IsNullOrEmpty(message))
        {
           pos_args.Add(message);
        }
        if (operation == "optional argument" && !string.IsNullOrEmpty(message))
        {
            ops_args.Add(message);
        }
        if (operation == "information" && !string.IsNullOrEmpty(message))
        {
            more_info.Add(message);
        }
        if (operation == "call") {
            Console.WriteLine("usage: " + usage + " " + usage_flags);
            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
            }
            if (pos_args.Count > 0)
            {
                Console.WriteLine("\npositional arguments:");
                foreach (string arg in pos_args) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            if (ops_args.Count > 0)
            {
                Console.WriteLine("\noptions:");
                foreach (string arg in ops_args) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            if (more_info.Count > 0)
            {
                Console.WriteLine("\ninformation:");
                foreach (string arg in more_info) 
                {
                    Console.WriteLine("  " + arg);
                }
            }
            Environment.Exit(0);
        }
    }
}

