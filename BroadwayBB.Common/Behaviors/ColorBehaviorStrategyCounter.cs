using System.ComponentModel.Design.Serialization;

namespace BroadwayBB.Common.Behaviors;

public class ColorBehaviorStrategyCounter
{
    private int CounterLimit { get; }
    private int _counterValue;

    public ColorBehaviorStrategyCounter(int counterLimit) => CounterLimit = counterLimit;

    public void Increase() => _counterValue++;
    
    public bool LimitReached() => CounterLimit == _counterValue;

    public ColorBehaviorStrategyCounter DeepCopy()
    {
        var counterCopy = new ColorBehaviorStrategyCounter(CounterLimit);
        counterCopy._counterValue = _counterValue;

        return counterCopy;
    }
}