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
    public class ItemDeletingEventArgs : CancelRoutedEventArgs
    {
        #region Private Fields

        private object _item;

        #endregion Private Fields

        #region Public Properties

        public object Item
        {
            get
            {
                return _item;
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public ItemDeletingEventArgs(RoutedEvent itemDeletingEvent, object itemDeleting)
              : base(itemDeletingEvent)
        {
            _item = itemDeleting;
        }

        #endregion Public Constructors
    }
}