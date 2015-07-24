using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Modules.UserModule.Services;
using PrismWpfApplication.Infrastructure.Models;
using Moq;
using PrismWpfApplication.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;
using PrismWpfApplication.Infrastructure.Interfaces;
using System.Security;
using System.Threading.Tasks;

namespace UserModule.Test.Services
{
    [TestClass]
    public class BattlenetServiceFixture
    {
        [TestMethod]
        public void ShouldInitializeBattlenetService()
        {
            //Prepare
            var loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);
            
            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.AreEqual(OnlineStatuses.Offline, target.OnlineStatus);
        }

        [TestMethod]
        public void LoginAsRegisteredUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Foo";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach(char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Login(user, password);

            //Verify
            Assert.AreEqual(user, target.CurrentUser.Username);
            Assert.IsTrue(target.IsLoggedIn);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(OnlineStatuses.Online, target.OnlineStatus);
            loginStatusChangedEvent.VerifyAll();
        }

        [TestMethod]
        public async Task LoginAsRegisteredUserAsync()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Foo";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            } 

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = await target.LoginAsync(user, password);

            //Verify
            Assert.AreEqual(user, target.CurrentUser.Username);
            Assert.IsTrue(target.IsLoggedIn);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(OnlineStatuses.Online, target.OnlineStatus);
            loginStatusChangedEvent.VerifyAll();
        }

        [TestMethod]
        public void LoginWithUnregisteredUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Blackstone2";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Login(user, password);

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(UserQueryResult.ResultCode.LogginFailed, result.Code);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Never);
        }

        [TestMethod]
        public async Task LoginWithUnregisteredUserAsync()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Blackstone2";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = await target.LoginAsync(user, password);

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(UserQueryResult.ResultCode.LogginFailed, result.Code);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Never);
        }

        [TestMethod]
        public void LoginLogoutRegisteredUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Foo";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Login(user, password);
            target.Logout();

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.AreEqual(OnlineStatuses.Offline, target.OnlineStatus);
            Assert.IsTrue(result.Success);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Exactly(4));
        }
        
        [TestMethod]
        public void RegisterNewUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Blackstone3";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Register(user, password);

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.IsTrue(result.Success);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Never);
        }

        [TestMethod]
        public void RegisterAndLoginNewUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Blackstone3";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Register(user, password);
            target.Login(user, password);

            //Verify
            Assert.AreEqual(user, target.CurrentUser.Username);
            Assert.IsTrue(target.IsLoggedIn);
            Assert.AreEqual(OnlineStatuses.Online, target.OnlineStatus);
            Assert.IsTrue(result.Success);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Exactly(2));
        }

        [TestMethod]
        public void RegisterExistingUser()
        {
            //Prepare
            Mock<LoginStatusChangedEvent> loginStatusChangedEvent = new Mock<LoginStatusChangedEvent>();
            loginStatusChangedEvent.Setup(x => x.Publish(It.IsAny<IUserService>())).Verifiable();

            Mock<IEventAggregator> eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(loginStatusChangedEvent.Object);

            string user = "Foo";
            char[] pass = "1234".ToCharArray();
            SecureString password = new SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }

            //Act 
            IUserService target = new BattlenetService(eventAggregator.Object);
            UserQueryResult result = target.Register(user, password);

            //Verify
            Assert.IsNull(target.CurrentUser);
            Assert.IsFalse(target.IsLoggedIn);
            Assert.IsFalse(result.Success);
            loginStatusChangedEvent.Verify(x => x.Publish(It.IsAny<IUserService>()), Times.Never);
        }
    }
}
