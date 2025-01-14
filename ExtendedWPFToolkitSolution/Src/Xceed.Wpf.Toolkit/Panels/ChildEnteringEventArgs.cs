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

namespace Xceed.Wpf.Toolkit.Panels
{
    public class ChildEnteringEventArgs : RoutedEventArgs
    {
        #region Private Fields

        private readonly Rect _arrangeRect;

        private readonly UIElement _child;

        private Rect? _enterFrom;

        #endregion Private Fields

        #region Public Properties

        public Rect ArrangeRect
        {
            get
            {
                return _arrangeRect;
            }
        }

        public UIElement Child
        {
            get
            {
                return _child;
            }
        }

        public Rect? EnterFrom
        {
            get
            {
                return _enterFrom;
            }
            set
            {
                _enterFrom = value;
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public ChildEnteringEventArgs(UIElement child, Rect? enterFrom, Rect arrangeRect)
        {
            _child = child;
            _enterFrom = enterFrom;
            _arrangeRect = arrangeRect;
        }

        #endregion Public Constructors

        //null
    }
}