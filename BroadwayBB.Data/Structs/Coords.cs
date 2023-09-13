using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BroadwayBB.Data.Structs
{
    public struct Coords
    {
        public double X { get; }
        public double Y { get; }

        public Coords(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Coords operator +(Coords x, Coords y)
        {
            return new Coords(x.X + y.X, x.Y + y.Y);
        }
    }
}
