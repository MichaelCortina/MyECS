using System.Collections;

namespace MyECS;

public sealed class CircularList<T> : IEnumerable<T>
{
    private sealed class Node<T>
    {
        public T Data { get; init; }
        public Node<T> Next { get; set; }
    }
    
    private Node<T>? _head;
    private Node<T>? _tail;

    public CircularList()
    {
        _head = _tail = null;
    }
    
    public CircularList<T> Push(T element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));
        
        var node = new Node<T> { Data = element };
        if (_head is null)
        {
            node.Next = node;
            _tail = _head = node;
        }
        else
        {
            node.Next = _head;
            _head = node;
            _tail!.Next = node;
        }

        return this;
    }
    
    public IEnumerator<T> GetEnumerator() => _head is null
        ? Enumerable.Empty<T>().GetEnumerator() 
        : new CircularListIterator<T>(_head);
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private sealed class CircularListIterator<T> : IEnumerator<T>
    {
        private Node<T> _current;
        private readonly Node<T> _head;

        public CircularListIterator(Node<T> head)
        {
            _head = _current = head;
        }

        public bool MoveNext()
        {
            _current = _current.Next;
            return true;
        }

        public void Reset()
        {
            _current = _head;
        }

        public T Current => _current.Data;

        object IEnumerator.Current => Current!;

        public void Dispose() { }
    }
}