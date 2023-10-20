namespace BroadwayBB.Common.Entities.Attendees.PathFinder;

public class PriorityItem<T>
{
    public T Item { get; set; }
    public double Priority { get; set; }

    public PriorityItem(T item, double priority)
    {
        Item = item;
        Priority = priority;
    }
}

public class PriorityQueue<T>
{
    private readonly List<PriorityItem<T>> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T item, double priority)
    {
        var priorityItem = new PriorityItem<T>(item, priority);
        _items.Add(priorityItem);
        int childIndex = _items.Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (_items[childIndex].Priority >= _items[parentIndex].Priority) break;

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

            if (leftChildIndex < _items.Count && _items[leftChildIndex].Priority < _items[nextIndex].Priority)
                nextIndex = leftChildIndex;
            if (rightChildIndex < _items.Count && _items[rightChildIndex].Priority < _items[nextIndex].Priority)
                nextIndex = rightChildIndex;

            if (nextIndex == currentIndex) break;

            Swap(currentIndex, nextIndex);
            currentIndex = nextIndex;
        }

        return (frontItem.Item, frontItem.Priority);
    }

    private void Swap(int a, int b)
    {
        var temp = _items[a];
        _items[a] = _items[b];
        _items[b] = temp;
    }
}
