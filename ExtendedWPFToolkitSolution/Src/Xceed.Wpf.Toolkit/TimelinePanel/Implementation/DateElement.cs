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

namespace Xceed.Wpf.Toolkit
{
    public sealed class DateElement : IComparable<DateElement>
    {
        #region Internal Fields

        internal Rect PlacementRectangle;

        #endregion Internal Fields

        #region Private Fields

        private readonly DateTime _date;
        private readonly DateTime _dateEnd;
        private readonly UIElement _element;
        private readonly int _originalIndex;

        #endregion Private Fields

        #region Public Properties

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
        }

        public UIElement Element
        {
            get
            {
                return _element;
            }
        }

        #endregion Public Properties

        #region Internal Constructors

        internal DateElement(UIElement element, DateTime date, DateTime dateEnd)
      : this(element, date, dateEnd, -1)
        {
        }

        internal DateElement(UIElement element, DateTime date, DateTime dateEnd, int originalIndex)
        {
            _element = element;
            _date = date;
            _dateEnd = dateEnd;
            _originalIndex = originalIndex;
        }

        #endregion Internal Constructors

        #region Public Methods

        public int CompareTo(DateElement d)
        {
            int dateCompare = this.Date.CompareTo(d.Date);
            if (dateCompare != 0)
                return dateCompare;

            if (_originalIndex >= 0)
                return (_originalIndex < d._originalIndex) ? -1 : 1;

            return -this.DateEnd.CompareTo(d.DateEnd);
        }

        public override string ToString()
        {
            var fe = this.Element as FrameworkElement;
            if (fe == null)
                return base.ToString();

            if (fe.Tag != null)
                return fe.Tag.ToString();

            return fe.Name;
        }

        #endregion Public Methods
    }
}