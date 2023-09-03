namespace BroadwayBB.Common.Models.Interfaces;

public interface IMovement
{
    public int GridPosX { get; }
    public int GridPosY { get; }
    public int PosXOnTile { get; }
    public int PosYOnTile { get; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }
    public MovementDirection MovementDirection { get; }
    
    public void Move(List<MovementDirection> possibleDirections);
}