using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public bool x=false;
    public bool y=false;
    public bool z=false;
    public float speed=10.0f;
    void Update()
    {
        if(x)this.transform.Rotate(speed*Time.deltaTime,0,0);
        if(y)this.transform.Rotate(0,speed*Time.deltaTime,0);
        if(z)this.transform.Rotate(0,0,speed*Time.deltaTime);
    }
}
