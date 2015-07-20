using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace PrismWpfApplication.Infrastructure.Controls
{
    /// <summary>
    /// This class extends ContentControl with the
    /// functionality to revolve the content based
    /// on a set collection of items. 
    /// </summary>
    public class RevolvingContentControl : ContentControl
    {
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(RevolvingContentControl),
            new PropertyMetadata(1, IntervalCallBack, CoerceInterval));

        public static readonly DependencyProperty PauseProperty =
            DependencyProperty.Register("Pause", typeof(bool), typeof(RevolvingContentControl),
            new PropertyMetadata(false));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(RevolvingContentControl),
            new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        private DispatcherTimer timer;

        /// <summary>
        /// Read only field containing the revolving items.
        /// </summary>
        public IList<object> Items { get; private set; }

        public RevolvingContentControl()
        {
            timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = new TimeSpan(0, 0, this.Interval);
            timer.Tick += new EventHandler(this.RevolveContent);            
            timer.Start();
        }

        static RevolvingContentControl()
        {
            CommandManager.RegisterClassCommandBinding(typeof(RevolvingContentControl),
                new CommandBinding(RevolvingContentControl.ForwardCommand, OnForwardCommand, CanExecuteForwardCommand));
            CommandManager.RegisterClassCommandBinding(typeof(RevolvingContentControl),
                new CommandBinding(RevolvingContentControl.RewindCommand, OnRewindCommand, CanExecuteRewindCommand));
        }

        /// <summary>
        /// Gets or sets a collection used to generate the revolving content of the <see cref="RevolvingContentControl"/>.
        /// </summary>
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Changes the currently assigned Content item
        /// to the next item in the ItemsSource collection.
        /// </summary>
        public static readonly ICommand ForwardCommand = new RoutedUICommand("ForwardCommand", "ForwardCommand",
                                                        typeof(RevolvingContentControl));
       
        /// <summary>
        /// Changes the currently assigned Content item
        /// to the previous item in the ItemsSource collection.
        /// </summary>
        public static readonly ICommand RewindCommand = new RoutedUICommand("RewindCommand", "RewindCommand",
                                                        typeof(RevolvingContentControl));

        /// <summary>
        /// Gets or sets the pause state of the control. When set to 
        /// true halts the revolving of items assigned to the ItemsSource.
        /// Default value is false.
        /// </summary>
        public bool Pause
        {
            get { return (bool)GetValue(PauseProperty); }
            set { SetValue(PauseProperty, value); }
        }

        /// <summary>
        /// Gets or sets the time in seconds between 
        /// revolutions of the item assigned to the Content field. 
        /// Default value is 1.
        /// </summary>
        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        static void CanExecuteForwardCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            RevolvingContentControl invoker = sender as RevolvingContentControl;

            if (invoker == null || invoker.Items == null || invoker.Items.Count == 0)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        static void OnForwardCommand(object sender, ExecutedRoutedEventArgs e)
        {
            RevolvingContentControl invoker = sender as RevolvingContentControl;
                        
            if (invoker == null || invoker.Items == null || invoker.Items.Count == 0)
                return;

            invoker.timer.Stop();

            int currentIndex = invoker.Items.IndexOf(invoker.Content);
            if (currentIndex + 1 < invoker.Items.Count)
                invoker.Content = invoker.Items[currentIndex + 1];
            else
                invoker.Content = invoker.Items.First();

            invoker.timer.Start();
        }

        static void CanExecuteRewindCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            RevolvingContentControl invoker = sender as RevolvingContentControl;

            if (invoker == null || invoker.Items == null || invoker.Items.Count == 0)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        static void OnRewindCommand(object sender, ExecutedRoutedEventArgs e)
        {
            RevolvingContentControl invoker = sender as RevolvingContentControl;

            if (invoker == null || invoker.Items == null || invoker.Items.Count == 0)
                return;

            invoker.timer.Stop();

            int currentIndex = invoker.Items.IndexOf(invoker.Content);
            if (currentIndex - 1 < 0)
                invoker.Content = invoker.Items.Last();
            else
                invoker.Content = invoker.Items[currentIndex - 1];

            invoker.timer.Start();
        }

        static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RevolvingContentControl;
            if (control != null)
                control.OnItemsSourceChanged((IEnumerable<object>)e.OldValue, (IEnumerable<object>)e.NewValue);
        }

        static Object CoerceInterval(DependencyObject d, Object baseValue)
        {
            int value = (int)baseValue;

            if (value < 0)
                return value * -1;
            else
                return value;
        }

        static void IntervalCallBack(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            RevolvingContentControl control = d as RevolvingContentControl;
            if (control.timer != null)
            {
                control.timer.Stop();
                control.timer.Interval = new TimeSpan(0, 0, control.Interval);
                control.timer.Start();
            }
            else
            {
                control.timer = new DispatcherTimer(DispatcherPriority.Background);
                control.timer.Interval = new TimeSpan(0, 0, control.Interval);
                control.timer.Tick += new EventHandler(control.RevolveContent);
            }
        }      

        protected virtual void RevolveContent(object sender, EventArgs e)
        {
            if (this.Items == null || this.Items.Count == 0 || this.Pause)
                return;
            int currentIndex = this.Items.IndexOf(this.Content);
            if (currentIndex + 1 < this.Items.Count)
                this.Content = this.Items[currentIndex + 1];
            else if (this.Items.Count > 0)
                this.Content = this.Items.First();
        }

        private void OnItemsSourceChanged(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            if (newValue != null)
            {
                this.Items = newValue.ToList();
                if (this.Items != null && this.Items.Count > 0)
                    this.Content = this.Items.First();
            }
            else
            {
                this.Items = null;
                this.Content = null;
            }           
        }
    }
}
