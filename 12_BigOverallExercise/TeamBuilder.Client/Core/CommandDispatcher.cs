namespace TeamBuilder.Client.Core
{
    using Commands;
    using Service;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string commandName = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray();
            string result = string.Empty;

            UserService userService = new UserService();
            EventService eventService = new EventService();
            TeamService teamService = new TeamService();
            InvitationService invitationService = new InvitationService();

            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand(userService);
                    result = registerUser.Execute(commandParameters);
                    break;
                case "Login":
                    LoginCommand loginCommand = new LoginCommand();
                    result = loginCommand.Execute(commandParameters);
                    break;
                case "Logout":
                    LogoutCommand logoutCommand = new LogoutCommand();
                    result = logoutCommand.Execute(commandParameters);
                    break;
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand(userService);
                    result = deleteUser.Execute(commandParameters);
                    break;
                case "CreateEvent":
                    CreateEventCommand createEvent = new CreateEventCommand(eventService);
                    result = createEvent.Execute(commandParameters);
                    break;
                case "CreateTeam":
                    CreateTeamCommand createTeam = new CreateTeamCommand(teamService);
                    result = createTeam.Execute(commandParameters);
                    break;
                case "InviteToTeam":
                    InviteToTeamCommand inviteToTeam = new InviteToTeamCommand(teamService, userService, invitationService);
                    result = inviteToTeam.Execute(commandParameters);
                    break;
                case "AcceptInvite":
                    AcceptInviteCommand acceptInvite = new AcceptInviteCommand(invitationService, teamService);
                    result = acceptInvite.Execute(commandParameters);
                    break;
                case "DeclineInvite":
                    DeclineInviteCommand declineInvite = new DeclineInviteCommand(invitationService, teamService);
                    result = declineInvite.Execute(commandParameters);
                    break;
                case "KickMember":
                    KickMemberCommand kickMember = new KickMemberCommand(teamService, userService, invitationService);
                    result = kickMember.Execute(commandParameters);
                    break;
                case "Disband":
                    DisbandTeamCommand disbandTeam = new DisbandTeamCommand(teamService);
                    result = disbandTeam.Execute(commandParameters);
                    break;
                case "AddTeamTo":
                    AddTeamToCommand addTeamTo = new AddTeamToCommand(teamService, eventService);
                    result = addTeamTo.Execute(commandParameters);
                    break;
                case "ShowEvent":
                    ShowEventCommand showEvent = new ShowEventCommand(eventService);
                    result = showEvent.Execute(commandParameters);
                    break;
                case "ShowTeam":
                    ShowTeamCommand showTeam = new ShowTeamCommand(teamService);
                    result = showTeam.Execute(commandParameters);
                    break;
                case "Exit":
                    ExitCommand exitCommand = new ExitCommand();
                    result = exitCommand.Execute(commandParameters);
                    break;
                case "ImportUsers":
                    ImportUsersCommand importUsers = new ImportUsersCommand(userService);
                    result = importUsers.Execute(commandParameters);
                    break;
                case "ImportTeams":
                    ImportTeamsCommand importTeams = new ImportTeamsCommand(teamService);
                    result = importTeams.Execute(commandParameters);
                    break;
                case "ExportTeam":
                    ExportTeamCommand exportTeam = new ExportTeamCommand(teamService);
                    result = exportTeam.Execute(commandParameters);
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} not valid!");
            }

            return result;

            /*
             * This is with reflection and have to remove switch()...
            // Get command's type.
            Type commandType = Type.GetType(
                                 "TeamBuilder.App.Core.Commands." + commandName + "Command");

            // If command's type is not found – it is not valid command.
            if (commandType == null)
            {
                throw new NotSupportedException($"Command {commandName} not supported!");
            }

            // Create instance of command with the type that we already extracted.
            object command = Activator.CreateInstance(commandType);

            // Get the method called “Execute” of the command.
            MethodInfo executeMethod = command.GetType().GetMethod("Execute");

            // Invoke the method we found passing the instance of the command and
            // array of all expected arguments that the method should take when it is invoked.
            result = executeMethod.Invoke(command, new object[] { inputArgs }) as string;
            */
        }
    }
}
