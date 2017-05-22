namespace PhotoShare.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Client.Core.Commands;
    using Mocks;

    [TestClass]
    public class RegisterCommandTests
    {
        [TestMethod]
        public void Register_NewUser_Should_SuccessMessage()
        {
            // Arrange, Act, Assert
            string[] commandParameters = new string[] { "username", "passw0rd", "passw0rd", "user@u.com" };

            RegisterUserCommand registerUser = new RegisterUserCommand(new UserServiceMock());
            string result = registerUser.Execute(commandParameters);

            Assert.AreEqual($"User {commandParameters[0]} was registered successfully!", result);
        }
    }
}
