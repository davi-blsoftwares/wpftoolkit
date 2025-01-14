/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using Xceed.Wpf.Toolkit.Media.Animation;

namespace Xceed.Wpf.Toolkit.Panels
{
    public static class Animators
    {
        #region Private Fields

        private static DoubleAnimator _backEaseIn;

        private static DoubleAnimator _backEaseInOut;

        private static DoubleAnimator _backEaseOut;

        private static DoubleAnimator _bounceEaseIn;

        private static DoubleAnimator _bounceEaseInOut;

        private static DoubleAnimator _bounceEaseOut;

        private static DoubleAnimator _circEaseIn;

        private static DoubleAnimator _circEaseInOut;

        private static DoubleAnimator _circEaseOut;

        private static DoubleAnimator _cubicEaseIn;

        private static DoubleAnimator _cubicEaseInOut;

        private static DoubleAnimator _cubicEaseOut;

        private static DoubleAnimator _elasticEaseIn;

        private static DoubleAnimator _elasticEaseInOut;

        private static DoubleAnimator _elasticEaseOut;

        private static DoubleAnimator _expoEaseIn;

        private static DoubleAnimator _expoEaseInOut;

        private static DoubleAnimator _expoEaseOut;

        private static DoubleAnimator _linear;

        private static DoubleAnimator _quadEaseIn;

        private static DoubleAnimator _quadEaseInOut;

        private static DoubleAnimator _quadEaseOut;

        private static DoubleAnimator _quartEaseIn;

        private static DoubleAnimator _quartEaseInOut;

        private static DoubleAnimator _quartEaseOut;

        private static DoubleAnimator _quintEaseIn;

        private static DoubleAnimator _quintEaseInOut;

        private static DoubleAnimator _quintEaseOut;

        private static DoubleAnimator _sineEaseIn;

        private static DoubleAnimator _sineEaseInOut;

        private static DoubleAnimator _sineEaseOut;

        #endregion Private Fields

        #region Public Properties

        public static DoubleAnimator BackEaseIn
        {
            get
            {
                if (_backEaseIn == null)
                {
                    _backEaseIn = new DoubleAnimator(PennerEquations.BackEaseIn);
                }
                return _backEaseIn;
            }
        }

        public static DoubleAnimator BackEaseInOut
        {
            get
            {
                if (_backEaseInOut == null)
                {
                    _backEaseInOut = new DoubleAnimator(PennerEquations.BackEaseInOut);
                }
                return _backEaseInOut;
            }
        }

        public static DoubleAnimator BackEaseOut
        {
            get
            {
                if (_backEaseOut == null)
                {
                    _backEaseOut = new DoubleAnimator(PennerEquations.BackEaseOut);
                }
                return _backEaseOut;
            }
        }

        public static DoubleAnimator BounceEaseIn
        {
            get
            {
                if (_bounceEaseIn == null)
                {
                    _bounceEaseIn = new DoubleAnimator(PennerEquations.BounceEaseIn);
                }
                return _bounceEaseIn;
            }
        }

        public static DoubleAnimator BounceEaseInOut
        {
            get
            {
                if (_bounceEaseInOut == null)
                {
                    _bounceEaseInOut = new DoubleAnimator(PennerEquations.BounceEaseInOut);
                }
                return _bounceEaseInOut;
            }
        }

        public static DoubleAnimator BounceEaseOut
        {
            get
            {
                if (_bounceEaseOut == null)
                {
                    _bounceEaseOut = new DoubleAnimator(PennerEquations.BounceEaseOut);
                }
                return _bounceEaseOut;
            }
        }

        public static DoubleAnimator CircEaseIn
        {
            get
            {
                if (_circEaseIn == null)
                {
                    _circEaseIn = new DoubleAnimator(PennerEquations.CircEaseIn);
                }
                return _circEaseIn;
            }
        }

        public static DoubleAnimator CircEaseInOut
        {
            get
            {
                if (_circEaseInOut == null)
                {
                    _circEaseInOut = new DoubleAnimator(PennerEquations.CircEaseInOut);
                }
                return _circEaseInOut;
            }
        }

        public static DoubleAnimator CircEaseOut
        {
            get
            {
                if (_circEaseOut == null)
                {
                    _circEaseOut = new DoubleAnimator(PennerEquations.CircEaseOut);
                }
                return _circEaseOut;
            }
        }

        public static DoubleAnimator CubicEaseIn
        {
            get
            {
                if (_cubicEaseIn == null)
                {
                    _cubicEaseIn = new DoubleAnimator(PennerEquations.CubicEaseIn);
                }
                return _cubicEaseIn;
            }
        }

        public static DoubleAnimator CubicEaseInOut
        {
            get
            {
                if (_cubicEaseInOut == null)
                {
                    _cubicEaseInOut = new DoubleAnimator(PennerEquations.CubicEaseInOut);
                }
                return _cubicEaseInOut;
            }
        }

        public static DoubleAnimator CubicEaseOut
        {
            get
            {
                if (_cubicEaseOut == null)
                {
                    _cubicEaseOut = new DoubleAnimator(PennerEquations.CubicEaseOut);
                }
                return _cubicEaseOut;
            }
        }

