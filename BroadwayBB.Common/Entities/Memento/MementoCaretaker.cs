namespace BroadwayBB.Common.Entities.Memento;

public class MementoCaretaker
{
    private readonly int _maxMementoCount = 10;
    private readonly Stack<MuseumMemento> mementos = new();

    public void AddMemento(MuseumMemento memento)
    {
        if (mementos.Count < _maxMementoCount) mementos.Push(memento);  
    } 
    
    public MuseumMemento? GetMemento() => mementos.Count > 0 ? mementos.Pop() : null;
}