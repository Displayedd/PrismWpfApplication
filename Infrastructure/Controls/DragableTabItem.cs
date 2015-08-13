using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PrismWpfApplication.Infrastructure.Controls
{
    /// <summary>
    /// Extends TabItem to allow for drag and drop.
    /// </summary>
    public class DragableTabItem : TabItem
    {
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            var tabItem = e.Source as DragableTabItem;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }


        protected override void OnDrop(DragEventArgs e)
        {
            var tabItemTarget = e.Source as DragableTabItem;

            var tabItemSource = e.Data.GetData(typeof(DragableTabItem)) as DragableTabItem;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabPanel = tabItemTarget.FindCommonVisualAncestor(tabItemSource) as TabPanel;
                int sourceIndex = tabPanel.Children.IndexOf(tabItemSource);
                int targetIndex = tabPanel.Children.IndexOf(tabItemTarget);

                var tabControl = tabPanel.TemplatedParent as TabControl;

                var items = tabControl.ItemsSource as IList;

                var sourceTemp = items[sourceIndex];
                var targetTemp = items[targetIndex];

                items[targetIndex] = sourceTemp;
                tabControl.SelectedIndex = targetIndex;
                items[sourceIndex] = targetTemp;
            }
        }
    }
}
