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

namespace Xceed.Wpf.Toolkit
{
    public class DoubleUpDown : CommonNumericUpDown<double>
    {
        #region Public Fields

        public static readonly DependencyProperty AllowInputSpecialValuesProperty =
            DependencyProperty.Register("AllowInputSpecialValues", typeof(AllowedSpecialValues), typeof(DoubleUpDown), new UIPropertyMetadata(AllowedSpecialValues.None));

        #endregion Public Fields

        #region Public Properties

        public AllowedSpecialValues AllowInputSpecialValues
        {
            get { return (AllowedSpecialValues)GetValue(AllowInputSpecialValuesProperty); }
            set { SetValue(AllowInputSpecialValuesProperty, value); }
        }

        #endregion Public Properties

        #region Public Constructors

        static DoubleUpDown()
        {
            UpdateMetadata(typeof(DoubleUpDown), 1d, double.NegativeInfinity, double.PositiveInfinity);
        }

        public DoubleUpDown()
          : base(Double.TryParse, Decimal.ToDouble, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override double? ConvertTextToValue(string text)
        {
            double? result = base.ConvertTextToValue(text);
            if (result != null)
            {
                if (double.IsNaN(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.NaN);
                else if (double.IsPositiveInfinity(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.PositiveInfinity);
                else if (double.IsNegativeInfinity(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.NegativeInfinity);
            }

            return result;
        }

        protected override double DecrementValue(double value, double increment)
        {
            return value - increment;
        }

        protected override double IncrementValue(double value, double increment)
        {
            return value + increment;
        }

        protected override double? OnCoerceIncrement(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Increment.");

            return base.OnCoerceIncrement(baseValue);
        }

        protected override double? OnCoerceMaximum(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Maximum.");

            return base.OnCoerceMaximum(baseValue);
        }

        protected override double? OnCoerceMinimum(double? baseValue)
        {
            if (baseValue.HasValue && double.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Minimum.");

            return base.OnCoerceMinimum(baseValue);
        }

        protected override void SetValidSpinDirection()
        {
            if (Value.HasValue && double.IsInfinity(Value.Value) && (Spinner != null))
            {
                Spinner.ValidSpinDirection = ValidSpinDirections.None;
            }
            else
            {
                base.SetValidSpinDirection();
            }
        }

        #endregion Protected Methods
    }
}