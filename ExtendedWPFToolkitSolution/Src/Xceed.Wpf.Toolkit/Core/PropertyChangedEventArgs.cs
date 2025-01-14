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

namespace Xceed.Wpf.Toolkit.Core
{
    public class PropertyChangedEventArgs<T> : RoutedEventArgs
    {
        #region Private Fields

        private readonly T _newValue;

        private readonly T _oldValue;

        #endregion Private Fields

        #region Public Properties

        public T NewValue
        {
            get
            {
                return _newValue;
            }
        }

        public T OldValue
        {
            get
            {
                return _oldValue;
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public PropertyChangedEventArgs(RoutedEvent Event, T oldValue, T newValue)
                                      : base()
        {
            _oldValue = oldValue;
            _newValue = newValue;
            this.RoutedEvent = Event;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            PropertyChangedEventHandler<T> handler = (PropertyChangedEventHandler<T>)genericHandler;
            handler(genericTarget, this);
        }

        #endregion Protected Methods
    }
}