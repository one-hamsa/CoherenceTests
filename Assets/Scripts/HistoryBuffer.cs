using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class HistoryBuffer<T>
{
    private List<(double time, T value)> _history;
    private int _numSamples;

    protected HistoryBuffer()
    {
        _numSamples = 32;
        _history = new(_numSamples);
    }
    
    protected HistoryBuffer(int numSamples)
    {
        _numSamples = numSamples;
        _history = new(_numSamples);
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
            if (time < sample.time)
            {
                if (i == 0)
                    return sample.value;
                var prevSample = _history[i - 1];
                double paramBetweenSamples = (time - prevSample.time) / (sample.time - prevSample.time);
                return Lerp(prevSample.value, sample.value, Mathf.Clamp01((float)paramBetweenSamples));
            }
        }

        if (n > 0)
            return _history[^1].value;

        throw new IndexOutOfRangeException("Cannot evaluate history buffer, no data!");
    }

    public void ApplyOffset(T offset)
    {
        for (int i = 0; i < _history.Count; i++)
        {
            var cur = _history[i];
            cur.value = ApplyOffset(cur.value, offset);
            _history[i] = cur;
        }
    }

    protected abstract T Lerp(T a, T b, float param);
    protected abstract T ApplyOffset(T val, T offset);
}

public class FloatHistoryBuffer : HistoryBuffer<float>
{
    protected override float Lerp(float a, float b, float param) => Mathf.Lerp(a, b, param);
    protected override float ApplyOffset(float val, float offset) => val + offset;
}
public class Vector3HistoryBuffer : HistoryBuffer<Vector3>
{
    protected override Vector3 Lerp(Vector3 a, Vector3 b, float param) => Vector3.Lerp(a, b, param);
    protected override Vector3 ApplyOffset(Vector3 val, Vector3 offset) => val + offset;
}
public class QuaternionHistoryBuffer : HistoryBuffer<Quaternion>
{
    protected override Quaternion Lerp(Quaternion a, Quaternion b, float param) => Quaternion.Lerp(a, b, param);
    protected override Quaternion ApplyOffset(Quaternion val, Quaternion offset) => offset * val;
}
