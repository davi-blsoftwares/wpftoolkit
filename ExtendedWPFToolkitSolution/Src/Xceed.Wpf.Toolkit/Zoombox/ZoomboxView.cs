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
using System.Windows;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace Xceed.Wpf.Toolkit.Zoombox
{
    [TypeConverter(typeof(ZoomboxViewConverter))]
    public class ZoomboxView
    {
        #region Private Fields

        private static readonly ZoomboxView _center = new ZoomboxView(ZoomboxViewKind.Center);

        private static readonly ZoomboxView _empty = new ZoomboxView(ZoomboxViewKind.Empty);

        private static readonly ZoomboxView _fill = new ZoomboxView(ZoomboxViewKind.Fill);

        private static readonly ZoomboxView _fit = new ZoomboxView(ZoomboxViewKind.Fit);

        private double _kindHeight = (int)ZoomboxViewKind.Empty;

        private double _scaleWidth = double.NaN;

        private double _x = double.NaN;

        private double _y = double.NaN;

        #endregion Private Fields

        #region Public Properties

        public static ZoomboxView Center
        {
            get
            {
                return _center;
            }
        }

        public static ZoomboxView Empty
        {
            get
            {
                return _empty;
            }
        }

        public static ZoomboxView Fill
        {
            get
            {
                return _fill;
            }
        }

        public static ZoomboxView Fit
        {
            get
            {
                return _fit;
            }
        }

        public Point Position
        {
            get
            {
                if (this.ViewKind != ZoomboxViewKind.Absolute)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("PositionOnlyAccessibleOnAbsolute"));

                return new Point(_x, _y);
            }
            set
            {
                if (this.ViewKind != ZoomboxViewKind.Absolute && this.ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), this.ViewKind.ToString()));

                _x = value.X;
                _y = value.Y;
                _kindHeight = (int)ZoomboxViewKind.Absolute;
            }
        }

        public Rect Region
        {
            get
            {
                // a region view has a positive _typeHeight value
                if (_kindHeight < 0)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("RegionOnlyAccessibleOnRegionalView"));

                return new Rect(_x, _y, _scaleWidth, _kindHeight);
            }
            set
            {
                if (this.ViewKind != ZoomboxViewKind.Region && this.ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), this.ViewKind.ToString()));

                if (!value.IsEmpty)
                {
                    _x = value.X;
                    _y = value.Y;
                    _scaleWidth = value.Width;
                    _kindHeight = value.Height;
                }
            }
        }

        public double Scale
        {
            get
            {
                if (this.ViewKind != ZoomboxViewKind.Absolute)
                    throw new InvalidOperationException(ErrorMessages.GetMessage("ScaleOnlyAccessibleOnAbsolute"));

                return _scaleWidth;
            }
            set
            {
                if (this.ViewKind != ZoomboxViewKind.Absolute && this.ViewKind != ZoomboxViewKind.Empty)
                    throw new InvalidOperationException(String.Format(ErrorMessages.GetMessage("ZoomboxViewAlreadyInitialized"), this.ViewKind.ToString()));

                _scaleWidth = value;
                _kindHeight = (int)ZoomboxViewKind.Absolute;
            }
        }

        public ZoomboxViewKind ViewKind
        {
            get
            {
                if (_kindHeight > 0)
                {
                    return ZoomboxViewKind.Region;
                }
                else
                {
                    return (ZoomboxViewKind)(int)_kindHeight;
                }
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public ZoomboxView()
        {
        }

        public ZoomboxView(double scale)
        {
            this.Scale = scale;
        }

        public ZoomboxView(Point position)
        {
            this.Position = position;
        }

        public ZoomboxView(double scale, Point position)
        {
            this.Position = position;
            this.Scale = scale;
        }

        public ZoomboxView(Rect region)
        {
            this.Region = region;
        }

        public ZoomboxView(double x, double y)
          : this(new Point(x, y))
        {
        }

        public ZoomboxView(double scale, double x, double y)
          : this(scale, new Point(x, y))
        {
        }

        public ZoomboxView(double x, double y, double width, double height)
          : this(new Rect(x, y, width, height))
        {
        }

        #endregion Public Constructors

        #region Private Constructors

        private ZoomboxView(ZoomboxViewKind viewType)
        {
            _kindHeight = (int)viewType;
        }

        #endregion Private Constructors

        #region Public Methods

        public static bool operator !=(ZoomboxView v1, ZoomboxView v2)
        {
            return !(v1 == v2);
        }

        public static bool operator ==(ZoomboxView v1, ZoomboxView v2)
        {
            if ((object)v1 == null)
                return (object)v2 == null;

            if ((object)v2 == null)
                return (object)v1 == null;

            return v1.Equals(v2);
        }

        public override bool Equals(object o)
        {
            bool result = false;
            if (o is ZoomboxView)
            {
                ZoomboxView other = (ZoomboxView)o;
                if (this.ViewKind == other.ViewKind)
                {
                    switch (this.ViewKind)
                    {
                        case ZoomboxViewKind.Absolute:
                            result = (DoubleHelper.AreVirtuallyEqual(_scaleWidth, other._scaleWidth))
                                  && (DoubleHelper.AreVirtuallyEqual(Position, other.Position));
                            break;

                        case ZoomboxViewKind.Region:
                            result = DoubleHelper.AreVirtuallyEqual(Region, other.Region);
                            break;

                        default:
                            result = true;
                            break;
                    }
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _scaleWidth.GetHashCode() ^ _kindHeight.GetHashCode();
        }

        public override string ToString()
        {
            switch (ViewKind)
            {
                case ZoomboxViewKind.Empty:
                    return "ZoomboxView: Empty";

                case ZoomboxViewKind.Center:
                    return "ZoomboxView: Center";

                case ZoomboxViewKind.Fill:
                    return "ZoomboxView: Fill";

                case ZoomboxViewKind.Fit:
                    return "ZoomboxView: Fit";

                case ZoomboxViewKind.Absolute:
                    return string.Format("ZoomboxView: Scale = {0}; Position = ({1}, {2})", _scaleWidth.ToString("f"), _x.ToString("f"), _y.ToString("f"));

                case ZoomboxViewKind.Region:
                    return string.Format("ZoomboxView: Region = ({0}, {1}, {2}, {3})", _x.ToString("f"), _y.ToString("f"), _scaleWidth.ToString("f"), _kindHeight.ToString("f"));
            }

            return base.ToString();
        }

        #endregion Public Methods
    }
}