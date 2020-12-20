using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRange<T>
{
    T GetRandom();
    T Lerp(float factor, bool clamp = true);
    float InvLerp(T value, bool clamp = true);
}
[System.Serializable]
public class FloatRange : IRange<float>
{
    public float _min;
    public float _max;

    public float GetRandom()
    {
        return Random.Range(_min, _max);
    }
    public float Lerp(float factor, bool clamp = true)
    {
        if (clamp)
            factor = Mathf.Clamp01(factor);
        return _min + (_max - _min) * factor;
    }
    public float InvLerp(float value, bool clamp = true)
    {
        Mathf.Clamp(value, _min, _max);
        return (value - _min) / (float)(_max - _min);
    }
}
[System.Serializable]
public class IntRange : IRange<int>
{
    public int _min;
    public int _max;

    public int GetRandom()
    {
        return Random.Range(_min, _max);
    }
    public int Lerp(float factor, bool clamp = true)
    {
        if (clamp)
            factor = Mathf.Clamp01(factor);
        return (int)(_min + (_max - _min) * factor);
    }
    public float InvLerp(int value, bool clamp = true)
    {
        Mathf.Clamp(value, _min, _max);
        return (value - _min) / (float)(_max - _min);
    }
}
[System.Serializable]
public class ColorRange : IRange<Color>
{
    public Color _min;
    public Color _max;

    public Color GetRandom()
    {
        return new Color( Utils.Random01(), Utils.Random01(), Utils.Random01() );
    }
    public Color Lerp(float factor, bool clamp = true)
    {
        if (clamp)
            factor = Mathf.Clamp01(factor);
        return _min + (_max - _min) * factor;
    }
    public float InvLerp(Color value, bool clamp = true)
    {
        Utils.InvalidException();
        return -1;
    }
}

public static class Utils
{
    public static void Assert( bool condition) { if (!condition) ThrowException(); }
    public static void InvalidException() { throw new System.Exception("Invalid"); }
    public static void NYIException() { throw new System.Exception("NYI"); }
    public static void ThrowException() { throw new System.Exception("Unknown"); }
    public static float Random01() { return UnityEngine.Random.Range((float)0, (float)1); }
}