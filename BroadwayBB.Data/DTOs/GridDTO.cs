using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Strategies.Interfaces;

namespace BroadwayBB.Data.Strategies;

public struct GridDTO : DTO
{
    public int Rows;
    public int Columns;

    public List<NodeTypeDTO> NodeTypes;
    public List<NodeDTO> Nodes;

    public GridDTO()
    {
        NodeTypes = new List<NodeTypeDTO>();
        Nodes = new List<NodeDTO>();
    }
}