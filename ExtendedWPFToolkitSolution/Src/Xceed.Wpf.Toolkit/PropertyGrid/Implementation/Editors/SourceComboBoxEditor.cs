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
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    public class SourceComboBoxEditor : ComboBoxEditor
    {
        #region Private Fields

        private ICollection _collection;
        private TypeConverter _typeConverter;

        #endregion Private Fields

        #region Public Constructors

        public SourceComboBoxEditor(ICollection collection, TypeConverter typeConverter)
        {
            _collection = collection;
            _typeConverter = typeConverter;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
        {
            return _collection;
        }

        protected override IValueConverter CreateValueConverter()
        {
            //When using a stringConverter, we need to convert the value
            if ((_typeConverter != null) && (_typeConverter is StringConverter))
                return new SourceComboBoxEditorConverter(_typeConverter);
            return null;
        }

        #endregion Protected Methods
    }

    internal class SourceComboBoxEditorConverter : IValueConverter
    {
        #region Private Fields

        private TypeConverter _typeConverter;

        #endregion Private Fields

        #region Internal Constructors

        internal SourceComboBoxEditorConverter(TypeConverter typeConverter)
        {
            _typeConverter = typeConverter;
        }

        #endregion Internal Constructors

        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_typeConverter != null)
            {
                if (_typeConverter.CanConvertTo(typeof(string)))
                    return _typeConverter.ConvertTo(value, typeof(string));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_typeConverter != null)
            {
                if (_typeConverter.CanConvertFrom(value.GetType()))
                    return _typeConverter.ConvertFrom(value);
            }
            return value;
        }

        #endregion Public Methods
    }
}