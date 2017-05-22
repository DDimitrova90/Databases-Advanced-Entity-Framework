namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ImportTeamsCommand
    {
        private readonly TeamService teamService;

        public ImportTeamsCommand(TeamService teamService)
        {
            this.teamService = teamService;
        }

        // ImportTeams <filePathToXmlFile>
        public string Execute(string[] data)
        {
            string filePath = data[0];

            if (data.Length != 1)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Path {filePath} is not valid!");
            }

            List<Team> teams = new List<Team>();

            try
            {
                teams = this.teamService.GetTeamsFromXml(filePath);
            }
            catch (Exception)
            {

                throw new FormatException("Xml format not valid!");
            }

            this.teamService.AddTeams(teams);

            return $"You have successfully imported {teams.Count()} teams!";
        }
    }
}
