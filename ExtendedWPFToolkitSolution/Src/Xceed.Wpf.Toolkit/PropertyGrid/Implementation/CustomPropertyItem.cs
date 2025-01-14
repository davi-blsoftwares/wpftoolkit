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

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    /// <summary>
    /// Used when properties are provided using a list source of items (eg. Properties or PropertiesSource).
    ///
    /// An instance of this class can be used as an item to easily customize the
    /// display of the property directly by modifying the values of this class
    /// (e.g., DisplayName, value, Category, etc.).
    /// </summary>
    public class CustomPropertyItem : PropertyItemBase
    {
        #region Public Fields

        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(string), typeof(CustomPropertyItem), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PropertyOrderProperty =
        DependencyProperty.Register("PropertyOrder", typeof(int), typeof(CustomPropertyItem), new UIPropertyMetadata(0));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(CustomPropertyItem), new UIPropertyMetadata(null, OnValueChanged, OnCoerceValueChanged));

        #endregion Public Fields

        #region Private Fields

        private int _categoryOrder;

        #endregion Private Fields

        #region Public Properties

        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public int CategoryOrder
        {
            get
            {
                return _categoryOrder;
            }
            set
            {
                if (_categoryOrder != value)
                {
                    _categoryOrder = value;
                    // Notify the parent helper since this property may affect ordering.
                    this.RaisePropertyChanged(() => this.CategoryOrder);
                }
            }
        }

        public int PropertyOrder
        {
            get
            {
                return (int)GetValue(PropertyOrderProperty);
            }
            set
            {
                SetValue(PropertyOrderProperty, value);
            }
        }

        public object Value
        {
            get
            {
                return (object)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        #endregion Public Properties

        #region Internal Constructors

        internal CustomPropertyItem()
        { }

        internal CustomPropertyItem(bool isPropertyGridCategorized, bool isSortedAlphabetically)
        {
            _isPropertyGridCategorized = isPropertyGridCategorized;
            _isSortedAlphabetically = isSortedAlphabetically;
        }

        #endregion Internal Constructors

        #region Protected Methods

        protected override Type GetPropertyItemType()
        {
            return this.Value.GetType();
        }

        protected virtual object OnCoerceValueChanged(object baseValue)
        {
            return baseValue;
        }

        protected override void OnEditorChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
            if (oldValue != null)
            {
                oldValue.DataContext = null;
            }

            //case 166547 : Do not overwrite a custom Editor's DataContext set by the user.
            if ((newValue != null) && (newValue.DataContext == null))
            {
                newValue.DataContext = this;
            }
        }

        protected virtual void OnValueChanged(object oldValue, object newValue)
        {
            if (IsInitialized)
            {
                RaiseEvent(new PropertyValueChangedEventArgs(PropertyGrid.PropertyValueChangedEvent, this, oldValue, newValue));
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private static object OnCoerceValueChanged(DependencyObject o, object baseValue)
        {
            CustomPropertyItem prop = o as CustomPropertyItem;
            if (prop != null)
                return prop.OnCoerceValueChanged(baseValue);

            return baseValue;
        }

        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CustomPropertyItem propertyItem = o as CustomPropertyItem;
            if (propertyItem != null)
            {
                propertyItem.OnValueChanged((object)e.OldValue, (object)e.NewValue);
            }
        }

        #endregion Private Methods
    }
}