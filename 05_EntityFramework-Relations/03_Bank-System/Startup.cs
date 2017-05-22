namespace _03_Bank_System
{
    using Core;
    using System;

    public class Startup
    {
        public static void Main()
        {
            CommandExecutor executor = new CommandExecutor();
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = executor.Execute(input);
                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
