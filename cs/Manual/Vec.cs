#if !HIGHPRECISION
using Real = System.Single;
#else
using Real = System.Double;
#endif
using System;
using System.Runtime.InteropServices;


namespace Lumix
{


    public struct Vec4
    {
        public Real x;
        public Real y;
        public Real z;
        public Real w;

        public Vec4(Real _x, Real _y, Real _z, Real _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec3 : ICloneable, IEquatable<Vec3>
    {
        public Real x;
        public Real y;
        public Real z;

        /// <summary>
        /// Vector 3, all components set to 0,
        /// </summary>
        public static readonly Vec3 Zero = new Vec3(0, 0, 0);
        /// <summary>
        /// Vector 3, all components set to 1,
        /// </summary>
        public static readonly Vec3 One = new Vec3(1, 1, 1);
        /// <summary>
        /// Vector 3, points "up", y component is 1
        /// </summary>
        public static readonly Vec3 Up = new Vec3(0, 1, 0);
        /// <summary>
        /// Vector 3, points "up", y component is 1
        /// </summary>
        public static readonly Vec3 Down = new Vec3(0, -1, 0);
        /// <summary>
        /// Vector 3, points "forward", z component is 1
        /// </summary>
        public static readonly Vec3 Forward = new Vec3(0, 0, 1);
        /// <summary>
        /// Vector 3, points "back", z component is -1
        /// </summary>
        public static readonly Vec3 Back = new Vec3(0, 0, -1);
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vec3 Right = new Vec3(1, 0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vec3 Left = new Vec3(-1, 0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static readonly Vec3 PosInfinity = new Vec3(Mathf.PosInfinity, Mathf.PosInfinity, Mathf.PosInfinity);


        /// <summary>
        ///  Returns the length (magnitude) of the vector.
        /// </summary>
        /// <remarks>
        ///  This operation requires a square root and is expensive in
        ///terms of CPU operations.If you don't need to know the exact
        ///length (e.g. for just comparing lengths) use squaredLength()
        /// instead.
        /// </remarks>
        public Real Length
        {
            get { return Mathf.Sqrt(x * x + y * y + z * z); }
        }

        /// <summary>
        /// Returns the square of the length(magnitude) of the vector.
        /// </summary>
        /// <remarks>
        /// This  method is for efficiency - calculating the actual
        /// length of a vector requires a square root, which is expensive
        /// in terms of the operations required.This method returns the
        /// square of the length of the vector, i.e.the same as the
        /// length but before the square root is taken.Use this if you
        /// want to find the longest / shortest vector without incurring
        /// the square root.
        /// </remarks>
        public Real SquaredLength
        {
            get { return x * x + y * y + z * z; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is invalid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is invalid; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalid
        {
            get
            {
                return Real.IsNaN(this.x) || Real.IsNaN(this.y) || Real.IsNaN(this.z);
            }
        }

        /// <summary>
        /// Get's a copy of this vector, just normalized
        /// </summary>
        public Vec3 Normalized
        {
            get
            {
                Vec3 ret = new Vec3(this);
                ret.Normalize();
                return ret;
            }
        }


        public Vec3(Real _x, Real _y, Real _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Vec3(Real value)
        {
            x = value;
            y = value;
            z = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        public Vec3(Vec3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Real this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Vector has only 3 indices!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Vector has only 3 indices!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return new Vec3(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vec3 Clone()
        {
            return new Vec3(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X:{0},Y:{1},Z:{2}", x, y, z);
        }


        public void Set(Real _x, Real _y, Real _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Add(Vec3 _left, Vec3 _right)
        {
            return new Vec3(_left.x + _right.x, _left.y + _right.y, _left.z + _right.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Subtract(Vec3 _left, Vec3 _right)
        {
            return new Vec3(_left.x - _right.x, _left.y - _right.y, _left.z - _right.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Multiply(Vec3 _left, Vec3 _right)
        {
            return new Vec3(_left.x * _right.x, _left.y * _right.y, _left.z * _right.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Multiply(Real _left, Vec3 _right)
        {
            return new Vec3(_left * _right.x, _left * _right.y, _left * _right.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Divide(Vec3 _left, Vec3 _right)
        {
            return new Vec3(_left.x / _right.x, _left.y / _right.y, _left.z / _right.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 Divide(Vec3 _left, Real _right)
        {
            return new Vec3(_left.x / _right, _left.y / _right, _left.z / _right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator +(Vec3 _left, Vec3 _right)
        {
            return Add(_left, _right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator -(Vec3 _left, Vec3 _right)
        {
            return Subtract(_left, _right);
        }
        public static bool operator ==(Vec3 _left, Vec3 _right)
        {
            return _left.x == _right.x && _left.y == _right.y && _left.z == _right.z;
        }
        public static bool operator !=(Vec3 _left, Vec3 _right)
        {
            return !(_left == _right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator *(Vec3 _left, Vec3 _right)
        {
            return Multiply(_left, _right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator *(Real _left, Vec3 _right)
        {
            return Multiply(_left, _right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator *(Vec3 _left, Real _right)
        {
            return Multiply(_right, _left);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator /(Vec3 _left, Vec3 _right)
        {
            return Divide(_left, _right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 operator /(Vec3 _left, Real _right)
        {
            return Divide(_left, _right);
        }

        public static Vec3 operator -(Vec3 _value)
        {
            return new Vec3(-_value.x, -_value.y, -_value.z);
        }
        /// <summary>
        /// Calculates the dot (scalar) product of this vector with another.
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Real DotProduct(Vec3 _left, Vec3 _right)
        {
            return _left.x * _right.x + _left.y * _right.y + _left.z * _right.z;
        }

        /// <summary>
        /// Calculates the absolute dot (scalar) product of given vector with other.
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Real AbsDotProduct(Vec3 _left, Vec3 _right)
        {
            return Mathf.Abs(_left.x * _right.x) + Mathf.Abs(_left.y * _right.y) + Mathf.Abs(_left.z * _right.z);
        }

        /// <summary>
        /// Calculates the dot (scalar) product of this vector with another.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Real DotProduct(Vec3 _value)
        {
            return DotProduct(this, _value);
        }

        /// <summary>
        /// Calculates the absolute dot (scalar) product of this vector with another.
        /// </summary>
        /// <param name="_other"></param>
        /// <returns></returns>
        public Real AbsDotProduct(Vec3 _other)
        {
            return AbsDotProduct(this, _other);
        }

        /// <summary>
        /// Calculates the cross-product of 2 vectors, i.e. the vector that
        /// lies perpendicular to them both.
        /// </summary>
        /// <param name="_left"></param>
        /// <param name="_right"></param>
        /// <returns></returns>
        public static Vec3 CrossProduct(Vec3 _left, Vec3 _right)
        {
            return new Vec3(
                _left.y * _right.z - _left.z * _right.y,
                _left.z * _right.x - _left.x * _right.z,
                _left.x * _right.y - _left.y * _right.x
                );
        }

        /// <summary>
        /// Calculates the cross-product of 2 vectors, i.e. the vector that
        /// lies perpendicular to them both.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Vec3 CrossProduct(Vec3 value)
        {
            return CrossProduct(this, value);
        }

        /// <summary>
        ///  Normalizes the vector.
        /// </summary>
        /// <returns>The previous length of the vector.</returns>
        public float Normalize()
        {
            Real length = Mathf.Sqrt(x * x + y * y + z * z);

            if (length > (Real)0.0)
            {
                Real invLength = (Real)1.0 / length;
                x *= invLength;
                y *= invLength;
                z *= invLength;
            }

            return length;
        }

        /// <summary>
        ///  Sets this vector's components to the minimum of its own and the
        /// ones of the passed in vector.
        /// </summary>
        /// <param name="_other"></param>
        public void MakeFloor(Vec3 _other)
        {
            if (_other.x < x) x = _other.x;
            if (_other.y < y) y = _other.y;
            if (_other.z < z) z = _other.z;
        }

        /// <summary>
        /// Sets this vector's components to the maximum of its own and the
        /// ones of the passed in vector.
        /// </summary>
        /// <param name="_other"></param>
        public void MakeCeil(Vec3 _other)
        {
            if (_other.x > x) x = _other.x;
            if (_other.y > y) y = _other.y;
            if (_other.z > z) z = _other.z;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vec3))
                return false;

            Vec3 v = (Vec3)obj;

            return v.x == this.x && v.y == this.y && v.z == this.z;
        }

        public bool Equals(Vec3 v)
        {
            return v.x == this.x && v.y == this.y && v.z == this.z;
        }
    }

    public struct Vec2
    {
        public Real x;
        public Real y;

        public Vec2(Real _x, Real _y)
        {
            x = _x;
            y = _y;
        }
    }


    public struct Int2
    {
        public int x;
        public int y;

        public Int2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
