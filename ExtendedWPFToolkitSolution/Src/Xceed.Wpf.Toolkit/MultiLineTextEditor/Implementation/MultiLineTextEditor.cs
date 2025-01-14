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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit
{
    [TemplatePart(Name = PART_ResizeThumb, Type = typeof(Thumb))]
    [TemplatePart(Name = PART_TextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PART_DropDownButton, Type = typeof(ToggleButton))]
    public class MultiLineTextEditor : ContentControl
    {
        #region Public Fields

        public static readonly DependencyProperty DropDownHeightProperty = DependencyProperty.Register("DropDownHeight", typeof(double), typeof(MultiLineTextEditor), new UIPropertyMetadata(150.0));
        public static readonly DependencyProperty DropDownWidthProperty = DependencyProperty.Register("DropDownWidth", typeof(double), typeof(MultiLineTextEditor), new UIPropertyMetadata(200.0));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(MultiLineTextEditor), new UIPropertyMetadata(false, OnIsOpenChanged));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(MultiLineTextEditor), new UIPropertyMetadata(false));
        public static readonly DependencyProperty IsSpellCheckEnabledProperty = DependencyProperty.Register("IsSpellCheckEnabled", typeof(bool), typeof(MultiLineTextEditor), new UIPropertyMetadata(false));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(MultiLineTextEditor), new UIPropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MultiLineTextEditor), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(MultiLineTextEditor), new UIPropertyMetadata(TextWrapping.NoWrap));

        #endregion Public Fields

        #region Private Fields

        private const string PART_DropDownButton = "PART_DropDownButton";
        private const string PART_ResizeThumb = "PART_ResizeThumb";
        private const string PART_TextBox = "PART_TextBox";
        private Thumb _resizeThumb;
        private TextBox _textBox;
        private ToggleButton _toggleButton;

        #endregion Private Fields

        #region Public Properties

        public double DropDownHeight
        {
            get
            {
                return (double)GetValue(DropDownHeightProperty);
            }
            set
            {
                SetValue(DropDownHeightProperty, value);
            }
        }

        public double DropDownWidth
        {
            get
            {
                return (double)GetValue(DropDownWidthProperty);
            }
            set
            {
                SetValue(DropDownWidthProperty, value);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
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

        public bool IsSpellCheckEnabled
        {
            get
            {
                return (bool)GetValue(IsSpellCheckEnabledProperty);
            }
            set
            {
                SetValue(IsSpellCheckEnabledProperty, value);
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

        public TextWrapping TextWrapping
        {
            get
            {
                return (TextWrapping)GetValue(TextWrappingProperty);
            }
            set
            {
                SetValue(TextWrappingProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static MultiLineTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiLineTextEditor), new FrameworkPropertyMetadata(typeof(MultiLineTextEditor)));
        }

        public MultiLineTextEditor()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_resizeThumb != null)
                _resizeThumb.DragDelta -= ResizeThumb_DragDelta;

            _resizeThumb = GetTemplateChild(PART_ResizeThumb) as Thumb;

            if (_resizeThumb != null)
                _resizeThumb.DragDelta += ResizeThumb_DragDelta;

            _textBox = GetTemplateChild(PART_TextBox) as TextBox;
            _toggleButton = GetTemplateChild(PART_DropDownButton) as ToggleButton;
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (_textBox != null)
            {
                // Focus the content of the popup, after its loaded
                Dispatcher.BeginInvoke(new Action(() => _textBox.Focus()), DispatcherPriority.Background);
            }
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiLineTextEditor multiLineTextEditor = o as MultiLineTextEditor;
            if (multiLineTextEditor != null)
                multiLineTextEditor.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MultiLineTextEditor textEditor = o as MultiLineTextEditor;
            if (textEditor != null)
                textEditor.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void CloseEditor()
        {
            if (IsOpen)
                IsOpen = false;
            ReleaseMouseCapture();

            if (_toggleButton != null)
            {
                _toggleButton.Focus();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e)
                  || (e.Key == Key.Escape)
                  || (e.Key == Key.Tab))
                {
                    CloseEditor();
                    e.Handled = true;
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseEditor();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double yadjust = DropDownHeight + e.VerticalChange;
            double xadjust = DropDownWidth + e.HorizontalChange;

            if ((xadjust >= 0) && (yadjust >= 0))
            {
                DropDownWidth = xadjust;
                DropDownHeight = yadjust;
            }
        }

        #endregion Private Methods
    }
}