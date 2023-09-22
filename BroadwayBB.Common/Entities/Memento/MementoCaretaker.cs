namespace BroadwayBB.Common.Entities.Memento;

public class MementoCaretaker
{
    private readonly int _maxMementoCount = 10;
    private readonly Stack<MuseumMemento> _mementos = new();

    public void AddMemento(MuseumMemento memento)
    {
        if (_mementos.Count < _maxMementoCount) _mementos.Push(memento);  
    } 
    
    public MuseumMemento? GetMemento() => _mementos.Count > 0 ? _mementos.Pop() : null;
}