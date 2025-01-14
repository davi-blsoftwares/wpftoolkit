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
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit.Primitives
{
    internal class CachedTextInfo : ICloneable
    {
        #region Public Properties

        public int CaretIndex { get; private set; }

        public int SelectionLength { get; private set; }

        public int SelectionStart { get; private set; }

        public string Text { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public CachedTextInfo(TextBox textBox)
      : this(textBox.Text, textBox.CaretIndex, textBox.SelectionStart, textBox.SelectionLength)
        {
        }

        #endregion Public Constructors

        #region Private Constructors

        private CachedTextInfo(string text, int caretIndex, int selectionStart, int selectionLength)
        {
            this.Text = text;
            this.CaretIndex = caretIndex;
            this.SelectionStart = selectionStart;
            this.SelectionLength = selectionLength;
        }

        #endregion Private Constructors

        #region Public Methods

        public object Clone()
        {
            return new CachedTextInfo(this.Text, this.CaretIndex, this.SelectionStart, this.SelectionLength);
        }

        #endregion Public Methods
    }
}