#if !HIGHPRECISION
using Real = System.Single;
#else
using Real = System.Double;
#endif
using System;
using System.Collections.Generic;

namespace Lumix
{
    public static class Mathf
    {
        private static System.Random random_ = new Random();
        public static Real PI = (Real)System.Math.PI;
        public static Real TwoPI = 2 * PI;
        public static Real HalfPI = (Real)(PI / 2.0);
        public static Real Epsilon = Real.Epsilon;
        public static Real PosInfinity = Real.PositiveInfinity;
        public static Real NegInfinity = Real.NegativeInfinity;
        /// <summary>
        /// Returns the absolute value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Real Abs(Real value)
        {
            return System.Math.Abs(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="clampedValue"></param>
        /// <returns></returns>
        public static T Clamp<T>(T value, T max, T min) where T : struct, IComparable
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            else if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="clampedValue"></param>
        /// <returns></returns>
        public static Real Clamp(Real value, Real clampedValue)
        {
            return (Epsilon > Abs(value - clampedValue)) ? clampedValue : value;
        }

        /// <summary>
        /// Generate a random number of unit length
        /// </summary>
        /// <returns>a random number in the range from [0,1]</returns>
        public static Real UnitRandom()
        {
            return (Real)random_.NextDouble();
        }

        /// <summary>
        /// Generate a random number within the range provided.
        /// </summary>
        /// <param name="low">The lower bound of the range.</param>
        /// <param name="high">The upper bound of the range.</param>
        /// <returns>A random number in the range from [low,high].</returns>
        public static Real RangeRandom(Real _low, Real _high)
        {
            return (_high - _low) * UnitRandom() + _low;
        }

        /// <summary>
        /// Generate a random number in the range [-1,1].
        /// </summary>
        /// <returns>A random number in the range from [-1,1].</returns>
        public static Real SymmetricRandom()
        {
            return (Real)(2.0 * UnitRandom() - 1.0);
        }

        /// <summary>
        /// Returns the square root of a specific number
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Real Sqrt(Real _value)
        {
            return (Real)System.Math.Sqrt(_value);
        }

        /// <summary>
        /// returns the sine of the specified angle
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Real Sin(Real _value)
        {
            return (Real)System.Math.Sin(_value);
        }

        /// <summary>
        /// returns the cosine of the specified angle
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Real Cos(Real _value)
        {
            return (Real)System.Math.Cos(_value);
        }

        /// <summary>
        /// Returns the tangent of the specified _value.
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Real Tan(Real _value)
        {
            return (Real)System.Math.Tan(_value);
        }
        //extensios

        /// <summary>
        /// To the radians.
        /// </summary>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static float ToRadians(this float _value)
        {
            return (float)(_value * 0.01745329251994329576923690768489);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Real Max(Real _left, Real _right)
        {
            return System.Math.Max(_left, _right);
        }

        public static T Max<T>(T x, T y)
        {
            return (Comparer<T>.Default.Compare(x, y) > 0) ? x : y;
        }

        public static Real Saturate(Real _value)
        {
            return Clamp(_value, 0, 1);
        }
    }
}