using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;
using System.Drawing;
using System.Xml.Linq;

namespace BroadwayBB.Data.Strategies;

public class XmlFileReaderStrategy : IFileReaderStrategy
{
    public DTO ReadFile(FileData file)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var grid = new GridDTO();

        var fileData = XElement.Parse(file.Data);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        grid.Rows = int.Parse(fileData.Attribute("rows").Value);
        grid.Columns = int.Parse(fileData.Attribute("cols").Value);

        var typeData = fileData.Element("nodeTypes");
        if (typeData == null)
        {
            throw new FormatException();
        }

        var types = typeData.Elements("nodeType").ToList();

        foreach (var type in types)
        {
            var weight = int.Parse(type.Attribute("weight").Value);
            var red = int.Parse(type.Attribute("red").Value);
            var green = int.Parse(type.Attribute("green").Value);
            var blue = int.Parse(type.Attribute("blue").Value);
            var tag = (string)type.Attribute("tag").Value.ToUpper();

            grid.NodeTypes.Add(new NodeTypeDTO(weight, Color.FromArgb(red, green, blue), tag[0]));
        }

        var nodeData = fileData.Element("nodes");
        if (nodeData == null)
        {
            throw new FormatException();
        }

        var nodes = nodeData.Elements().ToList();

        foreach (var node in nodes)
        {
            var x = int.Parse(node.Attribute("x").Value);
            var y = int.Parse(node.Attribute("y").Value);

            var type = grid.NodeTypes.Where(t => t.Tag == node.Name.ToString().ToUpper()[0]).First();

            var nodeDto = new NodeDTO(type, new Coords(x, y));

            if (node.HasElements)
            {
                var edgeData = node.Element("edges");
                var edges = edgeData.Elements("edge").ToList();

                foreach (var edge in edges)
                {
                    var ex = int.Parse(edge.Attribute("x").Value);
                    var ey = int.Parse(edge.Attribute("y").Value);

                    nodeDto.Edges.Add(new Coords(ex, ey));
                }
            }

            grid.Nodes.Add(nodeDto);
        }

        return grid;
#pragma warning restore CS8602
    }
}