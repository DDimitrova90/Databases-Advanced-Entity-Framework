namespace TeamBuilder.Service
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class TeamService
    {
        public void AddTeam(string name, string acronym, string description)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = new Team();
                team.Name = name;
                team.Acronym = acronym;

                if (description != string.Empty)
                {
                    team.Description = description;
                }

                User currUser = SecurityService.GetCurrentUser();
                team.Creator = context.Users.SingleOrDefault(u => u.Username == currUser.Username);

                context.Teams.Add(team);
                context.SaveChanges();
            }
        }

        public void AddMember(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                team.Members.Add(user);

                context.SaveChanges();
            }
        }

        public void KickMember(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                team.Members.Remove(user);
                user.ParticipateTeams.Remove(team);

                context.SaveChanges();
            }
        }

        public void DisbandTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);
                team.IsDeleted = true;

                context.SaveChanges();
            }
        }

        public void AddTeamTo(string teamName, string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
               Event currEvent = context.Events
                    .Where(e => e.Name == eventName)
                    .OrderByDescending(e => e.StartDate)
                    .First();
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);

                currEvent.Teams.Add(team);
                team.Events.Add(currEvent);

                context.SaveChanges();
            }
        }

        public void AddTeams(List<Team> teams)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Teams.AddRange(teams);
                context.SaveChanges();
            }
        }

        public Team GetTeamByName(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Include("Members").SingleOrDefault(t => t.Name == teamName);
            }
        }

        public void ExportTeamToJson(Team team)
        {
            string jsonTeam = JsonConvert.SerializeObject(new
            {
                Name = team.Name,
                Acronym = team.Acronym,
                Members = team.Members.Select(m => m.Username)    
            }, 
            Formatting.Indented);

            File.WriteAllText("team.json", jsonTeam);
        }

        public List<Team> GetTeamsFromXml(string filePath)
        {
            XDocument teamsXml = XDocument.Load(filePath);
            var teamsElements = teamsXml.Root.Elements();

            List<Team> teams = new List<Team>();

            foreach (var te in teamsElements)
            {
                Team team = new Team();

                string name = te.Element("name").Value;
                string acronym = te.Element("acronym").Value;
                string description = te.Element("description").Value;
                int creatorId = int.Parse(te.Element("creator-id").Value);

                team.Name = name;
                team.Acronym = acronym;
                team.Description = description;
                team.CreatorId = creatorId;

                teams.Add(team);
            }

            return teams;
        }

        public StringBuilder ShowTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);

                StringBuilder strb = new StringBuilder();
                strb.AppendLine($"{team.Name} {team.Acronym}");
                strb.AppendLine("Members:");

                foreach (var member in team.Members)
                {
                    strb.AppendLine($"--{member.Username}");
                }

                return strb;
            }
        }

        public bool IsTeamExisting(string name)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == name);
            }
        }

        public bool IsUserCreator(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username);
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);

                if (team.Creator.Username == user.Username)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsAcronymValid(string acronym)
        {
            if (acronym.Length != 3)
            {
                return false;
            }

            return true;
        }
    }
}
