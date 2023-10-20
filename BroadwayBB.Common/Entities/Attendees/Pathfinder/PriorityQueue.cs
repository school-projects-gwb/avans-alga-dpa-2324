namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class PriorityQueue<T>
{
    private readonly List<(T item, double priority)> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T item, double priority)
    {
        _items.Add((item, priority));
        int childIndex = _items.Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (_items[childIndex].priority >= _items[parentIndex].priority) break;

            Swap(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public (T item, double priority) Dequeue()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        var frontItem = _items[0];
        _items[0] = _items[_items.Count - 1];
        _items.RemoveAt(_items.Count - 1);

        int currentIndex = 0;
        while (true)
        {
            int leftChildIndex = 2 * currentIndex + 1;
            int rightChildIndex = 2 * currentIndex + 2;
            int nextIndex = currentIndex;

            if (leftChildIndex < _items.Count && _items[leftChildIndex].priority < _items[nextIndex].priority)
                nextIndex = leftChildIndex;
            if (rightChildIndex < _items.Count && _items[rightChildIndex].priority < _items[nextIndex].priority)
                nextIndex = rightChildIndex;

            if (nextIndex == currentIndex) break;

            Swap(currentIndex, nextIndex);
            currentIndex = nextIndex;
        }

        return frontItem;
    }

    private void Swap(int a, int b) => (_items[a], _items[b]) = (_items[b], _items[a]);
}
