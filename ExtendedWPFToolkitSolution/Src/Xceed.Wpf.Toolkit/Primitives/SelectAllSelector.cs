/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Collections.Specialized;
using System.Windows;

namespace Xceed.Wpf.Toolkit.Primitives
{
    [TemplatePart(Name = PART_SelectAllSelectorItem, Type = typeof(SelectAllSelectorItem))]
    public class SelectAllSelector : Selector
    {
        #region Public Fields

        public static readonly DependencyProperty IsSelectAllActiveProperty = DependencyProperty.Register("IsSelectAllActive", typeof(bool), typeof(SelectAllSelector), new UIPropertyMetadata(false, OnIsSelectAllActiveChanged));
        public static readonly DependencyProperty SelectAllContentProperty = DependencyProperty.Register("SelectAllContent", typeof(object), typeof(SelectAllSelector), new UIPropertyMetadata("Select All"));

        #endregion Public Fields

        #region Private Fields

        private const string PART_SelectAllSelectorItem = "PART_SelectAllSelectorItem";

        private SelectAllSelectorItem _selectAllSelecotrItem;

        #endregion Private Fields

        #region Public Properties

        public bool IsSelectAllActive
        {
            get
            {
                return (bool)GetValue(IsSelectAllActiveProperty);
            }
            set
            {
                SetValue(IsSelectAllActiveProperty, value);
            }
        }

        public object SelectAllContent
        {
            get
            {
                return (object)GetValue(SelectAllContentProperty);
            }
            set
            {
                SetValue(SelectAllContentProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _selectAllSelecotrItem = this.GetTemplateChild(PART_SelectAllSelectorItem) as SelectAllSelectorItem;
        }

        public void SelectAll()
        {
            foreach (var item in this.ItemsCollection)
            {
                if (!this.SelectedItems.Contains(item))
                {
                    this.SelectedItems.Add(item);
                }
            }
        }

        public void UnSelectAll()
        {
            this.SelectedItems.Clear();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnIsSelectAllActiveChanged(bool oldValue, bool newValue)
        {
            if (newValue && (this.Items.Count > 0))
            {
                this.UpdateSelectAllSelectorItem();
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            this.UpdateSelectAllSelectorItem();
        }

        protected override void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnSelectedItemsCollectionChanged(sender, e);

            this.UpdateSelectAllSelectorItem();
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnIsSelectAllActiveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var selector = o as SelectAllSelector;
            if (selector != null)
                selector.OnIsSelectAllActiveChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void UpdateSelectAllSelectorItem()
        {
            if (_selectAllSelecotrItem != null)
            {
                // All items are selected; select the SelectAll option.
                if (this.Items.Count == this.SelectedItems.Count)
                {
                    _selectAllSelecotrItem.ModifyCurrentSelection(true);
                }
                // Some items are selected; set the SelectAll option to null.
                else if (this.SelectedItems.Count > 0)
                {
                    _selectAllSelecotrItem.ModifyCurrentSelection(null);
                }
                // No items are selected; unselect the SelectAll option.
                else
                {
                    _selectAllSelecotrItem.ModifyCurrentSelection(false);
                }
            }
        }

        #endregion Private Methods
    }
}