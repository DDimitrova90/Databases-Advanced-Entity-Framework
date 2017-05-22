namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class DisbandTeamCommand
    {
        private readonly TeamService teamService;

        public DisbandTeamCommand(TeamService teamService)
        {
            this.teamService = teamService;
        }

        // Disband <teamName>
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

            User user = SecurityService.GetCurrentUser();

            if (!this.teamService.IsUserCreator(teamName, user.Username))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.teamService.DisbandTeam(teamName);

            return $"{teamName} has disbanded!";
        }
    }
}
