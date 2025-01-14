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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Xceed.Wpf.Toolkit.Primitives
{
    [TemplatePart(Name = PART_HeaderThumb, Type = typeof(Thumb))]
    [TemplatePart(Name = PART_Icon, Type = typeof(Image))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_ToolWindowCloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_BlockMouseInputsBorder, Type = typeof(Border))]
    [TemplatePart(Name = PART_HeaderGrid, Type = typeof(Grid))]
    public class WindowControl : ContentControl
    {
        #region Public Fields

        public static readonly RoutedEvent ActivatedEvent = EventManager.RegisterRoutedEvent("Activated", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WindowControl));
        public static readonly DependencyProperty CaptionFontSizeProperty = DependencyProperty.Register("CaptionFontSize", typeof(double), typeof(WindowControl), new UIPropertyMetadata(15d));
        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(WindowControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty CaptionIconProperty = DependencyProperty.Register("CaptionIcon", typeof(ImageSource), typeof(WindowControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(WindowControl), new UIPropertyMetadata(String.Empty));
        public static readonly DependencyProperty CaptionShadowBrushProperty = DependencyProperty.Register("CaptionShadowBrush", typeof(Brush), typeof(WindowControl), new UIPropertyMetadata(new SolidColorBrush(Color.FromArgb(179, 255, 255, 255))));
        public static readonly RoutedEvent CloseButtonClickedEvent = EventManager.RegisterRoutedEvent("CloseButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WindowControl));
        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register("CloseButtonStyle", typeof(Style), typeof(WindowControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(WindowControl), new PropertyMetadata(Visibility.Visible, null, OnCoerceCloseButtonVisibility));
        public static readonly ComponentResourceKey DefaultCloseButtonStyleKey = new ComponentResourceKey(typeof(WindowControl), "DefaultCloseButtonStyle");

        public static readonly RoutedEvent HeaderDragDeltaEvent = EventManager.RegisterRoutedEvent("HeaderDragDelta", RoutingStrategy.Bubble, typeof(DragDeltaEventHandler), typeof(WindowControl));
        public static readonly RoutedEvent HeaderIconClickedEvent = EventManager.RegisterRoutedEvent("HeaderIconClicked", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(WindowControl));
        public static readonly RoutedEvent HeaderIconDoubleClickedEvent = EventManager.RegisterRoutedEvent("HeaderIconDoubleClicked", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(WindowControl));
        public static readonly RoutedEvent HeaderMouseLeftButtonClickedEvent = EventManager.RegisterRoutedEvent("HeaderMouseLeftButtonClicked", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(WindowControl));
        public static readonly RoutedEvent HeaderMouseLeftButtonDoubleClickedEvent = EventManager.RegisterRoutedEvent("HeaderMouseLeftButtonDoubleClicked", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(WindowControl));
        public static readonly RoutedEvent HeaderMouseRightButtonClickedEvent = EventManager.RegisterRoutedEvent("HeaderMouseRightButtonClicked", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(WindowControl));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(WindowControl), new UIPropertyMetadata(true, OnIsActiveChanged, OnCoerceIsActive));
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof(double), typeof(WindowControl), new PropertyMetadata(0.0, new PropertyChangedCallback(OnLeftPropertyChanged), OnCoerceLeft));
        public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof(double), typeof(WindowControl), new PropertyMetadata(0.0, new PropertyChangedCallback(OnTopPropertyChanged), OnCoerceTop));
        public static readonly DependencyProperty WindowBackgroundProperty = DependencyProperty.Register("WindowBackground", typeof(Brush), typeof(WindowControl), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowBorderBrushProperty = DependencyProperty.Register("WindowBorderBrush", typeof(Brush), typeof(WindowControl), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowBorderThicknessProperty = DependencyProperty.Register("WindowBorderThickness", typeof(Thickness), typeof(WindowControl), new PropertyMetadata(new Thickness(0)));
        public static readonly DependencyProperty WindowInactiveBackgroundProperty = DependencyProperty.Register("WindowInactiveBackground", typeof(Brush), typeof(WindowControl), new PropertyMetadata(null));
        public static readonly DependencyProperty WindowOpacityProperty = DependencyProperty.Register("WindowOpacity", typeof(double), typeof(WindowControl), new PropertyMetadata(1d));
        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.Register("WindowStyle", typeof(WindowStyle), typeof(WindowControl), new PropertyMetadata(WindowStyle.SingleBorderWindow, null, OnCoerceWindowStyle));

        public static readonly DependencyProperty WindowThicknessProperty = DependencyProperty.Register("WindowThickness", typeof(Thickness), typeof(WindowControl),
                                                                        new PropertyMetadata(new Thickness(SystemParameters.ResizeFrameVerticalBorderWidth - 3,
                                                                                                            SystemParameters.ResizeFrameHorizontalBorderHeight - 3,
                                                                                                            SystemParameters.ResizeFrameVerticalBorderWidth - 3,
                                                                                                            SystemParameters.ResizeFrameHorizontalBorderHeight - 3)));

        #endregion Public Fields

        #region Internal Fields

        internal Border _windowBlockMouseInputsPanel;

        #endregion Internal Fields

        #region Private Fields

        private const string PART_BlockMouseInputsBorder = "PART_BlockMouseInputsBorder";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_HeaderGrid = "PART_HeaderGrid";
        private const string PART_HeaderThumb = "PART_HeaderThumb";
        private const string PART_Icon = "PART_Icon";
        private const string PART_ToolWindowCloseButton = "PART_ToolWindowCloseButton";
        private Button _closeButton;
        private Thumb _headerThumb;
        private Image _icon;
        private bool _IsBlockMouseInputsPanelActive;
        private bool _setIsActiveInternal;
        private Button _windowToolboxCloseButton;

        #endregion Private Fields

        #region Public Properties

        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public double CaptionFontSize
        {
            get
            {
                return (double)GetValue(CaptionFontSizeProperty);
            }
            set
            {
                SetValue(CaptionFontSizeProperty, value);
            }
        }

        public Brush CaptionForeground
        {
            get
            {
                return (Brush)GetValue(CaptionForegroundProperty);
            }
            set
            {
                SetValue(CaptionForegroundProperty, value);
            }
        }

        public ImageSource CaptionIcon
        {
            get
            {
                return (ImageSource)GetValue(CaptionIconProperty);
            }
            set
            {
                SetValue(CaptionIconProperty, value);
            }
        }

        public Brush CaptionShadowBrush
        {
            get
            {
                return (Brush)GetValue(CaptionShadowBrushProperty);
            }
            set
            {
                SetValue(CaptionShadowBrushProperty, value);
            }
        }

        public Style CloseButtonStyle
        {
            get
            {
                return (Style)GetValue(CloseButtonStyleProperty);
            }
            set
            {
                SetValue(CloseButtonStyleProperty, value);
            }
        }

        public Visibility CloseButtonVisibility
        {
            get
            {
                return (Visibility)GetValue(CloseButtonVisibilityProperty);
            }
            set
            {
                SetValue(CloseButtonVisibilityProperty, value);
            }
        }

        public bool IsActive
        {
            get
            {
                return (bool)GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }

        public double Left
        {
            get
            {
                return (double)GetValue(LeftProperty);
            }
            set
            {
                SetValue(LeftProperty, value);
            }
        }

        public double Top
        {
            get
            {
                return (double)GetValue(TopProperty);
            }
            set
            {
                SetValue(TopProperty, value);
            }
        }

        public Brush WindowBackground
        {
            get
            {
                return (Brush)GetValue(WindowBackgroundProperty);
            }
            set
            {
                SetValue(WindowBackgroundProperty, value);
            }
        }

        public Brush WindowBorderBrush
        {
            get
            {
                return (Brush)GetValue(WindowBorderBrushProperty);
            }
            set
            {
                SetValue(WindowBorderBrushProperty, value);
            }
        }

        public Thickness WindowBorderThickness
        {
            get
            {
                return (Thickness)GetValue(WindowBorderThicknessProperty);
            }
            set
            {
                SetValue(WindowBorderThicknessProperty, value);
            }
        }

        public Brush WindowInactiveBackground
        {
            get
            {
                return (Brush)GetValue(WindowInactiveBackgroundProperty);
            }
            set
            {
                SetValue(WindowInactiveBackgroundProperty, value);
            }
        }

        public double WindowOpacity
        {
            get
            {
                return (double)GetValue(WindowOpacityProperty);
            }
            set
            {
                SetValue(WindowOpacityProperty, value);
            }
        }

        public WindowStyle WindowStyle
        {
            get
            {
                return (WindowStyle)GetValue(WindowStyleProperty);
            }
            set
            {
                SetValue(WindowStyleProperty, value);
            }
        }

        public Thickness WindowThickness
        {
            get
            {
                return (Thickness)GetValue(WindowThicknessProperty);
            }
            set
            {
                SetValue(WindowThicknessProperty, value);
            }
        }

        #endregion Public Properties

        #region Internal Properties

        internal virtual bool AllowPublicIsActiveChange
        {
            get { return true; }
        }

        internal bool IsBlockMouseInputsPanelActive
        {
            get
            {
                return _IsBlockMouseInputsPanelActive;
            }
            set
            {
                if (value != _IsBlockMouseInputsPanelActive)
                {
                    _IsBlockMouseInputsPanelActive = value;
                    this.UpdateBlockMouseInputsPanel();
                }
            }
        }

        internal bool IsStartupPositionInitialized
        {
            get;
            set;
        }

        #endregion Internal Properties

        #region Public Constructors

        static WindowControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowControl), new FrameworkPropertyMetadata(typeof(WindowControl)));
        }

        public WindowControl()
        {
        }

        #endregion Public Constructors

        #region Public Events

        public event RoutedEventHandler Activated
        {
            add
            {
                AddHandler(ActivatedEvent, value);
            }
            remove
            {
                RemoveHandler(ActivatedEvent, value);
            }
        }

        public event RoutedEventHandler CloseButtonClicked
        {
            add
            {
                AddHandler(CloseButtonClickedEvent, value);
            }
            remove
            {
                RemoveHandler(CloseButtonClickedEvent, value);
            }
        }

        public event DragDeltaEventHandler HeaderDragDelta
        {
            add
            {
                AddHandler(HeaderDragDeltaEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderDragDeltaEvent, value);
            }
        }

        public event MouseButtonEventHandler HeaderIconClicked
        {
            add
            {
                AddHandler(HeaderIconClickedEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderIconClickedEvent, value);
            }
        }

        public event MouseButtonEventHandler HeaderIconDoubleClicked
        {
            add
            {
                AddHandler(HeaderIconDoubleClickedEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderIconDoubleClickedEvent, value);
            }
        }

        public event MouseButtonEventHandler HeaderMouseLeftButtonClicked
        {
            add
            {
                AddHandler(HeaderMouseLeftButtonClickedEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderMouseLeftButtonClickedEvent, value);
            }
        }

        public event MouseButtonEventHandler HeaderMouseLeftButtonDoubleClicked
        {
            add
            {
                AddHandler(HeaderMouseLeftButtonDoubleClickedEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderMouseLeftButtonDoubleClickedEvent, value);
            }
        }

        public event MouseButtonEventHandler HeaderMouseRightButtonClicked
        {
            add
            {
                AddHandler(HeaderMouseRightButtonClickedEvent, value);
            }
            remove
            {
                RemoveHandler(HeaderMouseRightButtonClickedEvent, value);
            }
        }

        #endregion Public Events

        #region Internal Events

        internal event EventHandler<EventArgs> LeftChanged;

        internal event EventHandler<EventArgs> TopChanged;

        #endregion Internal Events

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_headerThumb != null)
            {
                _headerThumb.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.HeaderPreviewMouseLeftButtonDown);
                _headerThumb.PreviewMouseRightButtonDown -= new MouseButtonEventHandler(this.HeaderPreviewMouseRightButtonDown);
                _headerThumb.DragDelta -= new DragDeltaEventHandler(this.HeaderThumbDragDelta);
            }
            _headerThumb = this.Template.FindName(PART_HeaderThumb, this) as Thumb;
            if (_headerThumb != null)
            {
                _headerThumb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.HeaderPreviewMouseLeftButtonDown);
                _headerThumb.PreviewMouseRightButtonDown += new MouseButtonEventHandler(this.HeaderPreviewMouseRightButtonDown);
                _headerThumb.DragDelta += new DragDeltaEventHandler(this.HeaderThumbDragDelta);
            }

            if (_icon != null)
            {
                _icon.MouseLeftButtonDown -= new MouseButtonEventHandler(this.IconMouseLeftButtonDown);
            }
            _icon = this.Template.FindName(PART_Icon, this) as Image;
            if (_icon != null)
            {
                _icon.MouseLeftButtonDown += new MouseButtonEventHandler(this.IconMouseLeftButtonDown);
            }

            if (_closeButton != null)
            {
                _closeButton.Click -= new RoutedEventHandler(this.Close);
            }
            _closeButton = this.Template.FindName(PART_CloseButton, this) as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += new RoutedEventHandler(this.Close);
            }

            if (_windowToolboxCloseButton != null)
            {
                _windowToolboxCloseButton.Click -= new RoutedEventHandler(this.Close);
            }
            _windowToolboxCloseButton = this.Template.FindName(PART_ToolWindowCloseButton, this) as Button;
            if (_windowToolboxCloseButton != null)
            {
                _windowToolboxCloseButton.Click += new RoutedEventHandler(this.Close);
            }

            _windowBlockMouseInputsPanel = this.Template.FindName(PART_BlockMouseInputsBorder, this) as Border;
            this.UpdateBlockMouseInputsPanel();
        }

        #endregion Public Methods

        #region Internal Methods

        internal double GetHeaderHeight()
        {
            Grid headerGrid = this.Template.FindName(PART_HeaderGrid, this) as Grid;
            if (headerGrid != null)
                return headerGrid.ActualHeight;
            return 0;
        }

        internal void SetIsActiveInternal(bool isActive)
        {
            _setIsActiveInternal = true;
            this.IsActive = isActive;
            _setIsActiveInternal = false;
        }

        internal virtual void UpdateBlockMouseInputsPanel()
        {
            if (_windowBlockMouseInputsPanel != null)
            {
                _windowBlockMouseInputsPanel.Visibility = this.IsBlockMouseInputsPanelActive ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual object OnCoerceCloseButtonVisibility(Visibility newValue)
        {
            return newValue;
        }

        protected virtual object OnCoerceWindowStyle(WindowStyle newValue)
        {
            return newValue;
        }

        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                if (this.GetType() == typeof(WindowControl))
                {
                    this.RaiseEvent(new RoutedEventArgs(ActivatedEvent, this));
                }
            }
        }

        protected virtual void OnLeftPropertyChanged(double oldValue, double newValue)
        {
            EventHandler<EventArgs> handler = LeftChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnTopPropertyChanged(double oldValue, double newValue)
        {
            EventHandler<EventArgs> handler = TopChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnWindowStyleChanged(WindowStyle oldValue, WindowStyle newValue)
        {
        }

        #endregion Protected Methods

        #region Private Methods

        private static object OnCoerceCloseButtonVisibility(DependencyObject d, object basevalue)
        {
            if (basevalue == DependencyProperty.UnsetValue)
                return basevalue;

            WindowControl windowControl = d as WindowControl;
            if (windowControl == null)
                return basevalue;
            return windowControl.OnCoerceCloseButtonVisibility((Visibility)basevalue);
        }

        private static object OnCoerceIsActive(DependencyObject d, object basevalue)
        {
            WindowControl w = d as WindowControl;
            if (w != null && !w._setIsActiveInternal && !w.AllowPublicIsActiveChange)
                throw new InvalidOperationException("Cannot set IsActive directly. This is handled by the underlying system");

            return basevalue;
        }

        private static object OnCoerceLeft(DependencyObject d, object basevalue)
        {
            if (basevalue == DependencyProperty.UnsetValue)
                return basevalue;

            var windowControl = (WindowControl)d;
            if (windowControl == null)
                return basevalue;

            return windowControl.OnCoerceLeft(basevalue);
        }

        private static object OnCoerceTop(DependencyObject d, object basevalue)
        {
            if (basevalue == DependencyProperty.UnsetValue)
                return basevalue;

            var windowControl = (WindowControl)d;
            if (windowControl == null)
                return basevalue;

            return windowControl.OnCoerceTop(basevalue);
        }

        private static object OnCoerceWindowStyle(DependencyObject d, object basevalue)
        {
            if (basevalue == DependencyProperty.UnsetValue)
                return basevalue;

            WindowControl windowControl = d as WindowControl;
            if (windowControl == null)
                return basevalue;
            return windowControl.OnCoerceWindowStyle((WindowStyle)basevalue);
        }

        private static void OnIsActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var windowControl = obj as WindowControl;
            if (windowControl != null)
                windowControl.OnIsActiveChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnLeftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            WindowControl windowControl = obj as WindowControl;
            if (windowControl != null)
                windowControl.OnLeftPropertyChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnTopPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            WindowControl windowControl = obj as WindowControl;
            if (windowControl != null)
                windowControl.OnTopPropertyChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnWindowStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindowControl window = (WindowControl)d;
            if (window != null)
                window.OnWindowStyleChanged((WindowStyle)e.OldValue, (WindowStyle)e.NewValue);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(CloseButtonClickedEvent, this));
        }

        private void HeaderPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            args.RoutedEvent = (e.ClickCount == 2) ? HeaderMouseLeftButtonDoubleClickedEvent : HeaderMouseLeftButtonClickedEvent;
            args.Source = this;
            this.RaiseEvent(args);
        }

        private void HeaderPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Right);
            args.RoutedEvent = HeaderMouseRightButtonClickedEvent;
            args.Source = this;
            this.RaiseEvent(args);
        }

        private void HeaderThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            DragDeltaEventArgs args = new DragDeltaEventArgs(e.HorizontalChange, e.VerticalChange);
            args.RoutedEvent = HeaderDragDeltaEvent;
            args.Source = this;
            this.RaiseEvent(args);
        }

        private void IconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            args.RoutedEvent = (e.ClickCount == 2) ? HeaderIconDoubleClickedEvent : HeaderIconClickedEvent;
            args.Source = this;
            this.RaiseEvent(args);
        }

        private object OnCoerceLeft(object newValue)
        {
            var value = (double)newValue;
            if (object.Equals(value, double.NaN))
                return 0.0;

            return newValue;
        }

        private object OnCoerceTop(object newValue)
        {
            var value = (double)newValue;
            if (object.Equals(value, double.NaN))
                return 0.0;

            return newValue;
        }

        #endregion Private Methods
    }
}