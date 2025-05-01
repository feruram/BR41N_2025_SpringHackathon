using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcalbrationController : MonoBehaviour
{
    public bool enter=false;
    public bool t=false;
    public bool space=false;
    public TOffsetter offsetter;
    public IKSetter setter;
    void Update()
    {
        enter=Input.GetKeyDown(KeyCode.Return);
        t=Input.GetKeyDown(KeyCode.T);
        space=Input.GetKeyDown(KeyCode.Space);
        if(t){
            offsetter.StartCalibration();
            setter.beforeCalibration();
        }
        if(enter){
            offsetter.OnEnter();
            setter.afterCalibration();
        }
        if(space){
            setter.SetIKTarget();
        }
    }
}
