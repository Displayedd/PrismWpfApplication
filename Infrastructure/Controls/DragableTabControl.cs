using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismWpfApplication.Infrastructure.Controls
{
    /// <summary>
    /// Extends TabControl to use DragableTabItem instead of TabItem.
    /// </summary>
    public class DragableTabControl : TabControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DragableTabItem();
        }
    }
}
