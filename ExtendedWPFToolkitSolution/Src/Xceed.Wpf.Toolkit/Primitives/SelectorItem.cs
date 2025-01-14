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
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit.Primitives
{
    public class SelectorItem : ContentControl
    {
        #region Public Fields

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool?), typeof(SelectorItem), new UIPropertyMetadata(false, OnIsSelectedChanged));

        public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof(SelectorItem));

        public static readonly RoutedEvent UnselectedEvent = Selector.UnSelectedEvent.AddOwner(typeof(SelectorItem));

        #endregion Public Fields

        #region Public Properties

        public bool? IsSelected
        {
            get
            {
                return (bool?)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        #endregion Public Properties

        #region Internal Properties

        internal Selector ParentSelector
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as Selector;
            }
        }

        #endregion Internal Properties

        #region Public Constructors

        static SelectorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectorItem), new FrameworkPropertyMetadata(typeof(SelectorItem)));
        }

        #endregion Public Constructors

        #region Protected Methods

        protected virtual void OnIsSelectedChanged(bool? oldValue, bool? newValue)
        {
            if (newValue.HasValue)
            {
                if (newValue.Value)
                {
                    this.RaiseEvent(new RoutedEventArgs(Selector.SelectedEvent, this));
                }
                else
                {
                    this.RaiseEvent(new RoutedEventArgs(Selector.UnSelectedEvent, this));
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SelectorItem selectorItem = o as SelectorItem;
            if (selectorItem != null)
                selectorItem.OnIsSelectedChanged((bool?)e.OldValue, (bool?)e.NewValue);
        }

        #endregion Private Methods
    }
}