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
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Xceed.Wpf.Toolkit.Core.Utilities
{
    public class ContextMenuUtilities
    {
        #region Public Fields

        public static readonly DependencyProperty OpenOnMouseLeftButtonClickProperty = DependencyProperty.RegisterAttached("OpenOnMouseLeftButtonClick", typeof(bool), typeof(ContextMenuUtilities), new FrameworkPropertyMetadata(false, OpenOnMouseLeftButtonClickChanged));

        #endregion Public Fields

        #region Public Methods

        public static bool GetOpenOnMouseLeftButtonClick(FrameworkElement element)
        {
            return (bool)element.GetValue(OpenOnMouseLeftButtonClickProperty);
        }

        public static void OpenOnMouseLeftButtonClickChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control != null)
            {
                if ((bool)e.NewValue)
                {
                    control.PreviewMouseLeftButtonDown += ContextMenuUtilities.Control_PreviewMouseLeftButtonDown;
                }
                else
                {
                    control.PreviewMouseLeftButtonDown -= ContextMenuUtilities.Control_PreviewMouseLeftButtonDown;
                }
            }
        }

        public static void SetOpenOnMouseLeftButtonClick(FrameworkElement element, bool value)
        {
            element.SetValue(OpenOnMouseLeftButtonClickProperty, value);
        }

        #endregion Public Methods

        #region Private Methods

        private static void Control_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var control = sender as FrameworkElement;
            if ((control != null) && (control.ContextMenu != null))
            {
                // Get PropertyItemBase parent
                var parent = VisualTreeHelper.GetParent(control);
                while (parent != null)
                {
                    var propertyItemBase = parent as PropertyItemBase;
                    if (propertyItemBase != null)
                    {
                        // Set the ContextMenu.DataContext to the PropertyItem associated to the clicked image.
                        control.ContextMenu.DataContext = propertyItemBase;
                        break;
                    }
                    parent = VisualTreeHelper.GetParent(parent);
                }

                control.ContextMenu.PlacementTarget = control;
                control.ContextMenu.IsOpen = true;
            }
        }

        #endregion Private Methods
    }
}