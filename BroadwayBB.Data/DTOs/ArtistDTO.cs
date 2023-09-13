using BroadwayBB.Data.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadwayBB.Data.DTOs
{
    public struct ArtistDTO
    {
        public Coords Coords;
        public double VelocityX;
        public double VelocityY;

        public ArtistDTO(string x, string y, string vx, string vy)
        {
            var dx = double.Parse(x, CultureInfo.InvariantCulture);
            var dy = double.Parse(y, CultureInfo.InvariantCulture);

            Coords = new Coords(dx, dy);
            VelocityX = double.Parse(vx, CultureInfo.InvariantCulture);
            VelocityY = double.Parse(vy, CultureInfo.InvariantCulture);
        }

        public ArtistDTO(int x, int y, int vx, int vy)
        {
            Coords = new Coords(x, y);
            VelocityX = vx;
            VelocityY = vy;
        }

        public ArtistDTO(double x, double y, double vx, double vy)
        {
            Coords = new Coords(x, y);
            VelocityX = vx;
            VelocityY = vy;
        }
    }
}
