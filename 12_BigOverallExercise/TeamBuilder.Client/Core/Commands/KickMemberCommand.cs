namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class KickMemberCommand
    {
        private readonly TeamService teamService;
        private readonly UserService userService;
        private readonly InvitationService invitationService;

        public KickMemberCommand(TeamService teamService, UserService userService, InvitationService invitationService)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.invitationService = invitationService;
        }

        // KickMember <teamName> <username>
        public string Execute(string[] data)
        {
            string teamName = data[0];
            string username = data[1];

            if (data.Length != 2)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.teamService.IsTeamExisting(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!this.invitationService.IsUserMember(teamName, username))
            {
                throw new ArgumentException($"User {username} is not a member in {teamName}!");
            }

            User user = SecurityService.GetCurrentUser();

            if (!this.teamService.IsUserCreator(teamName, user.Username))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            User userToKick = this.userService.GetUserByUsername(username);

            if (this.invitationService.IsInvitedUserCreator(teamName, userToKick.Username))
            {
                throw new InvalidOperationException("Command not allowed. Use DisbandTeam instead.");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.teamService.KickMember(teamName, username);

            return $"User {username} was kicked from {teamName}!";
        }
    }
}
