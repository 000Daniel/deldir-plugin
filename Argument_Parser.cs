public class AP3
{
    //#############################################################################
    //
    // This is my argument parser 3.1, for more information:
    // https://github.com/000Daniel/CSharp-Projects/tree/main/Argument%20Parser%20Template/V3.1
    //
    //#############################################################################
    //
    //
    //
    //this list would contain all arguments that are not flags.
    static List<string> all_arguments_list = new List<string>();
    //after the flags have been declared, the program does not include them in
    //the 'all_arguments_list' list, and sets the 'cleared' boolean to true.
    //more on this later...
    static bool cleared = false;

    //this function compares the args[] to flags[] and returns false or
    //true accordingly. it also automatically adds the description of
    //those flags to 'help_message'.
    public static bool Boolean(string[] args, string[] flags, string desc)
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

    //this function compares all args[] to 'all_flags_list'.
    //Note: all_flags_list contains all the options that trigger the
    //      flags.
    //if 'all_flags_list' does not contain args[], those arguments will
    //be stored in 'all_arguments_list'.
    //for example, listing all Arguments won't include '-q'.
    //
    //overtime the software would remove items from 'all_arguments_list'.
    //this is done so that each positional argument would be checked in its
    //correct location. (not including List argument)
    static void clear_list(string[] args)
    {
        if (!cleared)
        {
            foreach (string argument in args) 
            {
                if (!all_flags_list.Contains(argument))
                {
                    all_arguments_list.Add(argument);
                }
            }
            cleared = true;
        }
    }


    //this function reads a positional argument and returns its Int.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static int Int(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                int result = int.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its uInt.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static uint uInt(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                uint result = uint.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid unsigned Integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its Float.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static float Float(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                float result = float.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Floating-point value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its String.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static string String(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                string result = all_arguments_list[0].ToString();
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid String.");
            }
        }
        return(" ");
    }

    //this function reads a positional argument and returns its Char.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static char Char(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                char result = char.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a single and valid Character.");
            }
        }
        return(' ');
    }

    //this function reads a positional argument and returns its Decimal.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static decimal Decimal(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                decimal result = decimal.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Decimal floating-point value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its Long.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static long Long(string[] args, string name,string desc)
    {
        clear_list(args);

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                long result = long.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Long-integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its uLong.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static ulong uLong(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                ulong result = ulong.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid unsigned Long-integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its Short.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static short Short(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                short result = short.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Short-integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its uShort.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static ushort uShort(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                ushort result = ushort.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid unsigned Short-integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its Double.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static double Double(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                double result = double.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Double-precision floating-point value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its Byte.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static byte Byte(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                byte result = byte.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid unsigned Byte-integer value.");
            }
        }
        return(0);
    }

    //this function reads a positional argument and returns its sByte.
    //each time an argument goes through it removes it from the
    //'all_arguments_list' list.
    public static sbyte sByte(string[] args, string name,string desc)
    {
        clear_list(args);
        

        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        if (all_arguments_list.Count >= 1)
        {
            try
            {
                sbyte result = sbyte.Parse(all_arguments_list[0]);
                all_arguments_list.RemoveAt(0);
                
                return(result);
            }
            catch
            {
                ErrorCall("Please enter a valid Byte-integer value.");
            }
        }
        return(0);
    }

    //this function returns all remaining arguments of 'all_arguments_list'.
    //this function won't return: flags or other positional arguments, depending
    //on its order in Main() function.
    public static List<string> List(string[] args, string name,string desc)
    {
        clear_list(args);
        string message = name;
        for (int i = message.Length; i < 22; i++) 
            {
                message = message + " ";
            }
        message = message + desc;
        help_message("positional argument",message);

        return all_arguments_list;
    }

    //these static variables/data-types are used in this script.
    //they should not be modified or removed.
    static List<string> all_flags_list = new List<string>();
    static string usage_flags = "";
    static string description = "";
    static List<string> pos_args = new List<string>();
    static List<string> ops_args = new List<string>();
    static List<string> more_info = new List<string>();

    //this function is the help_message that recieves data, and when
    //it recieves a call(help_message("call",string.Empty);), it will
    //print out a help message.
    //this help message should automatically include all optional
    //flags and positional arguments.
    //Note: this help_message is formatted like any other 'official'
    //      help page/message that you would find.
    public static void help_message(string operation, string message)
    {
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
            string usage = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
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

                //this function handles Error messages.
                //this would be called when the user did something wrong.
                //it will print the message in RED and exit the program.
    public static void ErrorCall(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error! {0}", message);
        Console.ResetColor();
        Environment.Exit(0);
    }

                //this function handles Warning messages.
                //this would be called when the user did something wrong.
                //it will print the message in YELLOW.
    public static void WarningCall(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Warning! {0}", message);
        Console.ResetColor();
    }
}
//created by https://github.com/000Daniel