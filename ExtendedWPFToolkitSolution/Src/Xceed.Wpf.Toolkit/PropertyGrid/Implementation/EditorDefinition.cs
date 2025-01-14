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
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    [Obsolete(@"Use EditorTemplateDefinition instead of EditorDefinition. " + EditorDefinition.UsageEx)]
    public class EditorDefinition : EditorTemplateDefinition
    {
        #region Private Fields

        private const string UsageEx = " (XAML Ex: <t:EditorTemplateDefinition TargetProperties=\"FirstName,LastName\" .../> OR <t:EditorTemplateDefinition TargetProperties=\"{x:Type l:MyType}\" .../> )";

        private PropertyDefinitionCollection _properties = new PropertyDefinitionCollection();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the template of the editor.
        /// This Property is part of the obsolete EditorDefinition class.
        /// Use EditorTemplateDefinition class and the Edit<b>ing</b>Template property.
        /// </summary>
        public DataTemplate EditorTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// List the PropertyDefinitions that identify the properties targeted by the EditorTemplate.
        /// This Property is part of the obsolete EditorDefinition class.
        /// Use "EditorTemplateDefinition" class and the "TargetProperties" property<br/>
        /// XAML Ex.: &lt;t:EditorTemplateDefinition TargetProperties="FirstName,LastName" .../&gt;
        /// </summary>
        public PropertyDefinitionCollection PropertiesDefinitions
        {
            get
            {
                return _properties;
            }
            set
            {
                _properties = value;
            }
        }

        public Type TargetType
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Constructors

        public EditorDefinition()
        {
            const string usageErr = "{0} is obsolete. Instead use {1}.";
            System.Diagnostics.Trace.TraceWarning(string.Format(usageErr, typeof(EditorDefinition), typeof(EditorTemplateDefinition)) + UsageEx);
        }

        #endregion Public Constructors

        #region Internal Methods

        internal override void Lock()
        {
            const string usageError = @"Use a EditorTemplateDefinition instead of EditorDefinition in order to use the '{0}' property.";
            if (this.EditingTemplate != null)
                throw new InvalidOperationException(string.Format(usageError, "EditingTemplate"));

            if (this.TargetProperties != null && this.TargetProperties.Count > 0)
                throw new InvalidOperationException(string.Format(usageError, "TargetProperties"));

            List<object> properties = new List<object>();
            if (this.PropertiesDefinitions != null)
            {
                foreach (PropertyDefinition def in this.PropertiesDefinitions)
                {
                    if (def.TargetProperties != null)
                    {
                        properties.AddRange(def.TargetProperties.Cast<object>());
                    }
                }
            }

            this.TargetProperties = properties;
            this.EditingTemplate = this.EditorTemplate;

            base.Lock();
        }

        #endregion Internal Methods
    }
}