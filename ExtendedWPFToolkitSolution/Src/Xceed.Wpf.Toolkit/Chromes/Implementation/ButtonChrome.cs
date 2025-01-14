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

namespace Xceed.Wpf.Toolkit.Chromes
{
    public class ButtonChrome : ContentControl
    {
        #region Public Fields

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonChrome), new UIPropertyMetadata(default(CornerRadius), new PropertyChangedCallback(OnCornerRadiusChanged)));
        public static readonly DependencyProperty InnerCornerRadiusProperty = DependencyProperty.Register("InnerCornerRadius", typeof(CornerRadius), typeof(ButtonChrome), new UIPropertyMetadata(default(CornerRadius), new PropertyChangedCallback(OnInnerCornerRadiusChanged)));

        public static readonly DependencyProperty RenderCheckedProperty = DependencyProperty.Register("RenderChecked", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderCheckedChanged));

        public static readonly DependencyProperty RenderEnabledProperty = DependencyProperty.Register("RenderEnabled", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(true, OnRenderEnabledChanged));

        public static readonly DependencyProperty RenderFocusedProperty = DependencyProperty.Register("RenderFocused", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderFocusedChanged));

        public static readonly DependencyProperty RenderMouseOverProperty = DependencyProperty.Register("RenderMouseOver", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderMouseOverChanged));

        public static readonly DependencyProperty RenderNormalProperty = DependencyProperty.Register("RenderNormal", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(true, OnRenderNormalChanged));

        public static readonly DependencyProperty RenderPressedProperty = DependencyProperty.Register("RenderPressed", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderPressedChanged));

        #endregion Public Fields

        #region Public Properties

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        public CornerRadius InnerCornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(InnerCornerRadiusProperty);
            }
            set
            {
                SetValue(InnerCornerRadiusProperty, value);
            }
        }

        public bool RenderChecked
        {
            get
            {
                return (bool)GetValue(RenderCheckedProperty);
            }
            set
            {
                SetValue(RenderCheckedProperty, value);
            }
        }

        public bool RenderEnabled
        {
            get
            {
                return (bool)GetValue(RenderEnabledProperty);
            }
            set
            {
                SetValue(RenderEnabledProperty, value);
            }
        }

        public bool RenderFocused
        {
            get
            {
                return (bool)GetValue(RenderFocusedProperty);
            }
            set
            {
                SetValue(RenderFocusedProperty, value);
            }
        }

        public bool RenderMouseOver
        {
            get
            {
                return (bool)GetValue(RenderMouseOverProperty);
            }
            set
            {
                SetValue(RenderMouseOverProperty, value);
            }
        }

        public bool RenderNormal
        {
            get
            {
                return (bool)GetValue(RenderNormalProperty);
            }
            set
            {
                SetValue(RenderNormalProperty, value);
            }
        }

        public bool RenderPressed
        {
            get
            {
                return (bool)GetValue(RenderPressedProperty);
            }
            set
            {
                SetValue(RenderPressedProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static ButtonChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonChrome), new FrameworkPropertyMetadata(typeof(ButtonChrome)));
        }

        #endregion Public Constructors

        #region Protected Methods

        protected virtual void OnCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            //we always want the InnerBorderRadius to be one less than the CornerRadius
            CornerRadius newInnerCornerRadius = new CornerRadius(Math.Max(0, newValue.TopLeft - 1),
                                                                 Math.Max(0, newValue.TopRight - 1),
                                                                 Math.Max(0, newValue.BottomRight - 1),
                                                                 Math.Max(0, newValue.BottomLeft - 1));

            InnerCornerRadius = newInnerCornerRadius;
        }

        protected virtual void OnInnerCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderCheckedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderEnabledChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderFocusedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderMouseOverChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderNormalChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected virtual void OnRenderPressedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnCornerRadiusChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnCornerRadiusChanged((CornerRadius)e.OldValue, (CornerRadius)e.NewValue);
        }

        private static void OnInnerCornerRadiusChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnInnerCornerRadiusChanged((CornerRadius)e.OldValue, (CornerRadius)e.NewValue);
        }

        private static void OnRenderCheckedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderCheckedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnRenderEnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderEnabledChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnRenderFocusedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderFocusedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnRenderMouseOverChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderMouseOverChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnRenderNormalChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderNormalChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnRenderPressedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderPressedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        #endregion Private Methods
    }
}