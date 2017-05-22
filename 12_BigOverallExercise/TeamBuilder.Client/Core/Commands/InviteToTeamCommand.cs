namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class InviteToTeamCommand
    {
        private readonly TeamService teamService;
        private readonly InvitationService invitationService;
        private readonly UserService userService;

        public InviteToTeamCommand(TeamService teamService, UserService userService, InvitationService invitationService)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.invitationService = invitationService;
        }

        // InviteToTeam <teamName> <username>
        public string Execute(string[] data)
        {
            string teamName = data[0];
            string username = data[1];

            if (data.Length != 2)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (this.invitationService.IsUserMember(teamName, username))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            if (!this.teamService.IsTeamExisting(teamName) || !this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException("Team or user does not exist!");
            }

            if (this.invitationService.IsInvitationExisting(teamName, username) && this.invitationService.IsInvitationActive(teamName, username))
            {
                throw new InvalidProgramException("Invite is already sent!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (this.invitationService.IsInvitedUserCreator(teamName, username))
            {
                this.teamService.AddMember(teamName, username);
            }

            this.invitationService.AddInvitation(teamName, username);

            return $"Team {teamName} invited {username}!";
        }
    }
}
