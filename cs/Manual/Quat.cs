#if !HIGHPRECISION
using Real = System.Single;
#else
using Real = System.Double;
#endif
using System;
using System.Runtime.InteropServices;



namespace Lumix
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct AxisAngle
    {
        public Vec3 axis;
        public Real angle;
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quat : ICloneable, IEquatable<Quat>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public static readonly Quat Zero = new Quat(0, 0, 0, 0);
        public static readonly Quat Identity = new Quat(1, 0, 0, 0);
        public static readonly Quat XAxis = new Quat(0, 1, 0, 0);
        public static readonly Quat YAxis = new Quat(0, 0, 1, 0);
        public static readonly Quat ZAxis = new Quat(0, 0, 0, 1);
        public static readonly Quat WAxis = new Quat(1, 0, 0, 0);

        public AxisAngle AxisAngle
        {
            get
            {
                AxisAngle ret;
                if (Mathf.Abs(1 - w * w) < 0.00001f)
                {
                    ret.angle = 0;
                    ret.axis = new Vec3(0, 1, 0);
                }
                else
                {
                    ret.angle = 2 * Mathf.ACos(w);
                    float tmp = 1 / Mathf.Sqrt(1 - w * w);
                    ret.axis = new Vec3(x * tmp, y * tmp, z * tmp);
                }
                return ret;
            }
        }

        public Vec3 Euler
        {
            get
            {
                // from urho3d
                Real check = 2.0f * (-y * z + w * x);

                if (check < -0.995f)
                {
                    return new Vec3(
                        -Mathf.PI * 0.5f, 0.0f, -Mathf.Atan2(2.0f * (x * z - w * y), 1.0f - 2.0f * (y * y + z * z)));
                }
                else if (check > 0.995f)
                {
                    return new Vec3(
                        Mathf.PI * 0.5f, 0.0f, Mathf.Atan2(2.0f * (x * z - w * y), 1.0f - 2.0f * (y * y + z * z)));
                }
                else
                {
                    return new Vec3(Mathf.ASin(check),
                        Mathf.Atan2(2.0f * (x * z + w * y), 1.0f - 2.0f * (x * x + y * y)),
                        Mathf.Atan2(2.0f * (x * y + w * z), 1.0f - 2.0f * (x * x + z * z)));
                }
            }
        }

        public Quat Conjugated
        {
            get
            {
                return new Quat(x, y, z, -w);
            }
        }

        public Quat Normalized
        {
            get
            {
                Quat ret = new Quat(this);
                ret.Normalize();
                return ret;
            }
        }

        public Quat(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public Quat(Vec3 _axis, Real _angle)
        {
            Real half_angle = _angle * 0.5f;
            Real s = Mathf.Sin(half_angle);
            w = Mathf.Cos(half_angle);
            x = _axis.x * s;
            y = _axis.y * s;
            z = _axis.z * s;
        }
        public Quat(Quat other)
        {
            x = other.x;
            y = other.y;
            z = other.z;
            w = other.w;
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
                        throw new IndexOutOfRangeException("Quat has only 4 indices!");
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
                        throw new IndexOutOfRangeException("Quat has only 4 indices!");
                }
            }
        }

        public static Quat FromAngleAxis(Real angle, Vec3 axis)
        {
            Quat ret = Quat.Identity;
            Real halfAngle = (Real)(0.5 * angle);
            Real sin = Mathf.Sin(halfAngle);
            ret.w = Mathf.Cos(halfAngle);
            ret.x = sin * axis.x;
            ret.y = sin * axis.y;
            ret.z = sin * axis.z;
            return ret;
        }

        public static Quat FromEuler(Vec3 _euler)
        {

            System.Diagnostics.Debug.Assert(_euler.x >= -Mathf.HalfPI && _euler.x <= Mathf.HalfPI);
            float ex = _euler.x * 0.5f;
            float ey = _euler.y * 0.5f;
            float ez = _euler.z * 0.5f;
            float sinX = Mathf.Sin(ex);
            float cosX = Mathf.Cos(ex);
            float sinY = Mathf.Sin(ey);
            float cosY = Mathf.Cos(ey);
            float sinZ = Mathf.Sin(ez);
            float cosZ = Mathf.Cos(ez);
            return new Quat()
            {
                w = cosY * cosX * cosZ + sinY * sinX * sinZ,
                x = cosY * sinX * cosZ + sinY * cosX * sinZ,
                y = sinY * cosX * cosZ - cosY * sinX * sinZ,
                z = cosY * cosX * sinZ - sinY * sinX * cosZ
            };
        }

        public void Normalize()
        {
            Real l = 1 / Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x *= l;
            y *= l;
            z *= l;
            w *= l;
        }
        public void Conjugate()
        {
            w = -w;
        }


        public Quat Vec3ToVec3(Vec3 _a, Vec3 _b)
        {
            Real angle = Mathf.ACos(Vec3.DotProduct(_a, _b));
            Vec3 normal = Vec3.CrossProduct(_a, _b);
            Real normal_len = normal.Length;
            return new Quat(normal_len < 0.001f ? new Vec3(0, 1, 0) : normal * (1 / normal_len), angle);
        }

        object ICloneable.Clone()
        {
            return new Quat(this);
        }

        public Quat Clone()
        {
            return new Quat(this);
        }
        public bool Equals(Quat o)
        {
            return o.x == this.x && o.y == this.y && o.z == this.z && o.w == this.w;
        }

        public override int GetHashCode()
        {
            return w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public void Nlerp(Quat _q1, Quat _q2, out Quat _out, Real t)
        {
            Real inv = (Real)1.0 - t;
            if (_q1.x * _q2.x + _q1.y * _q2.y + _q1.z * _q2.z + _q1.w * _q2.w < 0) t = -t;
            float ox = _q1.x * inv + _q2.x * t;
            float oy = _q1.y * inv + _q2.y * t;
            float oz = _q1.z * inv + _q2.z * t;
            float ow = _q1.w * inv + _q2.w * t;
            float l = 1 / Mathf.Sqrt(ox * ox + oy * oy + oz * oz + ow * ow);
            ox *= l;
            oy *= l;
            oz *= l;
            ow *= l;
            _out.x = ox;
            _out.y = oy;
            _out.z = oz;
            _out.w = ow;
        }

        public Vec3 Rotate(Vec3 _v)
        {
            // nVidia SDK implementation

            Vec3 uv, uuv;
            Vec3 qvec = new Vec3(x, y, z);
            uv = Vec3.CrossProduct(qvec, _v);
            uuv = Vec3.CrossProduct(qvec, uv);
            uv *= (2.0f * w);
            uuv *= 2.0f;

            return _v + uv + uuv;
        }

        public static Quat operator *(Quat _left, Quat _right)
        {
            return new Quat(_left.w * _right.x + _right.w * _left.x + _left.y * _right.z - _right.y * _left.z,
                            _left.w * _right.y + _right.w * _left.y + _left.z * _right.x - _right.z * _left.x,
                            _left.w * _right.z + _right.w * _left.z + _left.x * _right.y - _right.x * _left.y,
                            _left.w * _right.w - _left.x * _right.x - _left.y * _right.y - _left.z * _right.z);
        }

        public override string ToString()
        {
            return string.Format("W:{0},X:{1},Y:{2},Z:{3}", w, x, y, z);
        }
    }
}
