using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBaek<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
}
