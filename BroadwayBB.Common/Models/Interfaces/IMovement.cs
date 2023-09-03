namespace BroadwayBB.Common.Models.Interfaces;

public interface IMovement
{
    public int GridPosX { get; }
    public int GridPosY { get; }
    public int PosXOnGrid { get; }
    public int PosYOnGrid { get; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }
    
    public MovementPosition GetNextPosition();

    public void Move(MovementPosition newPosition);
}