namespace TeamBuilder.Service
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InvitationService
    {
        public void AddInvitation(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {

                Invitation invitation = new Invitation();
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                User user = context.Users.SingleOrDefault(u => u.Username == username);
                invitation.Team = team;
                invitation.InvitedUser = user;
                invitation.IsActive = true;

                context.Invitations.Add(invitation);
                context.SaveChanges();
            }
        }

        public void AcceptInvite(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.FirstOrDefault(i => i.Team.Name == teamName && i.IsActive == true);
                User user = context.Users.SingleOrDefault(u => u.Username == invitation.InvitedUser.Username);
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                user.ParticipateTeams.Add(team);

                context.SaveChanges();

                team.Members.Add(user);
                invitation.IsActive = false;

                context.SaveChanges();
            }
        }

        public void DeclineInvite(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.SingleOrDefault(i => i.Team.Name == teamName && i.IsActive == true);
                invitation.IsActive = false;

                context.SaveChanges();
            }
        }

        public bool IsInvitationExisting(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i => 
                i.Team.Name == teamName && 
                i.InvitedUser.Username == username);
            }
        }

        public bool IsInvitationByTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i => i.Team.Name == teamName);
            }
        }

        public bool IsInvitationActive(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.SingleOrDefault(i =>
                i.Team.Name == teamName &&
                i.InvitedUser.Username == username);

                if (invitation == null)
                {
                    return false;
                }

                return invitation.IsActive;
            }
        }

        public bool IsInvitedUserCreator(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.SingleOrDefault(i => i.Team.Name == teamName && i.InvitedUser.Username == username);

                if (invitation != null && invitation.InvitedUser.Username == invitation.Team.Creator.Username)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsUserMember(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.SingleOrDefault(i => i.Team.Name == teamName && i.InvitedUser.Username == username);

                if (invitation != null && invitation.Team.Members.Any(m => m.Username == invitation.InvitedUser.Username))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
