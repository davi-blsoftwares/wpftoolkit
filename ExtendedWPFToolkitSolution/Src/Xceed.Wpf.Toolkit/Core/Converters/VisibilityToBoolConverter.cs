﻿/*************************************************************************************

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
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.Core.Converters
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        #region Private Fields

        private bool _inverted;

        private bool _not;

        #endregion Private Fields

        #region Public Properties

        public bool Inverted
        {
            get
            {
                return _inverted;
            }
            set
            {
                _inverted = value;
            }
        }

        //false

        public bool Not
        {
            get
            {
                return _not;
            }
            set
            {
                _not = value;
            }
        }

        #endregion Public Properties

        //false

        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Inverted ? this.BoolToVisibility(value) : this.VisibilityToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Inverted ? this.VisibilityToBool(value) : this.BoolToVisibility(value);
        }

        #endregion Public Methods

        #region Private Methods

        private object BoolToVisibility(object value)
        {
            if (!(value is bool))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotBool"));

            return ((bool)value ^ Not) ? Visibility.Visible : Visibility.Collapsed;
        }

        private object VisibilityToBool(object value)
        {
            if (!(value is Visibility))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotVisibility"));

            return (((Visibility)value) == Visibility.Visible) ^ Not;
        }

        #endregion Private Methods
    }
}