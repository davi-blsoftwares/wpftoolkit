/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Resources;

namespace Xceed.Wpf.Toolkit.Core
{
    internal static class ErrorMessages
    {
        #region Public Fields

        public const string AlreadyInColumnCollection = "Value already belongs to another 'ColumnDefinitionCollection'.";
        public const string AlreadyInRowCollection = "Value already belongs to another 'RowDefinitionCollection'.";
        public const string AlreadyInStackDefinition = "Value already belongs to another 'StackDefinitionCollection'.";
        public const string AnimationAccelerationRatioOOR = "AnimationAccelerationRatioOOR";
        public const string AnimationDecelerationRatioOOR = "AnimationDecelerationRatioOOR";
        public const string ArrayDestTooShort = "'array' destination not long enough.";
        public const string CollectionDisposed = "Collection was disposed, enumerator operations not valid.";
        public const string CollectionModified = "Collection was modified; enumeration operation may not execute.";
        public const string ColumnValueIsReadOnly = "Cannot modify 'ColumnDefinitionCollection' in read-only state.";
        public const string DefaultAnimationRateAnimationRateDefault = "DefaultAnimationRateAnimationRateDefault";
        public const string DefaultAnimatorCantAnimate = "DefaultAnimatorCantAnimate";
        public const string DefaultAnimatorIterativeAnimationDefault = "DefaultAnimatorIterativeAnimationDefault";
        public const string DestMultidimensional = "Destination is multidimensional. Expected array of rank 1.";
        public const string EndAngleCannotBeSetDirectlyInSlice = "EndAngleCannotBeSetDirectlyInSlice";
        public const string EnumerationFinished = "Enumeration already finished.";
        public const string EnumerationNotStarted = "Enumeration has not started. Call MoveNext.";
        public const string InvalidDefaultStackLength = "The default stack length must be Auto or an explicit value.";
        public const string InvalidRatePropertyAccessed = "InvalidRatePropertyAccessed";
        public const string MustBeColumnDefinition = "'ColumnDefinitionCollection' must be type 'ColumnDefinition'.";
        public const string MustBeRowDefinition = "'RowDefinitionCollection' must be type 'RowDefinition'.";
        public const string MustBeStackDefinition = "'StackDefinitionCollection' must be type 'StackDefinition'.";
        public const string NegativeSpeedNotSupported = "NegativeSpeedNotSupported";
        public const string NegativeTimeSpanNotSupported = "NegativeTimeSpanNotSupported";
        public const string PositionOnlyAccessibleOnAbsolute = "PositionOnlyAccessibleOnAbsolute";
        public const string RegionOnlyAccessibleOnRegionalView = "RegionOnlyAccessibleOnRegionalView";
        public const string RowValueIsReadOnly = "Cannot modify 'StackDefinitionCollection' in read-only state.";
        public const string ScaleOnlyAccessibleOnAbsolute = "ScaleOnlyAccessibleOnAbsolute";
        public const string SliceCannotBeSetDirectlyInEndAngle = "SliceCannotBeSetDirectlyInEndAngle";
        public const string SliceOOR = "SliceOOR";
        public const string StackValueIsReadOnly = "Cannot modify 'StackDefinitionCollection' in read-only state.";
        public const string SuppliedValueWasNotVisibility = "SuppliedValueWasNotVisibility";
        public const string UnableToConvertToZoomboxView = "UnableToConvertToZoomboxView";
        public const string UnexpectedType = "Expected type '{0}', got '{1}'.";
        public const string ViewModeInvalidForSource = "ViewModeInvalidForSource";
        public const string ViewStackCannotBeManipulatedNow = "ViewStackCannotBeManipulatedNow";
        public const string ZoomboxContentMustBeUIElement = "ZoomboxContentMustBeUIElement";
        public const string ZoomboxHasViewFinderButNotDisplay = "ZoomboxHasViewFinderButNotDisplay";
        public const string ZoomboxTemplateNeedsContent = "ZoomboxTemplateNeedsContent";
        public const string ZoomboxViewAlreadyInitialized = "ZoomboxViewAlreadyInitialized";

        #endregion Public Fields

        #region Private Fields

        private static readonly ResourceManager _resourceManager;

        #endregion Private Fields

        #region Public Constructors

        static ErrorMessages()
        {
            _resourceManager = new ResourceManager("Xceed.Wpf.Toolkit.Core.ErrorMessages", typeof(ErrorMessages).Assembly);
        }

        #endregion Public Constructors

        #region Public Methods

        public static string GetMessage(string msgId)
        {
            return _resourceManager.GetString(msgId);
        }

        #endregion Public Methods
    }
}