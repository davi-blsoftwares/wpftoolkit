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
using System.Linq.Expressions;
using System.Windows;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    public abstract class DefinitionBase : DependencyObject
    {
        #region Private Fields

        private bool _isLocked;

        #endregion Private Fields

        #region Internal Properties

        internal bool IsLocked
        {
            get { return _isLocked; }
        }

        #endregion Internal Properties

        #region Internal Methods

        internal virtual void Lock()
        {
            if (!_isLocked)
            {
                _isLocked = true;
            }
        }

        internal void ThrowIfLocked<TMember>(Expression<Func<TMember>> propertyExpression)
        {
            //In XAML, when using any properties of PropertyDefinition, the error of ThrowIfLocked is always thrown => prevent it !
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (this.IsLocked)
            {
                string propertyName = ReflectionHelper.GetPropertyOrFieldName(propertyExpression);
                string message = string.Format(
                    @"Cannot modify {0} once the definition has beed added to a collection.",
                    propertyName);
                throw new InvalidOperationException(message);
            }
        }

        #endregion Internal Methods
    }
}