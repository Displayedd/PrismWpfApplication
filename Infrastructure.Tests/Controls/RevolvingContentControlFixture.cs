using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Controls;
using System.ComponentModel;
using Moq;
using System.Collections.Generic;
using System.Windows.Input;

namespace Infrastructure.Tests.Controls
{
    [TestClass]
    public class RevolvingContentControlFixture
    {
        [TestMethod]
        public void WhenConstructed_InitialisValues()
        {
            //Prepare

            //Act
            RevolvingContentControl target = new RevolvingContentControl();

            //Verify
            Assert.AreEqual(1, target.Interval);
            Assert.IsFalse(target.Pause);
            Assert.IsNull(target.ItemsSource);
            Assert.IsNull(target.Items);
        }

        [TestMethod]
        public void WhenPauseChanged_ValueUpdated()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();
            bool delegateCalled = false;
            DependencyPropertyDescriptor.FromProperty(RevolvingContentControl.PauseProperty,
                typeof(RevolvingContentControl)).AddValueChanged(target, delegate { delegateCalled = true; });

            //Act
            target.Pause = true;

            //Verify
            Assert.IsTrue(delegateCalled);
            Assert.IsTrue(target.Pause);
        }

        [TestMethod]
        public void WhenIntervalChanged_ValueUpdated()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();
            bool delegateCalled = false;
            DependencyPropertyDescriptor.FromProperty(RevolvingContentControl.IntervalProperty,
                typeof(RevolvingContentControl)).AddValueChanged(target, delegate { delegateCalled = true; });

            //Act
            target.Interval = 2;

            //Verify
            Assert.IsTrue(delegateCalled);
            Assert.AreEqual(2, target.Interval);
        }

        [TestMethod]
        public void WhenItemsSourceChanged_ValueUpdated()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();
            bool delegateCalled = false;
            List<object> items = new List<object>();
            DependencyPropertyDescriptor.FromProperty(RevolvingContentControl.ItemsSourceProperty,
                typeof(RevolvingContentControl)).AddValueChanged(target, delegate { delegateCalled = true; });

            //Act
            target.ItemsSource = items;

            //Verify
            Assert.IsTrue(delegateCalled);
            Assert.AreEqual(items, target.ItemsSource);
        }

        [TestMethod]
        public void WhenNegativeIntervalValue_ValueNegated()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();

            //Act
            target.Interval = -2;

            //Verify
            Assert.AreEqual(2, target.Interval);
        }

        [TestMethod]
        public void WhenItemsSourceSet_ContentUpdated()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();
            object firstItem = new object();
            object secondItem = new object();
            List<object> items = new List<object> { firstItem, secondItem };

            //Act
            target.ItemsSource = items;

            //Verify
            Assert.AreEqual(firstItem, target.Content);
        }

        [TestMethod]
        public void WhenItemsSourceIsNull_ContentIsNull()
        {
            //Prepare
            RevolvingContentControl target = new RevolvingContentControl();
            object firstItem = new object();
            object secondItem = new object();
            List<object> items = new List<object> { firstItem, secondItem };
            target.ItemsSource = items;

            //Act
            target.ItemsSource = null;

            //Verify
            Assert.IsNull(target.Content);
        }
    }
}
