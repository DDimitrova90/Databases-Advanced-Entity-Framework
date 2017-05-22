namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class ExportTeamCommand
    {
        private readonly TeamService teamService;

        public ExportTeamCommand(TeamService teamService)
        {
            this.teamService = teamService;
        }

        // ExportTeam <teamName>
        public string Execute(string[] data)
        {
            string teamName = data[0];

            if (data.Length != 1)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.teamService.IsTeamExisting(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            Team team = this.teamService.GetTeamByName(teamName);
            this.teamService.ExportTeamToJson(team);

            return $"Team {teamName} exported!";
        }
    }
}
