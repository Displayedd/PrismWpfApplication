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
    /// Extends WrapPanel to work better when wrapped by a scrollviewer
    /// along the axis of orientation.
    /// </summary>
    public class ScrollableWrapPanel : WrapPanel
    {

        protected override Size MeasureOverride(Size availableSize)
        {
            // Infinite space in both directions use base implementation.
            if (Double.IsInfinity(availableSize.Width) && Double.IsInfinity(availableSize.Height))
                return base.MeasureOverride(availableSize);
            else if (Orientation == Orientation.Vertical)
                return CalculateVerticalOrientation(availableSize);
            else
                return CalculateHorizontalOrientation(availableSize);
        }

        private Size CalculateHorizontalOrientation(Size availableSize)
        {
            int lines = 0;
            double w = availableSize.Width, h = availableSize.Height;
            double minWidth = base.MinWidth;
            double maxChildWidth = 0, maxChildHeight = 0, totalWidth = 0;

            // Calculate child element dimensions
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);

                maxChildWidth = Double.IsNaN(base.ItemWidth) ? Math.Max(child.DesiredSize.Width, maxChildWidth) : base.ItemWidth;
                totalWidth += Double.IsNaN(base.ItemWidth) ? child.DesiredSize.Width : base.ItemWidth;

                maxChildHeight = Double.IsNaN(base.ItemHeight) ? Math.Max(child.DesiredSize.Height, maxChildHeight) : base.ItemHeight;
            }

            lines = CalculateLines(h, maxChildHeight, minWidth, totalWidth);

            return new Size((totalWidth / lines), lines * maxChildHeight);
        }

        private Size CalculateVerticalOrientation(Size availableSize)
        {
            int lines = 0;
            double w = availableSize.Width, h = availableSize.Height;
            double minHeight = base.MinHeight;
            double maxChildWidth = 0, maxChildHeight = 0, totalHeight = 0;

            // Calculate child element dimensions
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);

                maxChildWidth = Double.IsNaN(base.ItemWidth) ? Math.Max(child.DesiredSize.Width, maxChildWidth) : base.ItemWidth;

                maxChildHeight = Double.IsNaN(base.ItemHeight) ? Math.Max(child.DesiredSize.Height, maxChildHeight) : base.ItemHeight;
                totalHeight += Double.IsNaN(base.ItemWidth) ? child.DesiredSize.Height : base.ItemHeight;
            }

            lines = CalculateLines(w, maxChildWidth, minHeight, totalHeight);

            return new Size(lines * maxChildWidth, totalHeight / lines);
        }

        private int CalculateLines(double size, double itemSize, double minLength, double totalLength)
        {
            int lines = 0;
            int sizeCount = int.MaxValue, lengthCount = int.MaxValue;

            // Check how many items can fit in submitted size
            if (!Double.IsInfinity(size) && !Double.IsInfinity(itemSize) && itemSize != 0)
            {
                try
                {
                    sizeCount = Convert.ToInt32(Math.Floor(size / itemSize));
                }
                catch (OverflowException)
                {
                    sizeCount = Children.Count;
                }
            }

            // Check how many lines of minimum length are required to fit all items.
            if (!Double.IsNaN(minLength))
            {
                try
                {
                    lengthCount = Convert.ToInt32(Math.Ceiling(totalLength / minLength));
                }
                catch (OverflowException)
                {
                    lengthCount = Children.Count;
                }
            }

            // Select the least required amount of lines.
            lines = Math.Min(sizeCount, lengthCount);

            return lines;
        }
    }
}
