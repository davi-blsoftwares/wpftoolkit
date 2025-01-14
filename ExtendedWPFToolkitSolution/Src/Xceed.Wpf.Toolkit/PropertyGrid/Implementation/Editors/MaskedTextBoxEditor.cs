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

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    public class MaskedTextBoxEditor : TypeEditor<MaskedTextBox>
    {
        #region Public Properties

        public string Mask
        {
            get;
            set;
        }

        public Type ValueDataType
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Protected Methods

        protected override MaskedTextBox CreateEditor()
        {
            return new PropertyGridEditorMaskedTextBox();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.BorderThickness = new System.Windows.Thickness(0);
            this.Editor.ValueDataType = this.ValueDataType;
            this.Editor.Mask = this.Mask;
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = MaskedTextBox.ValueProperty;
        }

        #endregion Protected Methods
    }

    public class PropertyGridEditorMaskedTextBox : MaskedTextBox
    {
        #region Public Constructors

        static PropertyGridEditorMaskedTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorMaskedTextBox), new FrameworkPropertyMetadata(typeof(PropertyGridEditorMaskedTextBox)));
        }

        #endregion Public Constructors
    }
}