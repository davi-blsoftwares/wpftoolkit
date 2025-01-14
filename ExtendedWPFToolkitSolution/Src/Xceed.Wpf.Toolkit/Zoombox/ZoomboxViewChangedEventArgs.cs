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
using Xceed.Wpf.Toolkit.Core;

namespace Xceed.Wpf.Toolkit.Zoombox
{
    public class ZoomboxViewChangedEventArgs : PropertyChangedEventArgs<ZoomboxView>
    {
        #region Private Fields

        private readonly int _newViewStackIndex = -1;

        private readonly int _oldViewStackIndex = -1;

        #endregion Private Fields

        #region Public Properties

        public bool IsNewViewFromStack
        {
            get
            {
                return _newViewStackIndex >= 0;
            }
        }

        public bool IsOldViewFromStack
        {
            get
            {
                return _oldViewStackIndex >= 0;
            }
        }

        public int NewViewStackIndex
        {
            get
            {
                return _newViewStackIndex;
            }
        }

        public int OldViewStackIndex
        {
            get
            {
                return _oldViewStackIndex;
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public ZoomboxViewChangedEventArgs(
                                                      ZoomboxView oldView,
      ZoomboxView newView,
      int oldViewStackIndex,
      int newViewStackIndex)
      : base(Zoombox.CurrentViewChangedEvent, oldView, newView)
        {
            _newViewStackIndex = newViewStackIndex;
            _oldViewStackIndex = oldViewStackIndex;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((ZoomboxViewChangedEventHandler)genericHandler)(genericTarget, this);
        }

        #endregion Protected Methods
    }
}