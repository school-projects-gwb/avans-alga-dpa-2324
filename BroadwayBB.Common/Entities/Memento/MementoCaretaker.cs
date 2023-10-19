using BroadwayBB.Simulation.Memento;

namespace BroadwayBB.Common.Entities.Memento;

public class MementoCaretaker
{
    private readonly int _maxMementoCount = 10;
    private readonly Stack<MuseumMemento> _mementos = new();
    private readonly Museum.Museum _museum;
    
    public MementoCaretaker(Museum.Museum museum)
    {
        _museum = museum;
    }
    
    public void AddMemento()
    {
        if (_mementos.Count < _maxMementoCount) _mementos.Push(_museum.CreateMemento());  
    }

    public void RestoreMemento()
    {
        if (_mementos.Count > 0) _museum.RewindMemento(_mementos.Pop());
    }
}