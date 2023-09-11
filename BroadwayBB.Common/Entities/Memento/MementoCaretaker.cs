namespace BroadwayBB.Common.Entities.Memento;

public class MementoCaretaker
{
    private readonly Stack<MuseumMemento> mementos = new();
    
    public void AddMemento(MuseumMemento memento) => mementos.Push(memento);
    
    public MuseumMemento? GetMemento() => mementos.Count > 0 ? mementos.Pop() : null;
}