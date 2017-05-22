namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class CreateTeamCommand
    {
        private readonly TeamService teamService;

        public CreateTeamCommand(TeamService teamService)
        {
            this.teamService = teamService;
        }

        // CreateTeam <name> <acronym> <description>
        public string Execute(string[] data)
        {
            string name = data[0];
            string acronym = data[1];
            string description = string.Empty;

            if (data.Length < 2 || data.Length > 3)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (data.Length == 3)
            {
                description = data[2];
            }

            if (this.teamService.IsTeamExisting(name))
            {
                throw new ArgumentException($"Team {name} exists!");
            }

            if (!this.teamService.IsAcronymValid(acronym))
            {
                throw new ArgumentException($"Acronym {acronym} not valid!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.teamService.AddTeam(name, acronym, description);

            return $"Team {name} successfully created!";
        }
    }
}
