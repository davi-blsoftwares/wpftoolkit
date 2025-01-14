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

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    public class PropertyGridEditorTextBlock : TextBlock
    {
        #region Public Constructors

        static PropertyGridEditorTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorTextBlock), new FrameworkPropertyMetadata(typeof(PropertyGridEditorTextBlock)));
        }

        #endregion Public Constructors
    }

    public class TextBlockEditor : TypeEditor<TextBlock>
    {
        #region Protected Methods

        protected override TextBlock CreateEditor()
        {
            return new PropertyGridEditorTextBlock();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Margin = new System.Windows.Thickness(5, 0, 0, 0);
            Editor.TextTrimming = TextTrimming.CharacterEllipsis;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = TextBlock.TextProperty;
        }

        #endregion Protected Methods
    }
}