namespace TeamBuilder.Client.Core.Commands
{
    using System;

    public class ExitCommand
    {
        // Exit
        public string Execute(string[] data)
        {
            if (data.Length > 0)
            {
                throw new FormatException("Invalid arguments count!");
            }

            Environment.Exit(0);
            return "Bye-bye!";
        }
    }
}
