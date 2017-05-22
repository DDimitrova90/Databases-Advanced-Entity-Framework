namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class CreateEventCommand
    {
        private readonly EventService eventService;

        public CreateEventCommand(EventService eventService)
        {
            this.eventService = eventService;
        }

        // CreateEvent <name> <description> <startDate> <endDate>
        public string Execute(string[] data)
        {
            string name = data[0];
            string description = data[1];
            string startDate = data[2] + " " + data[3];
            string endDate = data[4] + " " + data[5];

            if (data.Length != 6)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.eventService.AreDatesValid(startDate, endDate))
            {
                throw new ArgumentException("Please insert the dates in format: [dd/MM/yyyy HH:mm]!");
            }

            if (!this.eventService.IsStartDateBeforeEndDate(startDate, endDate))
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            this.eventService.AddEvent(name, description, startDate, endDate);

            return $"Event {name} was created successfully!";
        }
    }
}
