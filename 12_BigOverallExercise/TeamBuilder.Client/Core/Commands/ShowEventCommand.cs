namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class ShowEventCommand
    {
        private readonly EventService eventService;

        public ShowEventCommand(EventService eventService)
        {
            this.eventService = eventService;
        }

        // ShowEvent <eventName>
        public string Execute(string[] data)
        {
            string eventName = data[0];

            if (data.Length != 1)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.eventService.IsEventExisting(eventName))
            {
                throw new ArgumentException($"Event {eventName} not found!");
            }

            return this.eventService.ShowEvent(eventName).ToString();
        }
    }
}
