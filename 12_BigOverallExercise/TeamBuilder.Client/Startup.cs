namespace TeamBuilder.Client
{
    using Core;
    using Data;

    public class Startup
    {
        public static void Main()
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Database.Initialize(true);
            }

            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }
    }
}
