using BroadwayBB.Common.Entities.Structures;

namespace BroadwayBB.Simulation;

public class PointerRegistration
{
    public Coords? LeftClickPosition;
    public Coords? RightClickPosition;

    public bool IsValid() => LeftClickPosition != null && RightClickPosition != null;

    public void Reset() => LeftClickPosition = RightClickPosition = null;
}