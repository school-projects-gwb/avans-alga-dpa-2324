using System.ComponentModel.Design.Serialization;

namespace BroadwayBB.Common.Behaviors;

public class TileColorCounter
{
    private int CounterLimit { get; }
    private int _counterValue;

    public TileColorCounter(int counterLimit) => CounterLimit = counterLimit;

    public void Increase() => _counterValue++;
    
    public bool LimitReached() => CounterLimit == _counterValue;
}