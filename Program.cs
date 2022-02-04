class deldir
{
    static ConsoleColor color_directory = ConsoleColor.Magenta;
    static ConsoleColor color_folder = ConsoleColor.Green;
    static ConsoleColor color_file = ConsoleColor.Yellow;
    static ConsoleColor color_file2 = ConsoleColor.DarkYellow;
    static ConsoleColor color_folder2 = ConsoleColor.DarkGreen;
    static ConsoleColor color_warning = ConsoleColor.Red;
    static List<string> total_folders = new List<string>();
    static List<string> total_files = new List<string>();
    static bool Quiet = false;
    static bool Information = false;

    static void Main (string[] args)
    {
            // All this code is to get data from my Argument Parser.
            // more info abount this code at:
            // https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V3.1
        bool Help = AP3.Boolean(args, new string[]{ "-h" , "--i" , "--help"}, "show this help message and exit");
        Quiet = AP3.Boolean(args, new string[]{ "-q" , "--quiet"}, "don't print folder/file tree");
        Information = AP3.Boolean(args, new string[]{ "-i" , "--information"}, "display extra information");
        bool BaseDir = AP3.Boolean(args, new string[]{ "-b" , "--basedirectory"}, "delete the base directory");
        bool FolderOnly = AP3.Boolean(args, new string[]{ "--fol" , "--foldersonly"}, "delete only folders");
        bool FileOnly = AP3.Boolean(args, new string[]{ "--fil" , "--filesonly"}, "delete only files");
        List<string> Argument = AP3.List(args, "path", "change the path");
        AP3.help_message("description","Directory eraser");
        AP3.help_message("information","version 1.3");
        AP3.help_message("information","created by https://github.com/000Daniel");
        if (Help) {AP3.help_message("call",string.Empty);}
    
    try
    {
            // this software finds all files and folders in this order:
            // 1. find all folders in the base directory <add to list>
            // 2. go inside each folder and look for all files <add to list>
            // 3. now look for more folders and repeat
            // 4. find all files in base directory <add to list>
        var CurrentDir = "";
        try
        {
            CurrentDir = Directory.GetCurrentDirectory();
        }
        catch
        {
            AP3.ErrorCall("program executed at an invalid directory.");
        }
        bool onlyOneFile = false;
        if (Argument.Count() > 0) 
        {
            foreach (string arg in Argument)
            {
            // default/base path = the path that this software is executed at.
            // this code allows the user to choose a path different than the
            // base path, thanks to my Argument Parser(more on this later). 
                if (arg.IndexOf("/") == 0)
                {
                    if (Directory.Exists(arg))
                    {
                        CurrentDir = Path.GetFullPath(arg);
                        break;
                    }

                    if (File.Exists(arg))
                    {
                        total_files.Add(arg);
                        onlyOneFile = true;
                        break;
                    }

                    AP3.ErrorCall("directory or file missing.");
                }
                else if (Directory.Exists(CurrentDir + "/" + arg))
                {
                    CurrentDir = Path.GetFullPath(CurrentDir + "/" + arg);
                }
                else
                {
                    if (File.Exists(CurrentDir + "/" + arg))
                    {
                        total_files.Add(CurrentDir + "/" + arg);
                        onlyOneFile = true;
                        break;
                    }

                    AP3.ErrorCall("directory or file missing.");
                }
            }
        }
            // 'onlyOneFile' means that the user chose to delete a single file, the software would
            // skip all the folder and files checks.
        if (!onlyOneFile)
        {
            // these lists contain all files and folders with full paths.
            // the next code, does what was mentioned earlier with finding all the files and folders.
            // Note: it looks in the base directory OR the directory that the user chose.
            string[] allFiles = Directory.GetFiles(CurrentDir);
            string[] allFolders = Directory.GetDirectories(CurrentDir);
            Array.Sort(allFiles);
            Array.Sort(allFolders);

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
                string folder_name = folder.Substring(folder.LastIndexOf("/") + 1);

                if (!Quiet)
                {
                    treeBranch = treeBranch + "  ";

                    Console.ForegroundColor = color_folder;

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
                            Console.WriteLine(treeBranch + "╚═══" + file);
                        }
                        else
                        {
                            Console.WriteLine(treeBranch + "╚═══" + file.Substring(file.LastIndexOf("/") + 1));
                        }
                    }
                    total_files.Add(file);
                }
                Console.ResetColor();
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
                        break;
                    }
                    Console.WriteLine(file.Substring(file.LastIndexOf("/") + 1));
                }
                total_files.Add(file);
            }
            Console.ResetColor();
        }
            // this message asks whether the user wants to delete all files and folders, and lists
            // how many will be deleted.
            // if the user types 'yes' the software will delete the files/folders
            //
            // 'FolderOnly' and 'FileOnly' --> delete only folders or only files.
        if (FolderOnly && !FileOnly)
        {
            total_files = new List<string>();
        }
        if (FileOnly && !FolderOnly)
        {
            total_folders = new List<string>();
        }
        Console.ForegroundColor = color_warning;
        Console.WriteLine("\nYou are about to delete {0} Files and {1} Folders.",total_files.Count(),total_folders.Count());
        
            // '-b' --> delete base directory.
        if (BaseDir)
        {
            Console.WriteLine("And about to delete this directory: " + CurrentDir);
        }
        Console.ResetColor();

            // final confirmation message whether the user wants to delete the files/folders.
        Console.Write("Are you sure? [yes/N]");
        string userInput = Console.ReadLine()!.ToString();
        
            // this deletes all the requested folders and files.
            // FolderOnly --> delete ONLY folders.
            // FileOnly   --> delete ONLY files.
            // BaseDir    --> delete the base directory.
        if (!string.IsNullOrEmpty(userInput) && userInput.ToLower() == "yes")
        {
            Console.ForegroundColor = color_warning;
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
                    AP3.WarningCall(string.Format("couldn't delete '{0}'.",total_file));
                }
            }

            for (int i=total_folders.Count() -1; i >= 0; i--)
            {
                if (!Quiet)
                {
                    Console.WriteLine("Deleting folder: " + total_folders[i]);
                }
                try
                {
                    Directory.Delete(total_folders[i]);
                }
                catch
                {
                    AP3.WarningCall(string.Format("couldn't delete '{0}'.",total_folders[i]));
                }
            }
            if (BaseDir)
            {
                if (!Quiet)
                {
                    Console.WriteLine("Deleting base directory: " + CurrentDir);
                }
                try
                {
                    Directory.Delete(CurrentDir);
                }
                catch
                {
                    AP3.WarningCall("couldn't delete base directory.");
                }
            }
        }
        Console.ResetColor();
        Console.WriteLine("");
        Environment.Exit(0);
    }
    catch (Exception e)
    {
            // this exception is here to catch every error possible inside Main() function.
        AP3.ErrorCall(String.Format("something went wrong!\n{0}", e.Message));
    }
    }


            // this function looks for folders and files INSIDE a folder.
            // and it adds them to 'total_folders' and 'total_files'
            // this function will also loop multiple times until it finds all files and folders.
    static string treeBranch = "";
    static void researchFolder(string CurrentDir,string folder_name)
    {
        bool reset_length = false;
            // terminal_length will determine when should the tree reset its 'treeBranch'.
            // if a files/folder name is too long, the plugin would reset its 'treeBranch'.
            // minimum terminal_length is 20.
        int terminal_length = Console.BufferWidth - 5;
        if (terminal_length < 20)
        {
            terminal_length = 20;
        }
        if (treeBranch.Length + folder_name.Length > terminal_length)
        {
            treeBranch = "";
            reset_length = true;
        }

            // this prints the folders that are inside other folders.
        string[] allFolders = Directory.GetDirectories(CurrentDir + "/" + folder_name);
            // 'Array.Sort()' is to sort by the names of the folders and files.
        Array.Sort(allFolders);
        foreach (string folder in allFolders)
        {
            total_folders.Add(folder);

            Console.ForegroundColor = color_folder;
            string next_folder_name = folder.Substring(folder.LastIndexOf("/") + 1);
            if (!Quiet)
            {
                string display_text = "";

                if (Information)
                {
                    display_text = folder;
                }
                else
                {
                    display_text = next_folder_name;
                }
                if (reset_length)
                {
                    Console.ForegroundColor = color_folder2;
                    try
                    {
                        string temp_str = folder.Substring(0,folder.LastIndexOf("/"));
                        string temp_str2 = temp_str.Substring(temp_str.LastIndexOf("/") + 1);
            // this shortens the name of the parent folder.
                        if (temp_str2.Length > 5)
                        {
                            temp_str2 = temp_str2.Substring(temp_str2.Length - 5);
                            Console.Write(".." + temp_str2);
                        }
                        else
                        {
                            Console.Write(temp_str2);
                        }
                        Console.ForegroundColor = color_folder;
                        Console.Write("════{0}\n",display_text);
            // adds spaces(the length of the parent's folder name) to 'treeBranch'.
                        treeBranch = treeBranch + new string(' ',temp_str2.Length + 1);
                    }
                    catch (Exception e)
                    {
                        AP3.ErrorCall("something went wrong with the folder search system!\n" + e.Message);
                    }
                }
                else
                {
            // prints last folder with '╚═══', while every other folder with '╠═══'.
                    if (folder.Equals(allFolders.Last()))
                    {
                        Console.WriteLine(treeBranch + "╚═══" + display_text);
                    }
                    else
                    {
                        Console.WriteLine(treeBranch + "╠═══" + display_text);
                    }
                }
                Console.ResetColor();
            }
            // if the folder is the last folder in the directory dont add a '║'.
            if (folder.Equals(allFolders.Last()))
            {
                treeBranch = treeBranch + "  ";
            }
            else
            {
                treeBranch = treeBranch + "║ ";
            }
            
            // this prints the files inside the folders.
            string[] allFiles = Directory.GetFiles(CurrentDir + "/" + folder_name + "/" + next_folder_name);
            Array.Sort(allFiles);
            foreach (string file in allFiles)
            {
                total_files.Add(file);
                if (!Quiet)
                {
                    string file_short = file.Substring(file.LastIndexOf("/") + 1);
            // reset a 'treeBranch' if the file name is too long.
                    if (treeBranch.Length + file_short.Length > terminal_length)
                    {
                        treeBranch = "";
                        Console.ForegroundColor = color_file2;
                        try
                        {
                            Console.WriteLine(".." + file_short);
                        }
                        catch (Exception e)
                        {
                            AP3.ErrorCall("something went wrong with the file search system!\n" + e.Message);
                        }
                        break;
                    }
                    
            // prints the 'treeBranch' in green, while the file name in yellow.
                    if (Information)
                    {
                        Console.ForegroundColor = color_folder;
                        Console.Write(treeBranch);

                        Console.ForegroundColor = color_file;
                        Console.WriteLine("╚═══" + file);
                    }
                    else
                    {
                    Console.ForegroundColor = color_folder;
                    Console.Write(treeBranch);

                    Console.ForegroundColor = color_file;
                    Console.WriteLine("╚═══" + file.Substring(file.LastIndexOf("/") + 1));
                    }
                }
                Console.ResetColor();
            }
            // this starts searching the next folder.
            researchFolder(CurrentDir + "/" + folder_name, next_folder_name);
        }
            // remove the last 2(two) characters from 'treeBranch', each time the program
            // goes back 1(one) directory.
        if (treeBranch.Length > 1)
        {
            treeBranch = treeBranch.Substring(0,treeBranch.Length - 2);
        }
    }
}
//created by https://github.com/000Daniel
