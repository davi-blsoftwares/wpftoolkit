﻿/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

namespace Xceed.Wpf.Toolkit.Primitives
{
    internal struct HsvColor
    {
        #region Public Fields

        public double H;
        public double S;
        public double V;

        #endregion Public Fields

        #region Public Constructors

        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }

        #endregion Public Constructors
    }
}