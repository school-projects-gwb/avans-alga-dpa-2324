namespace BroadwayBB.Common.Models.Interfaces;

public interface IMovement
{
    public int GridPosX { get; }
    public int GridPosY { get; }
    public int PosXOnGrid { get; }
    public int PosYOnGrid { get; }
    public int SpeedVertical { get; }
    public int SpeedHorizontal { get; }

    // todo get next position based on speed, location, gridx gridy etc
    public void GetNextPosition()
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        throw new NotImplementedException();   
    }
}