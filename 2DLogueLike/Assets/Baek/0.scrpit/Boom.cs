using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    void Boomreturn()
    {
        ObjectPoolBaek.Instance.BoomReturn(gameObject);
    }
}
