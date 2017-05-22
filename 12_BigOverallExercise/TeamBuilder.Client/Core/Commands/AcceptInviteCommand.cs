namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class AcceptInviteCommand
    {
        private readonly InvitationService invitationService;
        private readonly TeamService teamService;

        public AcceptInviteCommand(InvitationService invitationService, TeamService teamService)
        {
            this.invitationService = invitationService;
            this.teamService = teamService;
        }

        // AcceptInvite <teamName>
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

            if (!this.invitationService.IsInvitationByTeamExisting(teamName))
            {
                throw new ArgumentException($"Invite from {teamName} is not found!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.invitationService.AcceptInvite(teamName);

            User user = SecurityService.GetCurrentUser();

            return $"User {user.Username} joined team {teamName}!";
        }
    }
}
