using System.Drawing;

namespace BroadwayBB.Common.Entities.Attendees.Collider;

public abstract class ColliderStrategyBase : IColliderStrategy
{
    private readonly int _minimumCollisionDistance = 1;
    public Rectangle Size { get; private set; }
    
    public ColliderStrategyBase(Rectangle size) => Size = size;

    public abstract void HandleCollision(List<IAttendee> attendees);

    protected void HandleIsColliding(IAttendee one, IAttendee two)
    {
        double dx = Math.Abs(one.Movement.GridPosX - two.Movement.GridPosX);
        double dy = Math.Abs(one.Movement.GridPosY - two.Movement.GridPosY);

        bool isColliding = dx <= _minimumCollisionDistance && dy <= _minimumCollisionDistance;
            
        if (isColliding)
            Console.WriteLine("Colliding: " + one.Movement.GridPosX + "|" + one.Movement.GridPosY + " -- " + two.Movement.GridPosX + "|" + two.Movement.GridPosY);
        
        one.Movement.IsColliding = isColliding;
        two.Movement.IsColliding = isColliding;
    }
    
    public abstract List<Rectangle> GetDebugInfo();
}