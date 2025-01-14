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

namespace Xceed.Wpf.Toolkit.Core.Utilities
{
    internal class WeakEventListener<TArgs> : IWeakEventListener where TArgs : EventArgs
    {
        #region Private Fields

        private Action<object, TArgs> _callback;

        #endregion Private Fields

        #region Public Constructors

        public WeakEventListener(Action<object, TArgs> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            _callback = callback;
        }

        #endregion Public Constructors

        #region Public Methods

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            _callback(sender, (TArgs)e);
            return true;
        }

        #endregion Public Methods
    }
}