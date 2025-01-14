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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit
{
    [TemplatePart(Name = PART_CalculatorPopup, Type = typeof(Popup))]
    [TemplatePart(Name = PART_Calculator, Type = typeof(Calculator))]
    public class CalculatorUpDown : DecimalUpDown
    {
        #region Public Fields

        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText", typeof(string), typeof(CalculatorUpDown), new UIPropertyMetadata("0"));
        public static readonly DependencyProperty EnterClosesCalculatorProperty = DependencyProperty.Register("EnterClosesCalculator", typeof(bool), typeof(CalculatorUpDown), new UIPropertyMetadata(false));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(CalculatorUpDown), new UIPropertyMetadata(false, OnIsOpenChanged));
        public static readonly DependencyProperty MemoryProperty = DependencyProperty.Register("Memory", typeof(decimal), typeof(CalculatorUpDown), new UIPropertyMetadata(default(decimal)));
        public static readonly DependencyProperty PrecisionProperty = DependencyProperty.Register("Precision", typeof(int), typeof(CalculatorUpDown), new UIPropertyMetadata(6));

        #endregion Public Fields

        #region Private Fields

        private const string PART_Calculator = "PART_Calculator";
        private const string PART_CalculatorPopup = "PART_CalculatorPopup";
        private Calculator _calculator;
        private Popup _calculatorPopup;
        private Decimal? _initialValue;

        #endregion Private Fields

        #region Public Properties

        public string DisplayText
        {
            get
            {
                return (string)GetValue(DisplayTextProperty);
            }
            set
            {
                SetValue(DisplayTextProperty, value);
            }
        }

        public bool EnterClosesCalculator
        {
            get
            {
                return (bool)GetValue(EnterClosesCalculatorProperty);
            }
            set
            {
                SetValue(EnterClosesCalculatorProperty, value);
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

        public decimal Memory
        {
            get
            {
                return (decimal)GetValue(MemoryProperty);
            }
            set
            {
                SetValue(MemoryProperty, value);
            }
        }

        public int Precision
        {
            get
            {
                return (int)GetValue(PrecisionProperty);
            }
            set
            {
                SetValue(PrecisionProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static CalculatorUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalculatorUpDown), new FrameworkPropertyMetadata(typeof(CalculatorUpDown)));
        }

        public CalculatorUpDown()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_calculatorPopup != null)
                _calculatorPopup.Opened -= CalculatorPopup_Opened;

            _calculatorPopup = GetTemplateChild(PART_CalculatorPopup) as Popup;

            if (_calculatorPopup != null)
                _calculatorPopup.Opened += CalculatorPopup_Opened;

            if (_calculator != null)
                _calculator.ValueChanged -= OnCalculatorValueChanged;

            _calculator = GetTemplateChild(PART_Calculator) as Calculator;

            if (_calculator != null)
                _calculator.ValueChanged += OnCalculatorValueChanged;
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                _initialValue = this.UpdateValueOnEnterKey ? this.ConvertTextToValue(this.TextBox.Text) : this.Value;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            if (IsOpen && EnterClosesCalculator)
            {
                var buttonType = CalculatorUtilities.GetCalculatorButtonTypeFromText(e.Text);
                if (buttonType == Calculator.CalculatorButtonType.Equal)
                {
                    CloseCalculatorUpDown(true);
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CalculatorUpDown calculatorUpDown = o as CalculatorUpDown;
            if (calculatorUpDown != null)
                calculatorUpDown.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void CalculatorPopup_Opened(object sender, EventArgs e)
        {
            if (_calculator != null)
            {
                var initValue = this.UpdateValueOnEnterKey ? this.ConvertTextToValue(this.TextBox.Text) : this.Value;
                _calculator.InitializeToValue(initValue);
                _calculator.Focus();
            }
        }

        private void CloseCalculatorUpDown(bool isFocusOnTextBox)
        {
            if (IsOpen)
                IsOpen = false;
            ReleaseMouseCapture();

            if (isFocusOnTextBox && (TextBox != null))
                TextBox.Focus();
        }

        private void OnCalculatorValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_calculator != null)
            {
                if (this.IsBetweenMinMax(_calculator.Value))
                {
                    if (this.UpdateValueOnEnterKey)
                    {
                        this.TextBox.Text = (_calculator.Value != null) ? _calculator.Value.Value.ToString(this.FormatString, this.CultureInfo) : null;
                    }
                    else
                    {
                        this.Value = _calculator.Value;
                    }
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    // Calculator will get focus in CalculatorPopup_Opened().
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    CloseCalculatorUpDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    if (EnterClosesCalculator)
                    {
                        if (this.UpdateValueOnEnterKey)
                        {
                            this.TextBox.Text = (_initialValue != null) ? _initialValue.Value.ToString(this.FormatString, this.CultureInfo) : null;
                        }
                        else
                        {
                            this.Value = _initialValue;
                        }
                    }
                    CloseCalculatorUpDown(true);
                    e.Handled = true;
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseCalculatorUpDown(true);
        }

        #endregion Private Methods
    }
}