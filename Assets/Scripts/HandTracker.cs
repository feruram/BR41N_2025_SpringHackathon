using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    public Transform tracker;
    public bool flag=true;
    private Vector3 prePos=new Vector3(0f,0f,0f);
    private Quaternion preRot;
    void FixedUpdate()
    {
        if(flag){
            tracker.position=this.transform.position;
            tracker.rotation=this.transform.rotation;
            prePos=this.transform.position;
            preRot=this.transform.rotation;
        }{
            tracker.position=prePos;
            tracker.rotation=preRot;
        }
        flag=true;
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag!="weapon"&&other.gameObject.tag!="player"){
            flag=false;
        }
        
    }
}
