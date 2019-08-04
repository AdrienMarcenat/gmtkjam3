using System;
using UnityEngine;

public static class Extension
{
    public static void RegisterAsListener (this System.Object objectToNotify, string tag, params System.Type[] GameEventTypes)
    {
        GameEventManagerProxy.Get ().Register (objectToNotify, tag, GameEventTypes);
    }

    public static void UnregisterAsListener (this System.Object objectToNotify, string tag)
    {
        GameEventManagerProxy.Get ().Unregister (objectToNotify, tag);
    }

    public static void ToggleListener(this System.Object objectToToggle, string tag, bool toggle)
    {
        GameEventManagerProxy.Get().ToggleListener(objectToToggle, tag, toggle);
    }

    public static void RegisterToUpdate (this System.Object objectToNotify, bool isPausable, params EUpdatePass[] updatePassList)
    {
        UpdaterProxy.Get ().Register (objectToNotify, isPausable, updatePassList);
    }
    public static void RegisterToUpdate(this System.Object objectToNotify, params EUpdatePass[] updatePassList)
    {
        UpdaterProxy.Get().Register(objectToNotify, false, updatePassList);
    }

    public static void UnregisterToUpdate (this System.Object objectToNotify, params EUpdatePass[] updatePassList)
    {
        UpdaterProxy.Get ().Unregister (objectToNotify, updatePassList);
    }

    public static void SetX (this Vector3 v, float newX)
    {
        v.Set (newX, v.y, v.z);
    }

    public static void SetY (this Vector3 v, float newY)
    {
        v.Set (v.x, newY, v.z);
    }

    public static void SetZ (this Vector3 v, float newZ)
    {
        v.Set (v.x, v.y, newZ);
    }

    public static void DebugLog (this System.Object caller, System.Object message)
    {
        LoggerProxy.Get ().Log ("[" + caller.ToString() + "]" + message);
    }
    public static void DebugWarning(this System.Object caller, System.Object message)
    {
        LoggerProxy.Get().Warning("[" + caller.ToString() + "]" + message);
    }

    public static float ms_TileUnitRatio = 1f;

    public static int ToWorldUnit(this int unit)
    {
        return (int)(unit * ms_TileUnitRatio);
    }

    public static int ToTileUnit(this int unit)
    {
        return (int)(unit / ms_TileUnitRatio);
    }

    public static T[] SubArray<T>(this T[] data, int index, int length = -1)
    {
        if (length == -1)
        {
            length = data.Length - index;
        }
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static int Modulo(this int unit, int modulo)
    {
        int res = unit % modulo;
        if (res < 0)
        {
            return res + modulo;
        }
        else
        {
            return res;
        }
    }
}