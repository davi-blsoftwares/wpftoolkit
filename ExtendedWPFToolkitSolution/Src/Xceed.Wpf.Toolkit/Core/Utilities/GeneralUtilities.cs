﻿/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.Core.Utilities
{
    internal sealed class GeneralUtilities : DependencyObject
    {
        #region Internal Fields

        internal static readonly DependencyProperty StubValueProperty = DependencyProperty.RegisterAttached(
          "StubValue",
          typeof(object),
          typeof(GeneralUtilities),
          new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion Internal Fields

        #region Private Constructors

        private GeneralUtilities()
        { }

        #endregion Private Constructors

        #region Public Methods

        public static object GetBindingValue(object sourceObject, Binding binding)
        {
            Binding bindingClone = new Binding()
            {
                BindsDirectlyToSource = binding.BindsDirectlyToSource,
                Converter = binding.Converter,
                ConverterCulture = binding.ConverterCulture,
                ConverterParameter = binding.ConverterParameter,
                FallbackValue = binding.FallbackValue,
                Mode = BindingMode.OneTime,
                Path = binding.Path,
                StringFormat = binding.StringFormat,
                TargetNullValue = binding.TargetNullValue,
                XPath = binding.XPath
            };

            bindingClone.Source = sourceObject;

            var targetObj = new GeneralUtilities();
            BindingOperations.SetBinding(targetObj, GeneralUtilities.StubValueProperty, bindingClone);
            object value = GeneralUtilities.GetStubValue(targetObj);
            BindingOperations.ClearBinding(targetObj, GeneralUtilities.StubValueProperty);
            return value;
        }

        public static object GetPathValue(object sourceObject, string path)
        {
            var targetObj = new GeneralUtilities();
            BindingOperations.SetBinding(targetObj, GeneralUtilities.StubValueProperty, new Binding(path) { Source = sourceObject });
            object value = GeneralUtilities.GetStubValue(targetObj);
            BindingOperations.ClearBinding(targetObj, GeneralUtilities.StubValueProperty);
            return value;
        }

        #endregion Public Methods

        #region Internal Methods

        internal static bool CanConvertValue(object value, object targetType)
        {
            return ((value != null)
                    && (!object.Equals(value.GetType(), targetType))
                    && (!object.Equals(targetType, typeof(object))));
        }

        internal static object GetStubValue(DependencyObject obj)
        {
            return (object)obj.GetValue(GeneralUtilities.StubValueProperty);
        }

        internal static void SetStubValue(DependencyObject obj, object value)
        {
            obj.SetValue(GeneralUtilities.StubValueProperty, value);
        }

        #endregion Internal Methods
    }
}