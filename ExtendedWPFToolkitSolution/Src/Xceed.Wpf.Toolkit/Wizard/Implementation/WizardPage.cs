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
using System.Windows.Input;
using System.Windows.Media;

namespace Xceed.Wpf.Toolkit
{
    public class WizardPage : ContentControl
    {
        #region Public Fields

        public static readonly DependencyProperty BackButtonVisibilityProperty = DependencyProperty.Register("BackButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));
        public static readonly DependencyProperty CanCancelProperty = DependencyProperty.Register("CanCancel", typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CancelButtonVisibilityProperty = DependencyProperty.Register("CancelButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty CanFinishProperty = DependencyProperty.Register("CanFinish", typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanHelpProperty = DependencyProperty.Register("CanHelp", typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanSelectNextPageProperty = DependencyProperty.Register("CanSelectNextPage", typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanSelectPreviousPageProperty = DependencyProperty.Register("CanSelectPreviousPage", typeof(bool?), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(WizardPage));

        public static readonly RoutedEvent EnterEvent = EventManager.RegisterRoutedEvent("Enter", RoutingStrategy.Bubble, typeof(EventHandler), typeof(WizardPage));

        public static readonly DependencyProperty ExteriorPanelBackgroundProperty = DependencyProperty.Register("ExteriorPanelBackground", typeof(Brush), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ExteriorPanelContentProperty = DependencyProperty.Register("ExteriorPanelContent", typeof(object), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty FinishButtonVisibilityProperty = DependencyProperty.Register("FinishButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(WizardPage), new UIPropertyMetadata(Brushes.White));

        public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.Register("HeaderImage", typeof(ImageSource), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HelpButtonVisibilityProperty = DependencyProperty.Register("HelpButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly RoutedEvent LeaveEvent = EventManager.RegisterRoutedEvent("Leave", RoutingStrategy.Bubble, typeof(EventHandler), typeof(WizardPage));

        public static readonly DependencyProperty NextButtonVisibilityProperty = DependencyProperty.Register("NextButtonVisibility", typeof(WizardPageButtonVisibility), typeof(WizardPage), new UIPropertyMetadata(WizardPageButtonVisibility.Inherit));

        public static readonly DependencyProperty NextPageProperty = DependencyProperty.Register("NextPage", typeof(WizardPage), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PageTypeProperty = DependencyProperty.Register("PageType", typeof(WizardPageType), typeof(WizardPage), new UIPropertyMetadata(WizardPageType.Exterior));

        public static readonly DependencyProperty PreviousPageProperty = DependencyProperty.Register("PreviousPage", typeof(WizardPage), typeof(WizardPage), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WizardPage));

        #endregion Public Fields

        #region Public Properties

        public WizardPageButtonVisibility BackButtonVisibility
        {
            get
            {
                return (WizardPageButtonVisibility)GetValue(BackButtonVisibilityProperty);
            }
            set
            {
                SetValue(BackButtonVisibilityProperty, value);
            }
        }

        public bool? CanCancel
        {
            get
            {
                return (bool?)GetValue(CanCancelProperty);
            }
            set
            {
                SetValue(CanCancelProperty, value);
            }
        }

        public WizardPageButtonVisibility CancelButtonVisibility
        {
            get
            {
                return (WizardPageButtonVisibility)GetValue(CancelButtonVisibilityProperty);
            }
            set
            {
                SetValue(CancelButtonVisibilityProperty, value);
            }
        }

        public bool? CanFinish
        {
            get
            {
                return (bool?)GetValue(CanFinishProperty);
            }
            set
            {
                SetValue(CanFinishProperty, value);
            }
        }

        public bool? CanHelp
        {
            get
            {
                return (bool?)GetValue(CanHelpProperty);
            }
            set
            {
                SetValue(CanHelpProperty, value);
            }
        }

        public bool? CanSelectNextPage
        {
            get
            {
                return (bool?)GetValue(CanSelectNextPageProperty);
            }
            set
            {
                SetValue(CanSelectNextPageProperty, value);
            }
        }

        public bool? CanSelectPreviousPage
        {
            get
            {
                return (bool?)GetValue(CanSelectPreviousPageProperty);
            }
            set
            {
                SetValue(CanSelectPreviousPageProperty, value);
            }
        }

        public string Description
        {
            get
            {
                return (string)base.GetValue(DescriptionProperty);
            }
            set
            {
                base.SetValue(DescriptionProperty, value);
            }
        }

        public Brush ExteriorPanelBackground
        {
            get
            {
                return (Brush)GetValue(ExteriorPanelBackgroundProperty);
            }
            set
            {
                SetValue(ExteriorPanelBackgroundProperty, value);
            }
        }

        public object ExteriorPanelContent
        {
            get
            {
                return (object)GetValue(ExteriorPanelContentProperty);
            }
            set
            {
                SetValue(ExteriorPanelContentProperty, value);
            }
        }

        public WizardPageButtonVisibility FinishButtonVisibility
        {
            get
            {
                return (WizardPageButtonVisibility)GetValue(FinishButtonVisibilityProperty);
            }
            set
            {
                SetValue(FinishButtonVisibilityProperty, value);
            }
        }

        public Brush HeaderBackground
        {
            get
            {
                return (Brush)GetValue(HeaderBackgroundProperty);
            }
            set
            {
                SetValue(HeaderBackgroundProperty, value);
            }
        }

        public ImageSource HeaderImage
        {
            get
            {
                return (ImageSource)GetValue(HeaderImageProperty);
            }
            set
            {
                SetValue(HeaderImageProperty, value);
            }
        }

        public WizardPageButtonVisibility HelpButtonVisibility
        {
            get
            {
                return (WizardPageButtonVisibility)GetValue(HelpButtonVisibilityProperty);
            }
            set
            {
                SetValue(HelpButtonVisibilityProperty, value);
            }
        }

        public WizardPageButtonVisibility NextButtonVisibility
        {
            get
            {
                return (WizardPageButtonVisibility)GetValue(NextButtonVisibilityProperty);
            }
            set
            {
                SetValue(NextButtonVisibilityProperty, value);
            }
        }

        public WizardPage NextPage
        {
            get
            {
                return (WizardPage)GetValue(NextPageProperty);
            }
            set
            {
                SetValue(NextPageProperty, value);
            }
        }

        public WizardPageType PageType
        {
            get
            {
                return (WizardPageType)GetValue(PageTypeProperty);
            }
            set
            {
                SetValue(PageTypeProperty, value);
            }
        }

        public WizardPage PreviousPage
        {
            get
            {
                return (WizardPage)GetValue(PreviousPageProperty);
            }
            set
            {
                SetValue(PreviousPageProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string)base.GetValue(TitleProperty);
            }
            set
            {
                base.SetValue(TitleProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static WizardPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardPage), new FrameworkPropertyMetadata(typeof(WizardPage)));
        }

        public WizardPage()
        {
            this.Loaded += new RoutedEventHandler(WizardPage_Loaded);
            this.Unloaded += new RoutedEventHandler(WizardPage_Unloaded);
        }

        #endregion Public Constructors

        #region Public Events

        public event RoutedEventHandler Enter
        {
            add
            {
                AddHandler(EnterEvent, value);
            }
            remove
            {
                RemoveHandler(EnterEvent, value);
            }
        }

        public event RoutedEventHandler Leave
        {
            add
            {
                AddHandler(LeaveEvent, value);
            }
            remove
            {
                RemoveHandler(LeaveEvent, value);
            }
        }

        #endregion Public Events

        #region Protected Methods

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if ((e.Property.Name == "CanSelectNextPage") || (e.Property.Name == "CanHelp") || (e.Property.Name == "CanFinish")
              || (e.Property.Name == "CanCancel") || (e.Property.Name == "CanSelectPreviousPage"))
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void WizardPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsVisible)
            {
                base.RaiseEvent(new RoutedEventArgs(WizardPage.EnterEvent, this));
            }
        }

        private void WizardPage_Unloaded(object sender, RoutedEventArgs e)
        {
            base.RaiseEvent(new RoutedEventArgs(WizardPage.LeaveEvent, this));
        }

        #endregion Private Methods
    }
}