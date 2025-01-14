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
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit
{
    [TemplatePart(Name = PART_SpectrumDisplay, Type = typeof(Rectangle))]
    public class ColorSpectrumSlider : Slider
    {
        #region Public Fields

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorSpectrumSlider), new PropertyMetadata(System.Windows.Media.Colors.Transparent));

        #endregion Public Fields

        #region Private Fields

        private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";

        private LinearGradientBrush _pickerBrush;
        private Rectangle _spectrumDisplay;

        #endregion Private Fields

        #region Public Properties

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        static ColorSpectrumSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpectrumSlider), new FrameworkPropertyMetadata(typeof(ColorSpectrumSlider)));
        }

        #endregion Public Constructors

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _spectrumDisplay = (Rectangle)GetTemplateChild(PART_SpectrumDisplay);
            CreateSpectrum();
            OnValueChanged(Double.NaN, Value);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            Color color = ColorUtilities.ConvertHsvToRgb(360 - newValue, 1, 1);
            SelectedColor = color;
        }

        #endregion Protected Methods

        #region Private Methods

        private void CreateSpectrum()
        {
            _pickerBrush = new LinearGradientBrush();
            _pickerBrush.StartPoint = new Point(0.5, 0);
            _pickerBrush.EndPoint = new Point(0.5, 1);
            _pickerBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

            var colorsList = ColorUtilities.GenerateHsvSpectrum();

            double stopIncrement = (double)1 / (colorsList.Count - 1);

            int i;
            for (i = 0; i < colorsList.Count; i++)
            {
                _pickerBrush.GradientStops.Add(new GradientStop(colorsList[i], i * stopIncrement));
            }

            _pickerBrush.GradientStops[i - 1].Offset = 1.0;
            if (_spectrumDisplay != null)
            {
                _spectrumDisplay.Fill = _pickerBrush;
            }
        }

        #endregion Private Methods
    }
}