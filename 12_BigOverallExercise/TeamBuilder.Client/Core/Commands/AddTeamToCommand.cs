namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class AddTeamToCommand
    {
        private readonly TeamService teamService;
        private readonly EventService eventService;

        public AddTeamToCommand(TeamService teamService, EventService eventService)
        {
            this.teamService = teamService;
            this.eventService = eventService;
        }

        // AddTeamTo <eventName> <teamName>
        public string Execute(string[] data)
        {
            string eventName = data[0];
            string teamName = data[1];

            if (data.Length != 2)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.eventService.IsEventExisting(eventName))
            {
                throw new ArgumentException($"Event {eventName} not found!");
            }

            if (!this.teamService.IsTeamExisting(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            User user = SecurityService.GetCurrentUser();

            if (!this.eventService.IsUserCreator(eventName, user.Username))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            if (this.eventService.IsTeamAddedToEvent(teamName, eventName))
            {
                throw new InvalidOperationException("Cannot add same team twice!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.teamService.AddTeamTo(teamName, eventName);

            return $"Team {teamName} added for {eventName}!";
        }
    }
}
