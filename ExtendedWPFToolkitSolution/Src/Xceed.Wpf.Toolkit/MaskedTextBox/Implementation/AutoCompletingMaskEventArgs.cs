/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.ComponentModel;

namespace Xceed.Wpf.Toolkit
{
    public class AutoCompletingMaskEventArgs : CancelEventArgs
    {
        #region Private Fields

        private int m_autoCompleteStartPosition;

        private string m_autoCompleteText;

        private string m_input;

        private MaskedTextProvider m_maskedTextProvider;

        private int m_selectionLength;

        private int m_startPosition;

        #endregion Private Fields

        #region Public Properties

        public int AutoCompleteStartPosition
        {
            get { return m_autoCompleteStartPosition; }
            set { m_autoCompleteStartPosition = value; }
        }

        public string AutoCompleteText
        {
            get { return m_autoCompleteText; }
            set { m_autoCompleteText = value; }
        }

        public string Input
        {
            get { return m_input; }
        }

        public MaskedTextProvider MaskedTextProvider
        {
            get { return m_maskedTextProvider; }
        }

        public int SelectionLength
        {
            get { return m_selectionLength; }
        }

        public int StartPosition
        {
            get { return m_startPosition; }
        }

        #endregion Public Properties

        #region Public Constructors

        public AutoCompletingMaskEventArgs(MaskedTextProvider maskedTextProvider, int startPosition, int selectionLength, string input)
        {
            m_autoCompleteStartPosition = -1;

            m_maskedTextProvider = maskedTextProvider;
            m_startPosition = startPosition;
            m_selectionLength = selectionLength;
            m_input = input;
        }

        #endregion Public Constructors
    }
}