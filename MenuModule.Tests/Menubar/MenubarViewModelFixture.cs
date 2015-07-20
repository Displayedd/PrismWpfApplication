using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Practices.Prism.Regions;
using PrismWpfApplication.Infrastructure;
using PrismWpfApplication.Modules.MenuModule.Menubar;
using PrismWpfApplication.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using System.ComponentModel;
using PrismWpfApplication.Infrastructure.Models;

namespace MenuModule.Tests
{
    [TestClass]
    public class MenubarViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Offline);

            //Act
            MenubarViewModel target = new MenubarViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);

            //Verify
            Assert.IsNull(target.Username);
            Assert.AreEqual(OnlineStatuses.Offline, target.OnlineStatus);
        }

        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Offline);

            MenubarViewModel target = new MenubarViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);


            bool userNameChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Username")
                {
                    userNameChangedRaised = true;
                }
            };

            bool onlineStatusChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "OnlineStatus")
                {
                    onlineStatusChangedRaised = true;
                }
            };

            target.Username = "";
            target.OnlineStatus = OnlineStatuses.Online;

            //Verify
            Assert.IsTrue(userNameChangedRaised);
            Assert.IsTrue(onlineStatusChangedRaised);
        }

        [TestMethod]
        public void WhenLoginStatusChangedEvent_PropertiesUpdated()
        {
            //Prepare
            Mock<IRegionManager> mockedRegionManager = new Mock<IRegionManager>();

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            Mock<IUserService> mockedUserService = new Mock<IUserService>();
            mockedUserService.Setup(x => x.IsLoggedIn).Returns(true);
            mockedUserService.Setup(x => x.CurrentUser).Returns(new UserInformation { Username = "User" });
            mockedUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Online);

            //Act
            MenubarViewModel target = new MenubarViewModel(mockedRegionManager.Object,
                mockedEventAggregator.Object, mockedUserService.Object);
            mockedLoginStatusChangedEvent.Publish(mockedUserService.Object);

            //Verify
            Assert.AreEqual("User", target.Username);
            Assert.AreEqual(OnlineStatuses.Online, target.OnlineStatus);
        }

        [TestMethod]
        public void WhenShowGamesViewCommandExecuted_NavigatesToGameView()
        {
            //Prepare
            Mock<IRegion> mockMainRegion = new Mock<IRegion>();
            mockMainRegion.Setup(x => x.RequestNavigate(new Uri("GamesView", UriKind.Relative), It.IsAny<Action<NavigationResult>>())).Verifiable();

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            mockRegionManager.Setup(x => x.Regions[RegionNames.MainBody]).Returns(mockMainRegion.Object);
            Mock<IUserService> mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Online);

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            MenubarViewModel target = new MenubarViewModel(mockRegionManager.Object, mockedEventAggregator.Object, mockUserService.Object);

            //Act
            target.ShowGamesViewCommand.Execute(null);

            //Verify
            mockMainRegion.Verify(x => x.RequestNavigate(new Uri("GamesView", UriKind.Relative), It.IsAny<Action<NavigationResult>>()), Times.Once());
        }

        [TestMethod]
        public void WhenShowStoreViewCommandExecuted_NavigatesToStoreView()
        {
            //Prepare
            Mock<IRegion> mockMainRegion = new Mock<IRegion>();
            mockMainRegion.Setup(x => x.RequestNavigate(new Uri("StoreView", UriKind.Relative), It.IsAny<Action<NavigationResult>>())).Verifiable();

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            mockRegionManager.Setup(x => x.Regions[RegionNames.MainBody]).Returns(mockMainRegion.Object);
            Mock<IUserService> mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Online);

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            MenubarViewModel target = new MenubarViewModel(mockRegionManager.Object, mockedEventAggregator.Object, mockUserService.Object);


            //Act
            target.ShowStoreViewCommand.Execute(null);

            //Verify
            mockMainRegion.Verify(x => x.RequestNavigate(new Uri("StoreView", UriKind.Relative), It.IsAny<Action<NavigationResult>>()), Times.Once());
        }

        [TestMethod]
        public void WhenLogoutCommandExecuted_UserLoggedOut()
        {
            //Prepare
            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IUserService> mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.Logout()).Verifiable();
            mockUserService.Setup(x => x.IsLoggedIn).Returns(false);
            mockUserService.Setup(x => x.OnlineStatus).Returns(OnlineStatuses.Offline);

            MockLoginStatusChangedEvent mockedLoginStatusChangedEvent = new MockLoginStatusChangedEvent();
            Mock<IEventAggregator> mockedEventAggregator = new Mock<IEventAggregator>();
            mockedEventAggregator.Setup(x => x.GetEvent<LoginStatusChangedEvent>()).Returns(mockedLoginStatusChangedEvent);

            MenubarViewModel target = new MenubarViewModel(mockRegionManager.Object, mockedEventAggregator.Object, mockUserService.Object);

            //Act
            target.LogoutCommand.Execute(null);

            //Verify
            mockUserService.Verify(x => x.Logout(), Times.Once);
            Assert.AreEqual(OnlineStatuses.Offline, target.OnlineStatus);

        }

        private class MockLoginStatusChangedEvent : LoginStatusChangedEvent
        {
            public Action<IUserService> SubscribeArgumentAction;
            public Predicate<IUserService> SubscribeArgumentFilter;
            public ThreadOption SubscribeArgumentThreadOption;

            public override SubscriptionToken Subscribe(Action<IUserService> action,
                ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<IUserService> filter)
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
