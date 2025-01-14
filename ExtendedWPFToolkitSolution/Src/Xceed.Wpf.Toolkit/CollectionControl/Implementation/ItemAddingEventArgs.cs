/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;
using Xceed.Wpf.Toolkit.Core;

namespace Xceed.Wpf.Toolkit
{
    public class ItemAddingEventArgs : CancelRoutedEventArgs
    {
        #region Public Properties

        public object Item
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Constructors

        public ItemAddingEventArgs(RoutedEvent itemAddingEvent, object itemAdding)
              : base(itemAddingEvent)
        {
            Item = itemAdding;
        }

        #endregion Public Constructors
    }
}