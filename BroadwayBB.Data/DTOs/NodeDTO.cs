using BroadwayBB.Data.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadwayBB.Data.DTOs
{
    public struct NodeDTO
    {
        public NodeTypeDTO Type;
        public Coords Coords;
        public List<Coords> Edges;

        public NodeDTO(NodeTypeDTO? type, Coords coords)
        {
            if (type != null) {
                Type = (NodeTypeDTO)type; 
            }
            else
            {
                Type = new NodeTypeDTO(0, Color.White, '_');
            }

            Coords = coords;
            Edges = new List<Coords>();
        }
    }
}
