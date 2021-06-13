using System;
using System.Collections;
using System.Collections.Generic;

public class CycleList<T> : IEnumerable<T>
{
    public T this [int index]
    {
        get
        {
            if (index == 0) return _head.Data; 
            var head = _head;
            for (int i = 0; i < index; i++)
            {
                head = head.Previous;
            }
            return head.Data;
        }
        set
        {
            if (index == 0) _head.Data = value;
            var head = _head;
            for (int i = 0; i < index; i++)
            {
                head = head.Previous;
            }
            head.Data = value;
        }
    }

    private Node<T> _head;
    private Node<T> _current;
    private Node<T> _tail;

    public int Count { get; private set; }

    public void Add(T data)
    {
        var node = new Node<T>(data);
        if (_head == null)
        {
            _head = node;
            _head.Next = _head;
            _tail = node;
            _head.Previous = _head;
        }
        else
        {
            var oldHead = _head;
            _head.Next = node;
            _head = node;
            _head.Previous = oldHead;
            _head.Next = _tail;
            _tail.Previous = _head;
        }

        _current = _head;
        Count++;
    }
    
    public T GetCurrent()
    {
        if (_current == null)
        {
            throw new NullReferenceException();
        }
        
        return _current.Data;
    }
    
    public T GetNext()
    {
        if (_current == null)
        {
            throw new NullReferenceException();
        }
        
        return _current.Next.Data;
    }
    
    public T GetPrevious()
    {
        if (_current == null)
        {
            throw new NullReferenceException();
        }
        
        return _current.Previous.Data;
    }
    
    public T MoveNext()
    {
        if (_current == null)
        {
            throw new NullReferenceException();
        }
        
        _current = _current.Next;
        return _current.Data;
    }
    
    public T MovePrevious()
    {
        if (_current == null)
        {
            throw new NullReferenceException();
        }
        
        _current = _current.Previous;
        return _current.Data;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        var current = _head.Next;
        do
        {
            yield return current.Data;
            current = current.Next;
        } while (current != _head.Next);
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    private class Node<T>
    {
        public T Data;
        public Node(T data)
        {
            Data = data;
        }

        public Node<T> Previous;
        public Node<T> Next;
    }
}
