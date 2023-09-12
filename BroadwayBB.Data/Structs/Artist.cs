using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadwayBB.Data.Structs
{
    public struct Artist
    {
        public double X;
        public double Y;
        public double VelocityX;
        public double VelocityY;

        public Artist(string x, string y, string vx, string vy)
        {
            X = double.Parse(x, CultureInfo.InvariantCulture);
            Y = double.Parse(y, CultureInfo.InvariantCulture);
            VelocityX = double.Parse(vx, CultureInfo.InvariantCulture);
            VelocityY = double.Parse(vy, CultureInfo.InvariantCulture);
        }

        public Artist(int x, int y, int vx, int vy)
        {
            X = x;
            Y = y;
            VelocityX = vx;
            VelocityY = vy;
        }

        public Artist(double x, double y, double vx, double vy)
        {
            X = x;
            Y = y;
            VelocityX = vx;
            VelocityY = vy;
        }
    }
}
