using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismWpfApplication.Infrastructure.Behaviors
{
    public static class ReactiveSizeBehavior
    {
        public static double GetSize(DependencyObject obj)
        {
            return (double)obj.GetValue(SizeProperty);
        }

        public static void SetSize(DependencyObject target, double value)
        {
            target.SetValue(SizeProperty, value);
        }
        
        public static double GetSmall(DependencyObject obj)
        {
            return (double)obj.GetValue(SmallProperty);
        }

        public static void SetSmall(DependencyObject target, double value)
        {
            target.SetValue(SmallProperty, value);
        }

        public static double GetMedium(DependencyObject obj)
        {
            return (double)obj.GetValue(MediumProperty);
        }

        public static void SetMedium(DependencyObject target, double value)
        {
            target.SetValue(MediumProperty, value);
        }

        public static double GetLarge(DependencyObject obj)
        {
            return (double)obj.GetValue(LargeProperty);
        }

        public static void SetLarge(DependencyObject target, double value)
        {
            target.SetValue(LargeProperty, value);
        }

        
        public static DependencyProperty SizeProperty =
                                                  DependencyProperty.RegisterAttached("Size",
                                                  typeof(double),
                                                  typeof(ReactiveSizeBehavior),
                                                  new UIPropertyMetadata(0d, OnSizeChanged));

        public static DependencyProperty SmallProperty =
                                          DependencyProperty.RegisterAttached("Small",
                                          typeof(double),
                                          typeof(ReactiveSizeBehavior),
                                          new PropertyMetadata(0d));

        public static DependencyProperty MediumProperty =
                                  DependencyProperty.RegisterAttached("Medium",
                                  typeof(double),
                                  typeof(ReactiveSizeBehavior),
                                  new PropertyMetadata(0d));

        public static DependencyProperty LargeProperty =
                          DependencyProperty.RegisterAttached("Large",
                          typeof(double),
                          typeof(ReactiveSizeBehavior),
                          new PropertyMetadata(0d));

        private static void OnSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is double)
            {
                double size = (double) e.NewValue;
                if(size <= GetSmall(sender))
                    GotoState(sender, WindowSizes.SmallState);
                else if(size <= GetMedium(sender))
                    GotoState(sender, WindowSizes.MediumState);
                else
                    GotoState(sender, WindowSizes.LargeState);

            }
        }

        private static void GotoState(DependencyObject sender, string state)
        {
            if (sender is FrameworkElement)
                System.Windows.VisualStateManager.GoToElementState(sender as FrameworkElement, state, true);
            else if (sender is Control)
                System.Windows.VisualStateManager.GoToState(sender as Control, state, true);
        }
    }
}
