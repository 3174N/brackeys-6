using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeToLive = 5f;
    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }
}
