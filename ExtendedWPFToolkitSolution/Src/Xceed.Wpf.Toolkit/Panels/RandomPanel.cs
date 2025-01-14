/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit.Panels
{
    public class RandomPanel : AnimationPanel
    {
        #region Public Fields

        public static readonly DependencyProperty MaximumHeightProperty =
          DependencyProperty.Register("MaximumHeight", typeof(double), typeof(RandomPanel),
            new FrameworkPropertyMetadata(
              100d,
              new PropertyChangedCallback(RandomPanel.OnMaximumHeightChanged),
              new CoerceValueCallback(RandomPanel.CoerceMaximumHeight)));

        public static readonly DependencyProperty MaximumWidthProperty =
      DependencyProperty.Register("MaximumWidth", typeof(double), typeof(RandomPanel),
        new FrameworkPropertyMetadata(
          100d,
          new PropertyChangedCallback(RandomPanel.OnMaximumWidthChanged),
          new CoerceValueCallback(RandomPanel.CoerceMaximumWidth)));

        public static readonly DependencyProperty MinimumHeightProperty =
      DependencyProperty.Register("MinimumHeight", typeof(double), typeof(RandomPanel),
        new FrameworkPropertyMetadata(
          10d,
          new PropertyChangedCallback(RandomPanel.OnMinimumHeightChanged),
          new CoerceValueCallback(RandomPanel.CoerceMinimumHeight)));

        public static readonly DependencyProperty MinimumWidthProperty =
                              DependencyProperty.Register("MinimumWidth", typeof(double), typeof(RandomPanel),
        new FrameworkPropertyMetadata(
          10d,
          new PropertyChangedCallback(RandomPanel.OnMinimumWidthChanged),
          new CoerceValueCallback(RandomPanel.CoerceMinimumWidth)));

        public static readonly DependencyProperty SeedProperty =
          DependencyProperty.Register("Seed", typeof(int), typeof(RandomPanel),
            new FrameworkPropertyMetadata(0,
              new PropertyChangedCallback(RandomPanel.SeedChanged)));

        #endregion Public Fields

        #region Private Fields

        // Using a DependencyProperty as the backing store for ActualSize.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ActualSizeProperty =
          DependencyProperty.RegisterAttached("ActualSize", typeof(Size), typeof(RandomPanel),
            new UIPropertyMetadata(new Size()));

        private Random _random = new Random();

        #endregion Private Fields

        #region Public Properties

        public double MaximumHeight
        {
            get
            {
                return (double)this.GetValue(RandomPanel.MaximumHeightProperty);
            }
            set
            {
                this.SetValue(RandomPanel.MaximumHeightProperty, value);
            }
        }

        public double MaximumWidth
        {
            get
            {
                return (double)this.GetValue(RandomPanel.MaximumWidthProperty);
            }
            set
            {
                this.SetValue(RandomPanel.MaximumWidthProperty, value);
            }
        }

        public double MinimumHeight
        {
            get
            {
                return (double)this.GetValue(RandomPanel.MinimumHeightProperty);
            }
            set
            {
                this.SetValue(RandomPanel.MinimumHeightProperty, value);
            }
        }

        public double MinimumWidth
        {
            get
            {
                return (double)this.GetValue(RandomPanel.MinimumWidthProperty);
            }
            set
            {
                this.SetValue(RandomPanel.MinimumWidthProperty, value);
            }
        }

        public int Seed
        {
            get
            {
                return (int)this.GetValue(RandomPanel.SeedProperty);
            }
            set
            {
                this.SetValue(RandomPanel.SeedProperty, value);
            }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override Size ArrangeChildrenOverride(UIElementCollection children, Size finalSize)
        {
            foreach (UIElement child in children)
            {
                if (child == null)
                    continue;

                Size childSize = RandomPanel.GetActualSize(child);

                double x = _random.Next(0, (int)(Math.Max(finalSize.Width - childSize.Width, 0)));
                double y = _random.Next(0, (int)(Math.Max(finalSize.Height - childSize.Height, 0)));

                double width = Math.Min(finalSize.Width, childSize.Width);
                double height = Math.Min(finalSize.Height, childSize.Height);

                this.ArrangeChild(child, new Rect(new Point(x, y), new Size(width, height)));
            }
            return finalSize;
        }

        protected override Size MeasureChildrenOverride(UIElementCollection children, Size constraint)
        {
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (UIElement child in children)
            {
                if (child == null)
                    continue;

                Size childSize = new Size(1d * _random.Next(Convert.ToInt32(MinimumWidth), Convert.ToInt32(MaximumWidth)),
                                           1d * _random.Next(Convert.ToInt32(MinimumHeight), Convert.ToInt32(MaximumHeight)));

                child.Measure(childSize);
                RandomPanel.SetActualSize(child, childSize);
            }
            return new Size();
        }

        #endregion Protected Methods

        #region Private Methods

        private static object CoerceMaximumHeight(DependencyObject d, object baseValue)
        {
            RandomPanel panel = (RandomPanel)d;
            double value = (double)baseValue;

            if (double.IsNaN(value) || double.IsInfinity(value) || (value < 0d))
                return DependencyProperty.UnsetValue;

            double minimum = panel.MinimumHeight;
            if (value < minimum)
                return minimum;

            return baseValue;
        }

        private static object CoerceMaximumWidth(DependencyObject d, object baseValue)
        {
            RandomPanel panel = (RandomPanel)d;
            double value = (double)baseValue;

            if (double.IsNaN(value) || double.IsInfinity(value) || (value < 0d))
                return DependencyProperty.UnsetValue;

            double minimum = panel.MinimumWidth;
            if (value < minimum)
                return minimum;

            return baseValue;
        }

        private static object CoerceMinimumHeight(DependencyObject d, object baseValue)
        {
            RandomPanel panel = (RandomPanel)d;
            double value = (double)baseValue;

            if (double.IsNaN(value) || double.IsInfinity(value) || (value < 0d))
                return DependencyProperty.UnsetValue;

            double maximum = panel.MaximumHeight;
            if (value > maximum)
                return maximum;

            return baseValue;
        }

        private static object CoerceMinimumWidth(DependencyObject d, object baseValue)
        {
            RandomPanel panel = (RandomPanel)d;
            double value = (double)baseValue;

            if (double.IsNaN(value) || double.IsInfinity(value) || (value < 0d))
                return DependencyProperty.UnsetValue;

            double maximum = panel.MaximumWidth;
            if (value > maximum)
                return maximum;

            return baseValue;
        }

        private static Size GetActualSize(DependencyObject obj)
        {
            return (Size)obj.GetValue(RandomPanel.ActualSizeProperty);
        }

        private static void OnMaximumHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RandomPanel panel = (RandomPanel)d;

            panel.CoerceValue(RandomPanel.MinimumHeightProperty);
            panel.InvalidateMeasure();
        }

        private static void OnMaximumWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RandomPanel panel = (RandomPanel)d;

            panel.CoerceValue(RandomPanel.MinimumWidthProperty);
            panel.InvalidateMeasure();
        }

        private static void OnMinimumHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RandomPanel panel = (RandomPanel)d;

            panel.CoerceValue(RandomPanel.MaximumHeightProperty);
            panel.InvalidateMeasure();
        }

        private static void OnMinimumWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RandomPanel panel = (RandomPanel)d;

            panel.CoerceValue(RandomPanel.MaximumWidthProperty);
            panel.InvalidateMeasure();
        }

        private static void SeedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is RandomPanel)
            {
                RandomPanel owner = (RandomPanel)obj;
                owner._random = new Random((int)args.NewValue);
                owner.InvalidateArrange();
            }
        }

        private static void SetActualSize(DependencyObject obj, Size value)
        {
            obj.SetValue(RandomPanel.ActualSizeProperty, value);
        }

        #endregion Private Methods
    }
}