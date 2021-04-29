using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    static T instance;
    public T Instance => instance;
    public static bool IsInitialized { get { return instance != null; } }

    protected virtual void Awake()
    {
        if (Instance == null) instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this) instance = null;
    }
}
