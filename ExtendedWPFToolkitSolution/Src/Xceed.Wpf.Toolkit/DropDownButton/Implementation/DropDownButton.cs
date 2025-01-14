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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit
{
    [TemplatePart(Name = PART_DropDownButton, Type = typeof(ToggleButton))]
    [TemplatePart(Name = PART_ContentPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PART_Popup, Type = typeof(Popup))]
    public class DropDownButton : ContentControl, ICommandSource
    {
        #region Public Fields

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DropDownButton));
        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DropDownButton));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(DropDownButton), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(DropDownButton), new PropertyMetadata((ICommand)null, OnCommandChanged));
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(DropDownButton), new PropertyMetadata(null));
        public static readonly DependencyProperty DropDownContentBackgroundProperty = DependencyProperty.Register("DropDownContentBackground", typeof(Brush), typeof(DropDownButton), new UIPropertyMetadata(null));
        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(DropDownButton), new UIPropertyMetadata(null, OnDropDownContentChanged));

        public static readonly DependencyProperty DropDownPositionProperty = DependencyProperty.Register("DropDownPosition", typeof(PlacementMode)
      , typeof(DropDownButton), new UIPropertyMetadata(PlacementMode.Bottom));

        public static readonly DependencyProperty IsDefaultProperty = DependencyProperty.Register("IsDefault", typeof(bool), typeof(DropDownButton), new UIPropertyMetadata(false, OnIsDefaultChanged));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(DropDownButton), new UIPropertyMetadata(false, OnIsOpenChanged));

        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double)
      , typeof(DropDownButton), new UIPropertyMetadata(SystemParameters.PrimaryScreenHeight / 2.0, OnMaxDropDownHeightChanged));

        public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent("Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DropDownButton));

        #endregion Public Fields

        #region Private Fields

        private const string PART_ContentPresenter = "PART_ContentPresenter";
        private const string PART_DropDownButton = "PART_DropDownButton";
        private const string PART_Popup = "PART_Popup";

        private System.Windows.Controls.Primitives.ButtonBase _button;
        private ContentPresenter _contentPresenter;
        private Popup _popup;

        // Keeps a copy of the CanExecuteChnaged handler so it doesn't get garbage collected.
        private EventHandler canExecuteChangedHandler;

        #endregion Private Fields

        #region Public Properties

        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }

        public object DropDownContent
        {
            get
            {
                return (object)GetValue(DropDownContentProperty);
            }
            set
            {
                SetValue(DropDownContentProperty, value);
            }
        }

        public Brush DropDownContentBackground
        {
            get
            {
                return (Brush)GetValue(DropDownContentBackgroundProperty);
            }
            set
            {
                SetValue(DropDownContentBackgroundProperty, value);
            }
        }

        public PlacementMode DropDownPosition
        {
            get
            {
                return (PlacementMode)GetValue(DropDownPositionProperty);
            }
            set
            {
                SetValue(DropDownPositionProperty, value);
            }
        }

        public bool IsDefault
        {
            get
            {
                return (bool)GetValue(IsDefaultProperty);
            }
            set
            {
                SetValue(IsDefaultProperty, value);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public double MaxDropDownHeight
        {
            get
            {
                return (double)GetValue(MaxDropDownHeightProperty);
            }
            set
            {
                SetValue(MaxDropDownHeightProperty, value);
            }
        }

        #endregion Public Properties

        #region Protected Properties

        protected System.Windows.Controls.Primitives.ButtonBase Button
        {
            get
            {
                return _button;
            }
            set
            {
                if (_button != null)
                    _button.Click -= DropDownButton_Click;

                _button = value;

                if (_button != null)
                    _button.Click += DropDownButton_Click;
            }
        }

        #endregion Protected Properties

        #region Public Constructors

        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));

            EventManager.RegisterClassHandler(typeof(DropDownButton), AccessKeyManager.AccessKeyPressedEvent, new AccessKeyPressedEventHandler(OnAccessKeyPressed));
        }

        public DropDownButton()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        #endregion Public Constructors

        #region Public Events

        public event RoutedEventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        public event RoutedEventHandler Closed
        {
            add
            {
                AddHandler(ClosedEvent, value);
            }
            remove
            {
                RemoveHandler(ClosedEvent, value);
            }
        }

        public event RoutedEventHandler Opened
        {
            add
            {
                AddHandler(OpenedEvent, value);
            }
            remove
            {
                RemoveHandler(OpenedEvent, value);
            }
        }

        #endregion Public Events

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Button = this.GetTemplateChild(PART_DropDownButton) as ToggleButton;

            _contentPresenter = GetTemplateChild(PART_ContentPresenter) as ContentPresenter;

            if (_popup != null)
                _popup.Opened -= Popup_Opened;

            _popup = GetTemplateChild(PART_Popup) as Popup;

            if (_popup != null)
                _popup.Opened += Popup_Opened;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnAccessKey(AccessKeyEventArgs e)
        {
            if (e.IsMultiple)
            {
                base.OnAccessKey(e);
            }
            else
            {
                this.OnClick();
            }
        }

        protected virtual void OnClick()
        {
            RaiseRoutedEvent(DropDownButton.ClickEvent);
            RaiseCommand();
        }

        protected virtual void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            // If old command is not null, then we need to remove the handlers.
            if (oldValue != null)
                UnhookCommand(oldValue, newValue);

            HookUpCommand(oldValue, newValue);

            CanExecuteChanged(); //may need to call this when changing the command parameter or target.
        }

        protected virtual void OnDropDownContentChanged(object oldValue, object newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (this.Button != null)
            {
                this.Button.Focus();
            }
        }

        protected virtual void OnIsDefaultChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                AccessKeyManager.Register("\r", this);
            }
            else
            {
                AccessKeyManager.Unregister("\r", this);
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (!(bool)e.NewValue)
            {
                this.CloseDropDown(false);
            }
        }

        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                RaiseRoutedEvent(DropDownButton.OpenedEvent);
            else
                RaiseRoutedEvent(DropDownButton.ClosedEvent);
        }

        protected virtual void OnMaxDropDownHeightChanged(double oldValue, double newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion Protected Methods

        #region Private Methods

        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {
            if (!e.Handled && (e.Scope == null) && (e.Target == null))
            {
                e.Target = sender as DropDownButton;
            }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DropDownButton dropDownButton = d as DropDownButton;
            if (dropDownButton != null)
                dropDownButton.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private static void OnDropDownContentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DropDownButton dropDownButton = o as DropDownButton;
            if (dropDownButton != null)
                dropDownButton.OnDropDownContentChanged((object)e.OldValue, (object)e.NewValue);
        }

        private static void OnIsDefaultChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dropDownButton = o as DropDownButton;
            if (dropDownButton != null)
                dropDownButton.OnIsDefaultChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DropDownButton dropDownButton = o as DropDownButton;
            if (dropDownButton != null)
                dropDownButton.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnMaxDropDownHeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dropDownButton = o as DropDownButton;
            if (dropDownButton != null)
                dropDownButton.OnMaxDropDownHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            CanExecuteChanged();
        }

        private void CanExecuteChanged()
        {
            if (Command != null)
            {
                RoutedCommand command = Command as RoutedCommand;

                // If a RoutedCommand.
                if (command != null)
                    IsEnabled = command.CanExecute(CommandParameter, CommandTarget) ? true : false;
                // If a not RoutedCommand.
                else
                    IsEnabled = Command.CanExecute(CommandParameter) ? true : false;
            }
        }

        /// <summary>
        /// Closes the drop down.
        /// </summary>
        private void CloseDropDown(bool isFocusOnButton)
        {
            if (IsOpen)
            {
                IsOpen = false;
            }
            ReleaseMouseCapture();

            if (isFocusOnButton && (this.Button != null))
            {
                Button.Focus();
            }
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            OnClick();
        }

        /// <summary>
        /// Hooks up a command to the CanExecuteChnaged event handler.
        /// </summary>
        /// <param name="oldCommand">The old command.</param>
        /// <param name="newCommand">The new command.</param>
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if (newCommand != null)
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    IsOpen = true;
                    // ContentPresenter items will get focus in Popup_Opened().
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
            }
        }

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsMouseCaptureWithin)
            {
                this.CloseDropDown(true);
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            // Set the focus on the content of the ContentPresenter.
            if (_contentPresenter != null)
            {
                _contentPresenter.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }
        }

        /// <summary>
        /// Raises the command's Execute event.
        /// </summary>
        private void RaiseCommand()
        {
            if (Command != null)
            {
                RoutedCommand routedCommand = Command as RoutedCommand;

                if (routedCommand == null)
                    ((ICommand)Command).Execute(CommandParameter);
                else
                    routedCommand.Execute(CommandParameter, CommandTarget);
            }
        }

        /// <summary>
        /// Raises routed events.
        /// </summary>
        private void RaiseRoutedEvent(RoutedEvent routedEvent)
        {
            RoutedEventArgs args = new RoutedEventArgs(routedEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// Unhooks a command from the Command property.
        /// </summary>
        /// <param name="oldCommand">The old command.</param>
        /// <param name="newCommand">The new command.</param>
        private void UnhookCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        #endregion Private Methods
    }
}