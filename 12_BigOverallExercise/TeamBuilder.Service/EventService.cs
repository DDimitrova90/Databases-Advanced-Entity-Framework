namespace TeamBuilder.Service
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventService
    {
        public void AddEvent(string name, string description, string currStartDate, string currEndDate)
        {
            DateTime startDate;
            bool isStartDateValid = DateTime.TryParseExact(currStartDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
            DateTime endDate;
            bool isEndDateValid = DateTime.TryParseExact(currEndDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event currEvent = new Event();
                currEvent.Name = name;
                currEvent.Description = description;
                currEvent.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
                currEvent.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0);
                User currUser = SecurityService.GetCurrentUser();
                currEvent.Creator = context.Users.SingleOrDefault(u => u.Username == currUser.Username);

                context.Events.Add(currEvent);
                context.SaveChanges();
            }
        }

        public StringBuilder ShowEvent(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event currEvent = context.Events
                    .Where(e => e.Name == eventName)
                    .OrderByDescending(e => e.StartDate)
                    .FirstOrDefault();

                StringBuilder strb = new StringBuilder();
                strb.AppendLine($"{currEvent.Name} {currEvent.StartDate} {currEvent.EndDate}");
                strb.AppendLine(currEvent.Description);
                strb.AppendLine("Teams:");

                foreach (var team in currEvent.Teams)
                {
                    strb.AppendLine($"-{team.Name}");
                }

                return strb;
            }
        }

        public bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }

        public bool IsUserCreator(string eventName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username);
                Event currEvent = context.Events
                    .Where(e => e.Name == eventName)
                    .OrderByDescending(e => e.StartDate)
                    .FirstOrDefault();

                if (currEvent.Creator.Username == user.Username)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsTeamAddedToEvent(string teamName, string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.SingleOrDefault(t => t.Name == teamName);

                return context.Events.Any(e => e.Teams.Any(t => t.Name == team.Name));
            }
        }

        public bool AreDatesValid(string currStartDate, string currEndDate)
        {
            DateTime startDate;
            bool isStartDateValid = DateTime.TryParseExact(currStartDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
            DateTime endDate;
            bool isEndDateValid = DateTime.TryParseExact(currEndDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);

            if (!isStartDateValid || !isEndDateValid)
            {
                return false;
            }

            return true;
        }

        public bool IsStartDateBeforeEndDate(string currStartDate, string currEndDate)
        {
            DateTime startDate;
            bool isStartDateValid = DateTime.TryParseExact(currStartDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate);
            DateTime endDate;
            bool isEndDateValid = DateTime.TryParseExact(currEndDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate);

            if (startDate > endDate)
            {
                return false;
            }

            return true;
        }
    }
}
