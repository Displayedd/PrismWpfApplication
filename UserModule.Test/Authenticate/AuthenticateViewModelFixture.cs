using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Infrastructure.Interfaces;
using PrismWpfApplication.Infrastructure.Models;
using PrismWpfApplication.Modules.UserModule.Authenticate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserModule.Test.Authenticate
{
    [TestClass]
    public class AuthenticateViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.GameRegions).Returns(gameRegions);
            mockedUserService.Setup(x => x.HomeRegion).Returns(gameRegions[1]);

            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object, 
                mockedEventAggregator.Object, mockedUserService.Object);

            //Verify
            Assert.IsInstanceOfType(target.GameRegions, typeof(List<GameRegion>));
            Assert.IsFalse(target.IsLoggedIn);
            Assert.IsFalse(target.LoginFailed);
            Assert.IsNotNull(target.LoginCommand);
            Assert.IsNotNull(target.SelectGameRegionCommand);
            Assert.IsNull(target.SecurePassword);
            Assert.IsNull(target.Username);
            Assert.IsInstanceOfType(target.SelectedRegion, typeof(GameRegion));
            Assert.IsNotNull(target.Notification);
            Assert.IsNotNull(target.ResizeMode);
        }

        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.GameRegions).Returns(gameRegions);
            mockedUserService.Setup(x => x.HomeRegion).Returns(gameRegions[1]);

            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);

            bool isLoggedInChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "IsLoggedIn")
                {
                    isLoggedInChangedRaised = true;
                }
            };

            bool userNameChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Username")
                {
                    userNameChangedRaised = true;
                }
            };

            bool passwordChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SecurePassword")
                {
                    passwordChangedRaised = true;
                }
            };

            bool selectedRegionChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "SelectedRegion")
                {
                    selectedRegionChangedRaised = true;
                }
            };

            bool gameRegionsChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "GameRegions")
                {
                    gameRegionsChangedRaised = true;
                }
            };

            bool loginFailedChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "LoginFailed")
                {
                    loginFailedChangedRaised = true;
                }
            };

            bool notificationChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Notification")
                {
                    notificationChangedRaised = true;
                }
            };

            bool resizeModeChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ResizeMode")
                {
                    resizeModeChangedRaised = true;
                }
            };

            //Act
            target.IsLoggedIn = true;
            target.LoginFailed = true;
            target.GameRegions = null;
            target.SecurePassword = new System.Security.SecureString();
            target.SelectedRegion = null;
            target.Username = "";
            target.Notification = "";
            target.ResizeMode = "";

            //Verify
            Assert.IsTrue(isLoggedInChangedRaised);
            Assert.IsTrue(loginFailedChangedRaised);
            Assert.IsTrue(userNameChangedRaised);
            Assert.IsTrue(passwordChangedRaised);
            Assert.IsTrue(selectedRegionChangedRaised);
            Assert.IsTrue(gameRegionsChangedRaised);
            Assert.IsTrue(notificationChangedRaised);
            Assert.IsTrue(resizeModeChangedRaised);
        }

        [TestMethod]
        public void WhenLoginStatusChangedEvent_PropertyUpdated()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.IsLoggedIn).Returns(true);

            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            mockedLoginStatusChangedEvent.Publish(mockedUserService.Object);

            //Verify
            Assert.IsTrue(target.IsLoggedIn);
        }

        [TestMethod]
        public void WhenLoginCommandExecuted_UserLoggedIn()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.LoginAsync(It.Is<string>(user => user == "user"),
                It.Is<SecureString>(pass => pass.Length == 4), It.IsAny<CancellationToken>())).
                Returns(Task.FromResult(new UserQueryResult { Success = true })).Verifiable();

            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            char[] password = "1234".ToCharArray();
            target.SecurePassword = new SecureString();
            foreach (char c in password)
            {
                target.SecurePassword.AppendChar(c);
            }

            target.Username = "user";

            //Act
            target.LoginCommand.Execute(null);

            //Verify
            mockedUserService.Verify(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<SecureString>(),
                It.IsAny<CancellationToken>()), Times.Once);
            mockedUserService.VerifyAll();
            target.LoginFailed = false;
        }

        [TestMethod]
        public void WhenLoginCommandFails_LoginFailedSet()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.LoginAsync(It.Is<string>(user => user == "user"),
                It.Is<SecureString>(pass => pass.Length == 4), It.IsAny<CancellationToken>())).
                Returns( Task.FromResult(new UserQueryResult { Success = false })).Verifiable();

            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            char[] password = "1234".ToCharArray();
            target.SecurePassword = new SecureString();
            foreach (char c in password)
            {
                target.SecurePassword.AppendChar(c);
            }

            target.Username = "user";

            //Act
            target.LoginCommand.Execute(null);

            //Verify
            mockedUserService.Verify(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<SecureString>(), 
                It.IsAny<CancellationToken>()), Times.Once);
            mockedUserService.VerifyAll();
            target.LoginFailed = true;
        }

        [TestMethod]
        public void WhenLoginAsGuestCommandExecuted_UserLoggedIn()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.Login(It.Is<string>(user => user == "Guest"),
                It.Is<SecureString>(pass => pass.Length == 0))).Returns(new UserQueryResult()).Verifiable();

            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);            

            //Act
            target.LoginAsGuestCommand.Execute(null);

            //Verify
            mockedUserService.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<SecureString>()), Times.Once);
            mockedUserService.VerifyAll();
        }

        [TestMethod]
        public void WhenMissingUsernameOrPassword_CantExecuteLoginCommand()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            target.Username = "user";

            //Verify
            Assert.IsFalse(target.LoginCommand.CanExecute(null));
        }
        
        [TestMethod]
        public void WhenNotLoggedIn_NavigateToAuthenticateView()
        {
            //Prepare
            Mock<IRegion> mockRegion = new Mock<IRegion>();
            mockRegion.Setup(x => x.RequestNavigate(new Uri("AuthenticateView", UriKind.Relative), It.IsAny<Action<NavigationResult>>())).Verifiable();

            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();
            mockedRegionManager.Setup(x => x.Regions[RegionNames.ShellContentRegion]).Returns(mockRegion.Object);

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.IsLoggedIn).Returns(false);

            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            target.IsLoggedIn = true;
            mockedLoginStatusChangedEvent.Publish(mockedUserService.Object);

            //Verify
            Assert.IsFalse(target.IsLoggedIn);
            mockRegion.Verify(x => x.RequestNavigate(new Uri("AuthenticateView", UriKind.Relative), It.IsAny<Action<NavigationResult>>()), Times.Once());
        }

        [TestMethod]
        public void WhenSelectGameRegionChanged_HomeRegionUpdated()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            GameRegion[] gameRegions = new GameRegion[] { 
                new GameRegion { Header = "Asia & Southeast Asia"}, 
                new GameRegion { Header = "Europe"},
                new GameRegion { Header = "Asia"},
                new GameRegion { Header = "China"}};
            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.GameRegions).Returns(gameRegions);
            mockedUserService.SetupSet(x => x.HomeRegion = gameRegions[0]).Verifiable();
            mockedUserService.SetupGet(x => x.HomeRegion).Returns(gameRegions[1]);
            
            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);


            //Act
            target.SelectedRegion = gameRegions[0];

            //Verify
            Assert.AreEqual(gameRegions[0], target.SelectedRegion);
            Assert.IsInstanceOfType(target.GameRegions, typeof(List<GameRegion>));
            mockedUserService.VerifySet(x => x.HomeRegion = gameRegions[0]);
        }

        [TestMethod]
        public void WhenSelectGameRegionCommandExecuted_SelectedRegionPropertyChanged()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.Login(It.Is<string>(user => user == "user"),
                It.Is<SecureString>(pass => pass.Length == 2))).Returns(new UserQueryResult()).Verifiable();

            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);

            GameRegion gameRegion = new GameRegion();

            //Act
            target.SelectGameRegionCommand.Execute(gameRegion);

            //Verify
            Assert.AreEqual(gameRegion, target.SelectedRegion);
        }

        [TestMethod]
        public void ValidatesProperties()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();

            //Act
            AuthenticateViewModel target = new AuthenticateViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);

            //Verify
            Assert.IsNotNull(((IDataErrorInfo)target)["Username"]);
            Assert.IsNotNull(((IDataErrorInfo)target)["SecurePassword"]);
        }

        private class MockLoginStatusChangedEvent : LoginStatusChangedEvent
        {
            public Action<IUserService> SubscribeArgumentAction;
            public Predicate<IUserService> SubscribeArgumentFilter;
            public ThreadOption SubscribeArgumentThreadOption;

            public override SubscriptionToken Subscribe(Action<IUserService> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<IUserService> filter)
            {
                SubscribeArgumentAction = action;
                SubscribeArgumentFilter = filter;
                SubscribeArgumentThreadOption = threadOption;
                return null;
            }

            public override void Publish(IUserService payload)
            {
                this.SubscribeArgumentAction(payload);
            }
        }
    }
}
