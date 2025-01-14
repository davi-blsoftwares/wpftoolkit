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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Xceed.Wpf.Toolkit
{
    public class CollectionControlButton : Button
    {
        #region Public Fields

        public static readonly RoutedEvent CollectionUpdatedEvent = EventManager.RegisterRoutedEvent("CollectionUpdated", RoutingStrategy.Bubble, typeof(EventHandler), typeof(CollectionControlButton));

        public static readonly DependencyProperty EditorDefinitionsProperty = DependencyProperty.Register("EditorDefinitions", typeof(EditorDefinitionCollection), typeof(CollectionControlButton), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CollectionControlButton), new UIPropertyMetadata(false));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CollectionControlButton), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), typeof(CollectionControlButton), new UIPropertyMetadata(null));

        public static readonly DependencyProperty NewItemTypesProperty = DependencyProperty.Register("NewItemTypes", typeof(IList), typeof(CollectionControlButton), new UIPropertyMetadata(null));

        #endregion Public Fields

        #region Public Properties

        public EditorDefinitionCollection EditorDefinitions
        {
            get
            {
                return (EditorDefinitionCollection)GetValue(EditorDefinitionsProperty);
            }
            set
            {
                SetValue(EditorDefinitionsProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public Type ItemsSourceType
        {
            get
            {
                return (Type)GetValue(ItemsSourceTypeProperty);
            }
            set
            {
                SetValue(ItemsSourceTypeProperty, value);
            }
        }

        public IList<Type> NewItemTypes
        {
            get
            {
                return (IList<Type>)GetValue(NewItemTypesProperty);
            }
            set
            {
                SetValue(NewItemTypesProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static CollectionControlButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CollectionControlButton), new FrameworkPropertyMetadata(typeof(CollectionControlButton)));
        }

        public CollectionControlButton()
        {
            this.Click += this.CollectionControlButton_Click;
        }

        #endregion Public Constructors

        #region Public Events

        public event RoutedEventHandler CollectionUpdated
        {
            add
            {
                AddHandler(CollectionUpdatedEvent, value);
            }
            remove
            {
                RemoveHandler(CollectionUpdatedEvent, value);
            }
        }

        #endregion Public Events

        #region Private Methods

        private void CollectionControlButton_Click(object sender, RoutedEventArgs e)
        {
            var collectionControlDialog = new CollectionControlDialog();
            var binding = new Binding("ItemsSource") { Source = this, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(collectionControlDialog, CollectionControlDialog.ItemsSourceProperty, binding);
            collectionControlDialog.NewItemTypes = this.NewItemTypes;
            collectionControlDialog.ItemsSourceType = this.ItemsSourceType;
            collectionControlDialog.IsReadOnly = this.IsReadOnly;
            collectionControlDialog.EditorDefinitions = this.EditorDefinitions;
            var collectionUpdated = collectionControlDialog.ShowDialog();
            if (collectionUpdated.HasValue && collectionUpdated.Value)
            {
                this.RaiseEvent(new RoutedEventArgs(CollectionControlButton.CollectionUpdatedEvent, this));
            }
        }

        #endregion Private Methods
    }
}