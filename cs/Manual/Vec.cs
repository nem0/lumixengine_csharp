using System;
using System.Runtime.InteropServices;

namespace Lumix
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec4 : ICloneable, IEquatable<Vec4>
	{
		public float x;
		public float y;
		public float z;
		public float w;

		public Vec4(float _x, float _y, float _z, float _w)
		{
			x = _x;
			y = _y;
			z = _z;
			w = _w;
		}

		public Vec4(float[] _values)
		{
			x = _values[0];
			y = _values[1];
			z = _values[2];
			w = _values[3];
		}

		public Vec4(Vec4 vector)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
			w = vector.w;
		}

		public float this[int index]
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
					case 3:
						return w;
					default:
						throw new IndexOutOfRangeException("Vector has only 4 indices!");
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
					case 3:
						w = value;
						break;
					default:
						throw new IndexOutOfRangeException("Vector has only 4 indices!");
				}
			}
		}

		object ICloneable.Clone()
		{
			return new Vec4(this);
		}

		public Vec4 Clone()
		{
			return new Vec4(this);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("X:{0},Y:{1},Z:{2},W:{3}", x, y, z, w);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vec4))
				return false;
			return Equals((Vec4)obj);
		}

		public bool Equals(Vec4 v)
		{
			return v.x == this.x && v.y == this.y && v.z == this.z & v.w == this.w;
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct DVec3
	{
		public Double x;
		public Double y;
		public Double z;

		public DVec3(double _x, double _y, double _z) {
			x = _x;
			y = _y;
			z = _z;
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vec3 : ICloneable, IEquatable<Vec3>
	{
		public float x;
		public float y;
		public float z;

		public float Length
		{
			get { return Mathf.Sqrt(x * x + y * y + z * z); }
		}

		public float SquaredLength
		{
			get { return x * x + y * y + z * z; }
		}

		public Vec3 Normalized
		{
			get
			{
				Vec3 ret = new Vec3(this);
				ret.Normalize();
				return ret;
			}
		}


		public Vec3(float _x, float _y, float _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}

		public Vec3(float value)
		{
			x = value;
			y = value;
			z = value;
		}

		public Vec3(Vec3 vector)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
		}

		public float this[int index]
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

		object ICloneable.Clone()
		{
			return new Vec3(this);
		}

		public Vec3 Clone()
		{
			return new Vec3(this);
		}


		public override string ToString()
		{
			return string.Format("X:{0},Y:{1},Z:{2}", x, y, z);
		}


		public void Set(float _x, float _y, float _z)
		{
			x = _x;
			y = _y;
			z = _z;
		}

		public static Vec3 Add(Vec3 _left, Vec3 _right)
		{
			return new Vec3(_left.x + _right.x, _left.y + _right.y, _left.z + _right.z);
		}

		public static Vec3 Subtract(Vec3 _left, Vec3 _right)
		{
			return new Vec3(_left.x - _right.x, _left.y - _right.y, _left.z - _right.z);
		}

		public static Vec3 Multiply(Vec3 _left, Vec3 _right)
		{
			return new Vec3(_left.x * _right.x, _left.y * _right.y, _left.z * _right.z);
		}

		public static Vec3 Multiply(float _left, Vec3 _right)
		{
			return new Vec3(_left * _right.x, _left * _right.y, _left * _right.z);
		}

		public static Vec3 Divide(Vec3 _left, Vec3 _right)
		{
			return new Vec3(_left.x / _right.x, _left.y / _right.y, _left.z / _right.z);
		}

		public static Vec3 Divide(Vec3 _left, float _right)
		{
			return new Vec3(_left.x / _right, _left.y / _right, _left.z / _right);
		}

		public static Vec3 operator +(Vec3 _left, Vec3 _right)
		{
			return Add(_left, _right);
		}

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

		public static Vec3 operator *(Vec3 _left, Vec3 _right)
		{
			return Multiply(_left, _right);
		}

		public static Vec3 operator *(float _left, Vec3 _right)
		{
			return Multiply(_left, _right);
		}

		public static Vec3 operator *(Vec3 _left, float _right)
		{
			return Multiply(_right, _left);
		}

		public static Vec3 operator /(Vec3 _left, Vec3 _right)
		{
			return Divide(_left, _right);
		}

		public static Vec3 operator /(Vec3 _left, float _right)
		{
			return Divide(_left, _right);
		}

		public static Vec3 operator -(Vec3 _value)
		{
			return new Vec3(-_value.x, -_value.y, -_value.z);
		}

		public static float DotProduct(Vec3 _left, Vec3 _right)
		{
			return _left.x * _right.x + _left.y * _right.y + _left.z * _right.z;
		}

		public float DotProduct(Vec3 _value)
		{
			return DotProduct(this, _value);
		}

		public static Vec3 CrossProduct(Vec3 _left, Vec3 _right)
		{
			return new Vec3(
				_left.y * _right.z - _left.z * _right.y,
				_left.z * _right.x - _left.x * _right.z,
				_left.x * _right.y - _left.y * _right.x
				);
		}

		public Vec3 CrossProduct(Vec3 value)
		{
			return CrossProduct(this, value);
		}

		public float Normalize()
		{
			float length = Mathf.Sqrt(x * x + y * y + z * z);

			if (length > (float)0.0)
			{
				float invLength = (float)1.0 / length;
				x *= invLength;
				y *= invLength;
				z *= invLength;
			}

			return length;
		}

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
		public float x;
		public float y;

		public Vec2(float _constant)
		{
			x = _constant;
			y = _constant;
		}

		public Vec2(float _x, float _y)
		{
			x = _x;
			y = _y;
		}

		public static Vec2 Add(Vec2 _left, Vec2 _right)
		{
			return new Vec2(_left.x + _right.x, _left.y + _right.y);
		}
		public static Vec2 Divide(Vec2 _left, Vec2 _right)
		{
			return new Vec2(_left.x / _right.x, _left.y / _right.y);
		}
		public static Vec2 Divide(Vec2 _left, float _right)
		{
			return new Vec2(_left.x / _right, _left.y / _right);
		}
		public static Vec2 Subtract(Vec2 _left, Vec2 _right)
		{
			return new Vec2(_left.x - _right.x, _left.y - _right.y);
		}

		public static Vec2 operator /(Vec2 _left, Vec2 _right)
		{
			return Divide(_left, _right);
		}

		public static Vec2 operator /(Vec2 _left, float _right)
		{
			return Divide(_left, _right);
		}

		public static Int2 operator -(Vec2 _left, Int2 _right)
		{
			return new Int2(_left.x - _right.x, _left.y - _right.y);
		}

		public static Vec2 operator -(Vec2 _left, Vec2 _right)
		{
			return Subtract(_left, _right);
		}
	}


	public struct Int2
	{
		public int x;
		public int y;

		public static readonly Int2 Zero = new Int2(0, 0);

		public int this[int _index]
		{
			get
			{
				switch (_index)
				{
					case 0:
						return x;
					case 1:
						return y;
					default:
						throw new IndexOutOfRangeException("index");
				}
			}
			set
			{
				switch (_index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					default:
						throw new IndexOutOfRangeException("index");

				}
			}
		}

		public Int2(float _x, float _y)
		{
			x = (int)_x;
			y = (int)_y;
		}

		public Int2(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public Int2(Int2 _other)
		{
			x = _other.x;
			y = _other.y;
		}

		public Int2(int _constant)
		{
			x = _constant;
			y = _constant;
		}
		public static Int2 Add(Int2 _left, Int2 _right)
		{
			return new Int2(_left.x + _right.x, _left.y + _right.y);
		}

		public static Int2 operator +(Int2 _left, Int2 _right)
		{
			return Add(_left, _right);
		}

		public static Int2 Subtract(Int2 _left, Int2 _right)
		{
			return new Int2(_left.x - _right.x, _left.y - _right.y);
		}

		public static Int2 operator -(Int2 _left, Int2 _right)
		{
			return Subtract(_left, _right);
		}

		public static Int2 Divide(Int2 _left, Int2 _right)
		{
			return new Int2(_left.x / _right.x, _left.y / _right.y);
		}
		public static Int2 Divide(Int2 _left, int _right)
		{
			return new Int2(_left.x / _right, _left.y / _right);
		}

		public static Int2 operator /(Int2 _left, Int2 _right)
		{
			return Divide(_left, _right);
		}
		public static Int2 operator /(Int2 _left, int _right)
		{
			return Divide(_left, _right);
		}
		public static Int2 operator -(Int2 _left, Vec2 _right)
		{
			return new Int2(_left.x - _right.x, _left.y - _right.y);
		}

		public static implicit operator Vec2(Int2 _value)
		{
			return new Vec2(_value.x, _value.y);
		}

		public static Int2 Multiply(Int2 _left, int _right)
		{
			return new Int2(_left.x * _right, _left.y * _right);
		}

		public static Int2 operator *(Int2 _left, int _right)
		{
			return Multiply(_left, _right);
		}

		public int[] ToArray()
		{
			return new int[] { x, y };
		}

		public override string ToString()
		{
			return string.Format("x:{0},y:{1}", x, y);
		}
	}
}
