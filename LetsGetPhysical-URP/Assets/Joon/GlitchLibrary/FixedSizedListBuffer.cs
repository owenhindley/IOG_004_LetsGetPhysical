using System.Collections.Generic;

public class FixedSizedListBuffer<T> : List<T>
{
    public int size { get; private set; }

    public FixedSizedListBuffer(int size)
    {
        this.size = size;
    }

    public new void Add(T obj)
    {
        base.Add(obj);
        if(Count > size) RemoveAt(0);
    }

}