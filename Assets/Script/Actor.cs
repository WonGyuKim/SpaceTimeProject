using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    // Start is called before the first frame update
    float mass;

    void Start()
    {
        mass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Mass
    {
        set { mass = value; }
        get { return mass; }
    }
}
