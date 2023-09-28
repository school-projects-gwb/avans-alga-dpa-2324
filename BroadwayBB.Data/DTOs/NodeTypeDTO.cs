﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadwayBB.Data.DTOs
{
    public struct NodeTypeDTO
    {
        public int Weight;
        public Color Color;
        public char Tag;

        public NodeTypeDTO(int weight, Color color, char tag)
        {
            Weight = weight;
            Color = color;
            Tag = tag;
        }
    }
}
