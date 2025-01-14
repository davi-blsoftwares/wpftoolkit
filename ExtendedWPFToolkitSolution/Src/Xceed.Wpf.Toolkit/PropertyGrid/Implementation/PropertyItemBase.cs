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
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    [TemplatePart(Name = PropertyGrid.PART_PropertyItemsControl, Type = typeof(PropertyItemsControl))]
    [TemplatePart(Name = PropertyItemBase.PART_ValueContainer, Type = typeof(ContentControl))]
    public abstract class PropertyItemBase : Control, IPropertyContainer, INotifyPropertyChanged
    {
        #region Public Fields

        public static readonly DependencyProperty AdvancedOptionsIconProperty =
            DependencyProperty.Register("AdvancedOptionsIcon", typeof(ImageSource), typeof(PropertyItemBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty AdvancedOptionsTooltipProperty =
        DependencyProperty.Register("AdvancedOptionsTooltip", typeof(object), typeof(PropertyItemBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(string), typeof(PropertyItemBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty DisplayNameProperty =
        DependencyProperty.Register("DisplayName", typeof(string), typeof(PropertyItemBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register("Editor", typeof(FrameworkElement), typeof(PropertyItemBase), new UIPropertyMetadata(null, OnEditorChanged));
        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.Register("HighlightedText", typeof(string), typeof(PropertyItemBase), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsExpandableProperty =
        DependencyProperty.Register("IsExpandable", typeof(bool), typeof(PropertyItemBase), new UIPropertyMetadata(false));

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(PropertyItemBase), new UIPropertyMetadata(false, OnIsExpandedChanged));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(PropertyItemBase), new UIPropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty WillRefreshPropertyGridProperty =
        DependencyProperty.Register("WillRefreshPropertyGrid", typeof(bool), typeof(PropertyItemBase), new UIPropertyMetadata(false));

        #endregion Public Fields

        #region Internal Fields

        internal const string PART_ValueContainer = "PART_ValueContainer";

        internal static readonly RoutedEvent ItemSelectionChangedEvent = EventManager.RegisterRoutedEvent(
        "ItemSelectionChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PropertyItemBase));

        internal bool _isPropertyGridCategorized;
        internal bool _isSortedAlphabetically = true;

        #endregion Internal Fields

        #region Private Fields

        private ContainerHelperBase _containerHelper;
        private IPropertyContainer _parentNode;
        private ContentControl _valueContainer;

        #endregion Private Fields

        #region Public Properties

        public ImageSource AdvancedOptionsIcon
        {
            get { return (ImageSource)GetValue(AdvancedOptionsIconProperty); }
            set { SetValue(AdvancedOptionsIconProperty, value); }
        }

        public object AdvancedOptionsTooltip
        {
            get { return (object)GetValue(AdvancedOptionsTooltipProperty); }
            set { SetValue(AdvancedOptionsTooltipProperty, value); }
        }

        bool IPropertyContainer.AutoGenerateProperties
        {
            get
            {
                if (this.ParentNode != null)
                {
                    var propertyItemPropertyDefinitions = this.GetPropertItemPropertyDefinitions();
                    // No PropertyDefinitions specified : show all properties of this PropertyItem.
                    if ((propertyItemPropertyDefinitions == null) || (propertyItemPropertyDefinitions.Count == 0))
                        return true;

                    // A PropertyDefinitions is specified : show only the properties of the PropertyDefinitions from this PropertyItem.
                    return this.ParentNode.AutoGenerateProperties;
                }
                return true;
            }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        public FrameworkElement Editor
        {
            get
            {
                return (FrameworkElement)GetValue(EditorProperty);
            }
            set
            {
                SetValue(EditorProperty, value);
            }
        }

        EditorDefinitionCollection IPropertyContainer.EditorDefinitions
        {
            get
            {
                return (this.ParentNode != null) ? this.ParentNode.EditorDefinitions : null;
            }
        }

        FilterInfo IPropertyContainer.FilterInfo
        {
            get { return new FilterInfo(); }
        }

        bool IPropertyContainer.HideInheritedProperties
        {
            get
            {
                return false;
            }
        }

        public string HighlightedText
        {
            get
            {
                return (string)GetValue(HighlightedTextProperty);
            }
            set
            {
                SetValue(HighlightedTextProperty, value);
            }
        }

        bool IPropertyContainer.IsCategorized
        {
            get
            {
                return _isPropertyGridCategorized;
            }
        }

        public bool IsExpandable
        {
            get { return (bool)GetValue(IsExpandableProperty); }
            set { SetValue(IsExpandableProperty, value); }
        }

        public bool IsExpanded
        {
            get
            {
                return (bool)GetValue(IsExpandedProperty);
            }
            set
            {
                SetValue(IsExpandedProperty, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        bool IPropertyContainer.IsSortedAlphabetically
        {
            get
            {
                return _isSortedAlphabetically;
            }
        }

        public int Level
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the parent property grid element of this property.
        /// A PropertyItemBase instance if this is a sub-element,
        /// or the PropertyGrid itself if this is a first-level property.
        /// </summary>
        public FrameworkElement ParentElement
        {
            get { return this.ParentNode as FrameworkElement; }
        }

        public IList Properties
        {
            get
            {
                if (_containerHelper == null)
                {
                    _containerHelper = new ObjectContainerHelper(this, null);
                }
                return _containerHelper.Properties;
            }
        }

        /// <summary>
        /// Get the PropertyContainerStyle for sub items of this property.
        /// It return the value defined on PropertyGrid.PropertyContainerStyle.
        /// </summary>
        public Style PropertyContainerStyle
        {
            get
            {
                return (ParentNode != null)
                ? ParentNode.PropertyContainerStyle
                : null;
            }
        }

        Style IPropertyContainer.PropertyContainerStyle
        {
            get { return this.PropertyContainerStyle; }
        }

        PropertyDefinitionCollection IPropertyContainer.PropertyDefinitions
        {
            get
            {
                return this.GetPropertItemPropertyDefinitions();
            }
        }

        public bool WillRefreshPropertyGrid
        {
            get
            {
                return (bool)GetValue(WillRefreshPropertyGridProperty);
            }
            set
            {
                SetValue(WillRefreshPropertyGridProperty, value);
            }
        }

        #endregion Public Properties

        #region Internal Properties

        ContainerHelperBase IPropertyContainer.ContainerHelper
        {
            get
            {
                return this.ContainerHelper;
            }
        }

        internal IPropertyContainer ParentNode
        {
            get
            {
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }

        internal ContentControl ValueContainer
        {
            get
            {
                return _valueContainer;
            }
        }

        #endregion Internal Properties

        #region Public Constructors

        static PropertyItemBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyItemBase), new FrameworkPropertyMetadata(typeof(PropertyItemBase)));
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal PropertyItemBase()
        {
            this.GotFocus += new RoutedEventHandler(PropertyItemBase_GotFocus);
            this.RequestBringIntoView += this.PropertyItemBase_RequestBringIntoView;
            AddHandler(PropertyItemsControl.PreparePropertyItemEvent, new PropertyItemEventHandler(OnPreparePropertyItemInternal));
            AddHandler(PropertyItemsControl.ClearPropertyItemEvent, new PropertyItemEventHandler(OnClearPropertyItemInternal));
        }

        #endregion Internal Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Methods

        bool? IPropertyContainer.IsPropertyVisible(PropertyDescriptor pd)
        {
            if (_parentNode != null)
            {
                return _parentNode.IsPropertyVisible(pd);
            }

            return null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _containerHelper.ChildrenItemsControl = GetTemplateChild(PropertyGrid.PART_PropertyItemsControl) as PropertyItemsControl;
            _valueContainer = GetTemplateChild(PropertyItemBase.PART_ValueContainer) as ContentControl;
        }

        #endregion Public Methods

        #region Internal Methods

        internal void RaisePropertyChanged<TMember>(Expression<Func<TMember>> propertyExpression)
        {
            this.Notify(this.PropertyChanged, propertyExpression);
        }

        internal void RaisePropertyChanged(string name)
        {
            this.Notify(this.PropertyChanged, name);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual string GetPropertyItemName()
        {
            return this.DisplayName;
        }

        protected virtual Type GetPropertyItemType()
        {
            return null;
        }

        protected virtual void OnEditorChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
        }

        protected virtual void OnIsExpandedChanged(bool oldValue, bool newValue)
        {
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            this.RaiseItemSelectionChangedEvent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            IsSelected = true;
            if (!this.IsKeyboardFocusWithin)
            {
                this.Focus();
            }
            // Handle the event; otherwise, the possible
            // parent property item will select itself too.
            e.Handled = true;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // First check that the raised property is actually a real CLR property.
            // This could be something else like an Attached DP.
            if (ReflectionHelper.IsPublicInstanceProperty(GetType(), e.Property.Name)
              && this.IsLoaded
              && (_parentNode != null)
              && !_parentNode.ContainerHelper.IsCleaning)
            {
                this.RaisePropertyChanged(e.Property.Name);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnEditorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PropertyItemBase propertyItem = o as PropertyItemBase;
            if (propertyItem != null)
                propertyItem.OnEditorChanged((FrameworkElement)e.OldValue, (FrameworkElement)e.NewValue);
        }

        private static void OnIsExpandedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PropertyItemBase propertyItem = o as PropertyItemBase;
            if (propertyItem != null)
                propertyItem.OnIsExpandedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PropertyItemBase propertyItem = o as PropertyItemBase;
            if (propertyItem != null)
                propertyItem.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        #endregion Private Methods

        internal ContainerHelperBase ContainerHelper
        {
            get
            {
                return _containerHelper;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _containerHelper = value;
                // Properties property relies on the "Properties" property of the helper
                // class. Raise a property-changed event.
                this.RaisePropertyChanged(() => this.Properties);
            }
        }

        private PropertyDefinitionCollection GetPropertItemPropertyDefinitions()
        {
            if ((this.ParentNode != null) && (this.ParentNode.PropertyDefinitions != null))
            {
                var name = this.GetPropertyItemName();
                foreach (var pd in this.ParentNode.PropertyDefinitions)
                {
                    if (pd.TargetProperties.Contains(name))
                    {
                        // PropertyDefinitions contains a PropertyDefinition for this PropertyItem Name => return its PropertyDefinitions.
                        return pd.PropertyDefinitions;
                    }
                    else
                    {
                        var type = this.GetPropertyItemType();
                        if (type != null)
                        {
                            foreach (var targetProperty in pd.TargetProperties)
                            {
                                var targetPropertyType = targetProperty as Type;
                                // PropertyDefinitions contains a PropertyDefinition for this PropertyItem Type => return its PropertyDefinitions.
                                if ((targetPropertyType != null) && targetPropertyType.IsAssignableFrom(type))
                                    return pd.PropertyDefinitions;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void OnClearPropertyItemInternal(object sender, PropertyItemEventArgs args)
        {
            _containerHelper.ClearChildrenPropertyItem(args.PropertyItem, args.Item);
            // This is the callback of the PreparePropertyItem comming from the template PropertyItemControl.
            args.PropertyItem.Level = 0;

            args.Handled = true;
        }

        private void OnPreparePropertyItemInternal(object sender, PropertyItemEventArgs args)
        {
            // This is the callback of the PreparePropertyItem comming from the template PropertyItemControl.
            args.PropertyItem.Level = this.Level + 1;
            _containerHelper.PrepareChildrenPropertyItem(args.PropertyItem, args.Item);

            args.Handled = true;
        }

        private void PropertyItemBase_GotFocus(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
            // Handle the event; otherwise, the possible
            // parent property item will select itself too.
            e.Handled = true;
        }

        private void PropertyItemBase_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void RaiseItemSelectionChangedEvent()
        {
            RaiseEvent(new RoutedEventArgs(PropertyItemBase.ItemSelectionChangedEvent));
        }
    }
}