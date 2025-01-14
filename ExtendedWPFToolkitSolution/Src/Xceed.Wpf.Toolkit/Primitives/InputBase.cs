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
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.Primitives
{
    public abstract class InputBase : Control
    {
        #region Public Fields

        public static readonly DependencyProperty AllowTextInputProperty = DependencyProperty.Register("AllowTextInput", typeof(bool), typeof(InputBase), new UIPropertyMetadata(true, OnAllowTextInputChanged));
        public static readonly DependencyProperty CultureInfoProperty = DependencyProperty.Register("CultureInfo", typeof(CultureInfo), typeof(InputBase), new UIPropertyMetadata(CultureInfo.CurrentCulture, OnCultureInfoChanged));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(InputBase), new UIPropertyMetadata(false, OnReadOnlyChanged));

        public static readonly DependencyProperty IsUndoEnabledProperty = DependencyProperty.Register("IsUndoEnabled", typeof(bool), typeof(InputBase), new UIPropertyMetadata(true, OnIsUndoEnabledChanged));

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(InputBase), new UIPropertyMetadata(TextAlignment.Left));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(InputBase), new FrameworkPropertyMetadata(default(String), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged, null, false, UpdateSourceTrigger.LostFocus));

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(object), typeof(InputBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty WatermarkTemplateProperty = DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(InputBase), new UIPropertyMetadata(null));

        #endregion Public Fields

        #region Public Properties

        public bool AllowTextInput
        {
            get
            {
                return (bool)GetValue(AllowTextInputProperty);
            }
            set
            {
                SetValue(AllowTextInputProperty, value);
            }
        }

        public CultureInfo CultureInfo
        {
            get
            {
                return (CultureInfo)GetValue(CultureInfoProperty);
            }
            set
            {
                SetValue(CultureInfoProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return (bool)GetValue(IsUndoEnabledProperty);
            }
            set
            {
                SetValue(IsUndoEnabledProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public TextAlignment TextAlignment
        {
            get
            {
                return (TextAlignment)GetValue(TextAlignmentProperty);
            }
            set
            {
                SetValue(TextAlignmentProperty, value);
            }
        }

        public object Watermark
        {
            get
            {
                return (object)GetValue(WatermarkProperty);
            }
            set
            {
                SetValue(WatermarkProperty, value);
            }
        }

        public DataTemplate WatermarkTemplate
        {
            get
            {
                return (DataTemplate)GetValue(WatermarkTemplateProperty);
            }
            set
            {
                SetValue(WatermarkTemplateProperty, value);
            }
        }

        #endregion Public Properties

        #region Protected Methods

        protected virtual void OnAllowTextInputChanged(bool oldValue, bool newValue)
        {
        }

        protected virtual void OnCultureInfoChanged(CultureInfo oldValue, CultureInfo newValue)
        {
        }

        protected virtual void OnIsUndoEnabledChanged(bool oldValue, bool newValue)
        {
        }

        protected virtual void OnReadOnlyChanged(bool oldValue, bool newValue)
        {
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnAllowTextInputChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            InputBase inputBase = o as InputBase;
            if (inputBase != null)
                inputBase.OnAllowTextInputChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnCultureInfoChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            InputBase inputBase = o as InputBase;
            if (inputBase != null)
                inputBase.OnCultureInfoChanged((CultureInfo)e.OldValue, (CultureInfo)e.NewValue);
        }

        private static void OnIsUndoEnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            InputBase inputBase = o as InputBase;
            if (inputBase != null)
                inputBase.OnIsUndoEnabledChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnReadOnlyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            InputBase inputBase = o as InputBase;
            if (inputBase != null)
                inputBase.OnReadOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            InputBase inputBase = o as InputBase;
            if (inputBase != null)
                inputBase.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        #endregion Private Methods
    }
}