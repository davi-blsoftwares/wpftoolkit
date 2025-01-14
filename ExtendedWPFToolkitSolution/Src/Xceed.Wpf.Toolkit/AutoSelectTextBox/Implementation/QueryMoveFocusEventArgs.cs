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
using System.Windows.Input;

namespace Xceed.Wpf.Toolkit
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")]
    public delegate void QueryMoveFocusEventHandler(object sender, QueryMoveFocusEventArgs e);

    public class QueryMoveFocusEventArgs : RoutedEventArgs
    {
        #region Private Fields

        private bool m_canMove = true;

        private FocusNavigationDirection m_navigationDirection;

        private bool m_reachedMaxLength;

        #endregion Private Fields

        #region Public Properties

        public bool CanMoveFocus
        {
            get
            {
                return m_canMove;
            }
            set
            {
                m_canMove = value;
            }
        }

        public FocusNavigationDirection FocusNavigationDirection
        {
            get
            {
                return m_navigationDirection;
            }
        }

        public bool ReachedMaxLength
        {
            get
            {
                return m_reachedMaxLength;
            }
        }

        #endregion Public Properties

        #region Internal Constructors

        //internal to prevent anybody from building this type of event.
        internal QueryMoveFocusEventArgs(FocusNavigationDirection direction, bool reachedMaxLength)
          : base(AutoSelectTextBox.QueryMoveFocusEvent)
        {
            m_navigationDirection = direction;
            m_reachedMaxLength = reachedMaxLength;
        }

        #endregion Internal Constructors

        #region Private Constructors

        //default CTOR private to prevent its usage.
        private QueryMoveFocusEventArgs()
        {
        }

        #endregion Private Constructors

        //defaults to true... if nobody does nothing, then its capable of moving focus.
    }
}