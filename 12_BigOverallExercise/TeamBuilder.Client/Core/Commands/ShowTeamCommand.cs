namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class ShowTeamCommand
    {
        private readonly TeamService teamService;

        public ShowTeamCommand(TeamService teamService)
        {
            this.teamService = teamService;
        }

        // ShowTeam <teamName>
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

            return this.teamService.ShowTeam(teamName).ToString().Trim();
        }
    }
}
