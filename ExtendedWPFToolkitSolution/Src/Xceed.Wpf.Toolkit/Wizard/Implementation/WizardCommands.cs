/*************************************************************************************

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
    public static class WizardCommands
    {
        #region Private Fields

        private static RoutedCommand _cancelCommand = new RoutedCommand();
        private static RoutedCommand _finishCommand = new RoutedCommand();

        private static RoutedCommand _helpCommand = new RoutedCommand();

        private static RoutedCommand _nextPageCommand = new RoutedCommand();

        private static RoutedCommand _previousPageCommand = new RoutedCommand();

        private static RoutedCommand _selectPageCommand = new RoutedCommand();

        #endregion Private Fields

        #region Public Properties

        public static RoutedCommand Cancel
        {
            get
            {
                return _cancelCommand;
            }
        }

        public static RoutedCommand Finish
        {
            get
            {
                return _finishCommand;
            }
        }

        public static RoutedCommand Help
        {
            get
            {
                return _helpCommand;
            }
        }

        public static RoutedCommand NextPage
        {
            get
            {
                return _nextPageCommand;
            }
        }

        public static RoutedCommand PreviousPage
        {
            get
            {
                return _previousPageCommand;
            }
        }

        public static RoutedCommand SelectPage
        {
            get
            {
                return _selectPageCommand;
            }
        }

        #endregion Public Properties
    }
}