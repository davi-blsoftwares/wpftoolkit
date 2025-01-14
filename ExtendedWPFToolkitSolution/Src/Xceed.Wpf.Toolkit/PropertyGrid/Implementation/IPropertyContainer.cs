/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.ComponentModel;
using System.Windows;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    internal interface IPropertyContainer
    {
        #region Public Properties

        bool AutoGenerateProperties { get; }
        ContainerHelperBase ContainerHelper { get; }

        EditorDefinitionCollection EditorDefinitions { get; }
        FilterInfo FilterInfo { get; }
        bool HideInheritedProperties { get; }
        bool IsCategorized { get; }
        bool IsSortedAlphabetically { get; }
        Style PropertyContainerStyle { get; }
        PropertyDefinitionCollection PropertyDefinitions { get; }

        #endregion Public Properties

        #region Public Methods

        bool? IsPropertyVisible(PropertyDescriptor pd);

        #endregion Public Methods
    }
}