        public static DoubleAnimator ElasticEaseIn
        {
            get
            {
                if (_elasticEaseIn == null)
                {
                    _elasticEaseIn = new DoubleAnimator(PennerEquations.ElasticEaseIn);
                }
                return _elasticEaseIn;
            }
        }

        public static DoubleAnimator ElasticEaseInOut
        {
            get
            {
                if (_elasticEaseInOut == null)
                {
                    _elasticEaseInOut = new DoubleAnimator(PennerEquations.ElasticEaseInOut);
                }
                return _elasticEaseInOut;
            }
        }

        public static DoubleAnimator ElasticEaseOut
        {
            get
            {
                if (_elasticEaseOut == null)
                {
                    _elasticEaseOut = new DoubleAnimator(PennerEquations.ElasticEaseOut);
                }
                return _elasticEaseOut;
            }
        }

        public static DoubleAnimator ExpoEaseIn
        {
            get
            {
                if (_expoEaseIn == null)
                {
                    _expoEaseIn = new DoubleAnimator(PennerEquations.ExpoEaseIn);
                }
                return _expoEaseIn;
            }
        }

        public static DoubleAnimator ExpoEaseInOut
        {
            get
            {
                if (_expoEaseInOut == null)
                {
                    _expoEaseInOut = new DoubleAnimator(PennerEquations.ExpoEaseInOut);
                }
                return _expoEaseInOut;
            }
        }

        public static DoubleAnimator ExpoEaseOut
        {
            get
            {
                if (_expoEaseOut == null)
                {
                    _expoEaseOut = new DoubleAnimator(PennerEquations.ExpoEaseOut);
                }
                return _expoEaseOut;
            }
        }

        public static DoubleAnimator Linear
        {
            get
            {
                if (_linear == null)
                {
                    _linear = new DoubleAnimator(PennerEquations.Linear);
                }
                return _linear;
            }
        }

        public static DoubleAnimator QuadEaseIn
        {
            get
            {
                if (_quadEaseIn == null)
                {
                    _quadEaseIn = new DoubleAnimator(PennerEquations.QuadEaseIn);
                }
                return _quadEaseIn;
            }
        }

        public static DoubleAnimator QuadEaseInOut
        {
            get
            {
                if (_quadEaseInOut == null)
                {
                    _quadEaseInOut = new DoubleAnimator(PennerEquations.QuadEaseInOut);
                }
                return _quadEaseInOut;
            }
        }

        public static DoubleAnimator QuadEaseOut
        {
            get
            {
                if (_quadEaseOut == null)
                {
                    _quadEaseOut = new DoubleAnimator(PennerEquations.QuadEaseOut);
                }
                return _quadEaseOut;
            }
        }

        public static DoubleAnimator QuartEaseIn
        {
            get
            {
                if (_quartEaseIn == null)
                {
                    _quartEaseIn = new DoubleAnimator(PennerEquations.QuartEaseIn);
                }
                return _quartEaseIn;
            }
        }

        public static DoubleAnimator QuartEaseInOut
        {
            get
            {
                if (_quartEaseInOut == null)
                {
                    _quartEaseInOut = new DoubleAnimator(PennerEquations.QuartEaseInOut);
                }
                return _quartEaseInOut;
            }
        }

        public static DoubleAnimator QuartEaseOut
        {
            get
            {
                if (_quartEaseOut == null)
                {
                    _quartEaseOut = new DoubleAnimator(PennerEquations.QuartEaseOut);
                }
                return _quartEaseOut;
            }
        }

        public static DoubleAnimator QuintEaseIn
        {
            get
            {
                if (_quintEaseIn == null)
                {
                    _quintEaseIn = new DoubleAnimator(PennerEquations.QuintEaseIn);
                }
                return _quintEaseIn;
            }
        }

        public static DoubleAnimator QuintEaseInOut
        {
            get
            {
                if (_quintEaseInOut == null)
                {
                    _quintEaseInOut = new DoubleAnimator(PennerEquations.QuintEaseInOut);
                }
                return _quintEaseInOut;
            }
        }

        public static DoubleAnimator QuintEaseOut
        {
            get
            {
                if (_quintEaseOut == null)
                {
                    _quintEaseOut = new DoubleAnimator(PennerEquations.QuintEaseOut);
                }
                return _quintEaseOut;
            }
        }

        public static DoubleAnimator SineEaseIn
        {
            get
            {
                if (_sineEaseIn == null)
                {
                    _sineEaseIn = new DoubleAnimator(PennerEquations.SineEaseIn);
                }
                return _sineEaseIn;
            }
        }

        public static DoubleAnimator SineEaseInOut
        {
            get
            {
                if (_sineEaseInOut == null)
                {
                    _sineEaseInOut = new DoubleAnimator(PennerEquations.SineEaseInOut);
                }
                return _sineEaseInOut;
            }
        }

        public static DoubleAnimator SineEaseOut
        {
            get
            {
                if (_sineEaseOut == null)
                {
                    _sineEaseOut = new DoubleAnimator(PennerEquations.SineEaseOut);
                }
                return _sineEaseOut;
            }
        }

        #endregion Public Properties
    }
}