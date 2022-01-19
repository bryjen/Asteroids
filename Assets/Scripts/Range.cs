using UnityEngine;

public class Range<T>
{
    private T _lowerBound;
    private T _upperBound;

    public T lowerBound
    {
        get => _lowerBound;
    }
    
    public T upperBound
    {
        get => _upperBound;
    }
    
    public Range(T lowerBound, T upperBound)
    {
        _lowerBound = lowerBound;
        _upperBound = upperBound;
    }
}
