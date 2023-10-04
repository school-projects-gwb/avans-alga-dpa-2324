namespace BroadwayBB.Common.Entities.Structures
{
    public struct Coords
    {
        public double Xd { get; set; }
        public double Yd { get; set; }

        public int Xi
        {
            get
            {
                return (int)Xd;
            }
            set
            {
                Xd = (int)value;
            }
        }
        public int Yi
        {
            get
            {
                return (int)Yd;
            }
            set
            {
                Yd = (int)value;
            }
        }

        public Coords(double x, double y)
        {
            Xd = x;
            Yd = y;
        }

        public static Coords operator +(Coords a, Coords b)
        {
            return new Coords(a.Xd + b.Xd, a.Yd + b.Yd);
        }
        public static Coords operator -(Coords a, Coords b)
        {
            return new Coords(a.Xd - b.Xd, a.Yd - b.Yd);
        }
        public static Coords operator *(Coords a, Coords b)
        {
            return new Coords(a.Xd * b.Xd, a.Yd * b.Yd);
        }
        public static Coords operator /(Coords a, Coords b)
        {
            return new Coords(a.Xd / b.Xd, a.Yd / b.Yd);
        }

        public static bool operator ==(Coords a, Coords b)
        {
            return a.Xd == b.Xd && a.Yd == b.Yd;
        }
        public static bool operator !=(Coords a, Coords b)
        {
            return a.Xd != b.Xd || a.Yd != b.Yd;
        }

        public static bool IntEqual(Coords a, Coords b)
        {
            return a.Xi == b.Xi && a.Yi == b.Yi;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
