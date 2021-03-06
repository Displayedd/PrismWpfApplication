﻿/*
 * Based on a solution posted to stackoverflow web site to apply effects
 * such as blur to objects behind a container but are not in the visual tree
 * of the container.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace PrismWpfApplication.Infrastructure.Behaviors
{
    public class BackgroundEffectBehavior : Behavior<Shape>
    {
        public static readonly DependencyProperty BackgroundContainerProperty
            = DependencyProperty.Register(
                                          "BackgroundContainer",
                                          typeof(UIElement),
                                          typeof(BackgroundEffectBehavior),
                                          new PropertyMetadata(OnContainerChanged));

        public static readonly DependencyProperty EffectProperty
            = DependencyProperty.Register(
                                  "Effect",
                                  typeof(Effect),
                                  typeof(BackgroundEffectBehavior),
                                  new PropertyMetadata());

        private static readonly DependencyProperty BrushProperty
            = DependencyProperty.Register(
                                          "Brush",
                                          typeof(VisualBrush),
                                          typeof(BackgroundEffectBehavior),
                                          new PropertyMetadata());

        private VisualBrush Brush
        {
            get { return (VisualBrush)this.GetValue(BrushProperty); }
            set { this.SetValue(BrushProperty, value); }
        }

        public UIElement BackgroundContainer
        {
            get { return (UIElement)this.GetValue(BackgroundContainerProperty); }
            set { this.SetValue(BackgroundContainerProperty, value); }
        }

        public Effect Effect
        {
            get { return (Effect)this.GetValue(EffectProperty); }
            set { this.SetValue(EffectProperty, value); }
        }

        protected override void OnAttached()
        {
            this.AssociatedObject.Effect = Effect;

            this.AssociatedObject.SetBinding(Shape.FillProperty,
                                             new Binding
                                             {
                                                 Source = this,
                                                 Path = new PropertyPath(BrushProperty)
                                             });

            this.AssociatedObject.LayoutUpdated += (sender, args) => this.UpdateBounds();
            this.UpdateBounds();
        }

        protected override void OnDetaching()
        {
            BindingOperations.ClearBinding(this.AssociatedObject, Border.BackgroundProperty);
        }

        private static void OnContainerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BackgroundEffectBehavior)d).OnContainerChanged((UIElement)e.OldValue, (UIElement)e.NewValue);
        }

        private void OnContainerChanged(UIElement oldValue, UIElement newValue)
        {
            if (oldValue != null)
            {
                oldValue.LayoutUpdated -= this.OnContainerLayoutUpdated;
            }

            if (newValue != null)
            {
                this.Brush = new VisualBrush(newValue)
                {
                    ViewboxUnits = BrushMappingMode.Absolute
                };

                newValue.LayoutUpdated += this.OnContainerLayoutUpdated;
                this.UpdateBounds();
            }
            else
            {
                this.Brush = null;
            }
        }

        private void OnContainerLayoutUpdated(object sender, EventArgs eventArgs)
        {
            this.UpdateBounds();
        }

        private void UpdateBounds()
        {
            if (this.AssociatedObject != null && this.BackgroundContainer != null && this.Brush != null)
            {
                Point difference = this.AssociatedObject.TranslatePoint(new Point(), this.BackgroundContainer);
                this.Brush.Viewbox = new Rect(difference, this.AssociatedObject.RenderSize);
            }
        }
    }
}