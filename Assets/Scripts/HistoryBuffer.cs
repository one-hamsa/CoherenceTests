using System.Collections.Generic;
using UnityEngine;

public abstract class HistoryBuffer<T>
{
    private List<(double time, T value)> _history;
    private int _numSamples;

    protected HistoryBuffer()
    {
        _numSamples = 10;
        _history = new(_numSamples);
    }
    
    protected HistoryBuffer(int numSamples)
    {
        _numSamples = numSamples;
        _history = new(numSamples);
    }

    public void AddSample(double time, T value)
    {
        while (_history.Count >= _numSamples)
            _history.RemoveAt(0);
        
        _history.Add((time, value));
    }

    public T Evaluate(double time)
    {
        int n = _history.Count;
        for (int i = 0; i < n; i++)
        {
            var sample = _history[i];
            if (time >= sample.time)
            {
                if (i == 0 || i+1 >= n)
                    return sample.value;
                var next_sample = _history[i + 1];
                double lerp = (time - sample.time) / (next_sample.time - sample.time);
                return Lerp(sample.value, next_sample.value, Mathf.Clamp01((float)lerp));
            }
        }

        return _history[^1].value;
    }

    protected abstract T Lerp(T a, T b, float param);
}

public class FloatHistoryBuffer : HistoryBuffer<float>
{
    protected override float Lerp(float a, float b, float param) => Mathf.Lerp(a, b, param);
}
public class Vector3HistoryBuffer : HistoryBuffer<Vector3>
{
    protected override Vector3 Lerp(Vector3 a, Vector3 b, float param) => Vector3.Lerp(a, b, param);
}
public class QuaternionHistoryBuffer : HistoryBuffer<Quaternion>
{
    protected override Quaternion Lerp(Quaternion a, Quaternion b, float param) => Quaternion.Lerp(a, b, param);
}
