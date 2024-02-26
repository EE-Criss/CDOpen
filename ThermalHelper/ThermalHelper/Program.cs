﻿

using ESC_POS_USB_NET.Printer;
using System.Text;

namespace CRX
{
    class App
    {
        const string CurrentClass = "C:CXR";

        App(string[] args)
        {

            //DEBUG FOR PRINTING ARGUMENTS
            //Console.WriteLine(CurrentClass);
            Console.WriteLine(args[0]);

            //WE GET THE ARGUMENTS WE NEED FROM CMD LINE 0
            // EXEC_TYPE @ PRINER_NAME @ CD# @ OPT1 @ OPT2

            /* There are 3 options
             * EASY - We use the library to open the cash drawer.
             * AUTO - Using the most common command to open the cash drawer
             * MANUAL - We manually set the pulse length to open the cash drawer (OPT1 & OPT2) */
            //spool.exe is a free software used to print, -> https://www.compuphase.com/software_spool.htm

            int idx = args[0].IndexOf(":");
            string command = args[0].Substring(idx + 1);
            Console.WriteLine(command);
            string[] subs = command.Split('@');
            var PrinterName = subs[1];

            string docPath;
            string strCmdText;
            char[] lines;
            string[] sas;
            ASCIIEncoding ascii = new ASCIIEncoding();

            switch (subs[0])
            {
                case "EASY":
                    
                    //We use the library as this may be the easiest way to achieve this
                    Printer printer = new Printer(PrinterName);
                    printer.OpenDrawer();
                    break;

                case "AUTO":
                    
                    try
                    {
                        // Create a string array with the lines of text, in this case commands
                        lines = new char[] { (char)27, (char)112, (char)Int32.Parse(subs[2]), (char)25, (char)250};
#if DEBUG
                        docPath = @Environment.CurrentDirectory;

#else
                        docPath = "C:\\ThermalHelperCRX";
#endif
                        // Write the string array to a new file

                        if (File.Exists(@docPath+ "\\OPENCD.plts"))
                        {
                            File.Delete(@docPath + "\\OPENCD.plts");
                        }

                        using (StreamWriter outputFile = new StreamWriter(File.Open(Path.Combine(docPath, "OPENCD.plts"), FileMode.CreateNew), Encoding.Latin1))
                        {
                            foreach (char line in lines)
                                outputFile.Write(line);
                        }
                        strCmdText = "/C " +docPath+ "\\spool.exe " + docPath + "\\OPENCD.plts " + PrinterName;
                        //Console.WriteLine(strCmdText);

                        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                   
                    break;
                case "MANUAL":

                    try { 
                    // Create a string array with the lines of text, in this case commands
                    lines = new char[] { (char)27, (char)112, (char)Int32.Parse(subs[2]), (char)Int32.Parse(subs[3]), (char)Int32.Parse(subs[4]) };
#if DEBUG
                    docPath = @Environment.CurrentDirectory;
#else
                        docPath = "C:\\ThermalHelperCRX";
#endif
                    //Need to see if file is already created, else file creation will make the app to exit without executing
                    if (File.Exists(@docPath + "\\OPENCD.plts"))
                    {
                        File.Delete(@docPath + "\\OPENCD.plts");
                    }
                    // Write the string array to a new file
                    using (StreamWriter outputFile = new StreamWriter(File.Open(Path.Combine(docPath, "OPENCD.plts"), FileMode.CreateNew), Encoding.Latin1))
                    {
                        foreach (char line in lines)
                            outputFile.Write(line);
                    }
                    strCmdText = "/C " + docPath + "\\spool.exe " + docPath + "\\OPENCD.plts " + PrinterName;
                    Console.WriteLine(strCmdText);
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }

        }

        public static void Main(string[] args)
        {
            const string CurrentClass = "M:PrincipalThread";
            try
            {
                new App(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(CurrentClass);
                Array.ForEach(args, Console.WriteLine);
            }
        }
    }
}