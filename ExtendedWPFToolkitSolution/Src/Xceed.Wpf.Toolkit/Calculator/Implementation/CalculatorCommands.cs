﻿/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows.Input;

namespace Xceed.Wpf.Toolkit
{
    public static class CalculatorCommands
    {
        #region Private Fields

        private static RoutedCommand _calculatorButtonClickCommand = new RoutedCommand();

        #endregion Private Fields

        #region Public Properties

        public static RoutedCommand CalculatorButtonClick
        {
            get
            {
                return _calculatorButtonClickCommand;
            }
        }

        #endregion Public Properties
    }
}