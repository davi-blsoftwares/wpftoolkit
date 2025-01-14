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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    /// <summary>
    /// This Control is intended to be used in the template of the
    /// PropertyItemBase and PropertyGrid classes to contain the
    /// sub-children properties.
    /// </summary>
    public class PropertyItemsControl : ItemsControl
    {
        #region Internal Fields

        internal static readonly RoutedEvent ClearPropertyItemEvent = EventManager.RegisterRoutedEvent("ClearPropertyItem", RoutingStrategy.Bubble, typeof(PropertyItemEventHandler), typeof(PropertyItemsControl));

        internal static readonly RoutedEvent PreparePropertyItemEvent = EventManager.RegisterRoutedEvent("PreparePropertyItem", RoutingStrategy.Bubble, typeof(PropertyItemEventHandler), typeof(PropertyItemsControl));

        #endregion Internal Fields

        #region Public Constructors

        public PropertyItemsControl()
        {
            var propertyItemsControlProperties = TypeDescriptor.GetProperties(this, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) });
            var prop1 = propertyItemsControlProperties.Find("VirtualizingPanel.IsVirtualizingWhenGrouping", false);
            if (prop1 != null)
            {
                prop1.SetValue(this, true);
            }
            var prop2 = propertyItemsControlProperties.Find("VirtualizingPanel.CacheLengthUnit", false);
            if (prop2 != null)
            {
                prop2.SetValue(this, Enum.ToObject(prop2.PropertyType, 1));
            }
        }

        #endregion Public Constructors

        #region Internal Events

        internal event PropertyItemEventHandler ClearPropertyItem
        {
            add
            {
                AddHandler(PropertyItemsControl.ClearPropertyItemEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyItemsControl.ClearPropertyItemEvent, value);
            }
        }

        internal event PropertyItemEventHandler PreparePropertyItem
        {
            add
            {
                AddHandler(PropertyItemsControl.PreparePropertyItemEvent, value);
            }
            remove
            {
                RemoveHandler(PropertyItemsControl.PreparePropertyItemEvent, value);
            }
        }

        #endregion Internal Events

        #region Protected Methods

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            this.RaiseClearPropertyItemEvent((PropertyItemBase)element, item);
            base.ClearContainerForItemOverride(element, item);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is PropertyItemBase);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            this.RaisePreparePropertyItemEvent((PropertyItemBase)element, item);
        }

        #endregion Protected Methods

        #region Private Methods

        private void RaiseClearPropertyItemEvent(PropertyItemBase propertyItem, object item)
        {
            this.RaiseEvent(new PropertyItemEventArgs(PropertyItemsControl.ClearPropertyItemEvent, this, propertyItem, item));
        }

        private void RaisePreparePropertyItemEvent(PropertyItemBase propertyItem, object item)
        {
            this.RaiseEvent(new PropertyItemEventArgs(PropertyItemsControl.PreparePropertyItemEvent, this, propertyItem, item));
        }

        #endregion Private Methods
    }
}