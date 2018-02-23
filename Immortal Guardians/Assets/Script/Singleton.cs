using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An abstract class used to access scripts

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour{

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }
}
