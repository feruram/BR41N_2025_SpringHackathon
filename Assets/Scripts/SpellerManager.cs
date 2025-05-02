using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellerManager : MonoBehaviour
{
    public Camera vrCamera;
    public float rayDistance = 2f;
    public float requiredGazeTime = 10.0f;
    public bool downwardGazeMode=false;
    private Timer timer;
    private GameObject currentTarget;
    private GameObject hitObject;
    [SerializeField]private float nowGazeTime=0;

    void Start()
    {
        timer = new Timer(requiredGazeTime);
    }

    void Update()
    {
        Vector3 gazeDirection=vrCamera.transform.forward;
        if(downwardGazeMode){
            gazeDirection=Quaternion.Euler(15f, 0f, 0f) * gazeDirection;
        }
        Ray ray = new Ray(vrCamera.transform.position, gazeDirection);
        RaycastHit hit;
        bool isSameTarget = false;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            hitObject = hit.collider.gameObject;
            if (System.Enum.TryParse(hitObject.name, out TargetName targetEnum))
            {
                if (hitObject == currentTarget)
                {
                    hitObject.GetComponent<Collider>().gameObject.GetComponent<HighLight>().SetHighlight();
                    isSameTarget = true;
                }
                else
                {
                    if(currentTarget!=null){
                        currentTarget.GetComponent<Collider>().gameObject.GetComponent<HighLight>().ClearHighlight();
                        Debug.Log(currentTarget.name);
                        Debug.Log(hitObject.name);
                        Debug.Log("out");
                    }
                    currentTarget = hitObject;
                    timer.Reset();
                    
                }
                nowGazeTime=timer.UpdateTimer(isSameTarget, Time.deltaTime);

                if (IsExternalInputActive(targetEnum))
                {
                    ExecuteFireFunction(targetEnum);
                    timer.Reset(); // 入力があったら即発射してリセット
                }
                else if (timer.IsTimeReached())
                {
                    ExecuteFireFunction(targetEnum);
                    timer.Reset(); // 偽装
                }
            }
            else
            {
                if(currentTarget!=null){
                currentTarget.GetComponent<Collider>().gameObject.GetComponent<HighLight>().ClearHighlight();
                Debug.Log(currentTarget.name);
                Debug.Log(hitObject.name);
                Debug.Log("out");
                }
                currentTarget = null;
                timer.Reset();
            }
        }
        else
        {
            if(currentTarget!=null){
                currentTarget.GetComponent<Collider>().gameObject.GetComponent<HighLight>().ClearHighlight();
                Debug.Log(currentTarget.name);
                Debug.Log(hitObject.name);
                Debug.Log("out");
            }
            currentTarget = null;
            timer.Reset();
        }
    }

    bool IsExternalInputActive(TargetName name)
    {
        switch (name)
        {
            case TargetName.RED:
                return REDActive();
            case TargetName.GREEN:
                return GREENActive();
            case TargetName.BLUE:
                return BLUEActive();
            default:
                return false;
        }
    }

    void ExecuteFireFunction(TargetName name)
    {
        switch (name)
        {
            case TargetName.RED:
                FireRED();
                break;
            case TargetName.GREEN:
                FireGREEN();
                break;
            case TargetName.BLUE:
                FireBLUE();
                break;
        }
    }

    // 仮
    bool REDActive() => false;
    bool GREENActive() => false;
    bool BLUEActive() => false;

    void FireRED() => Debug.Log("RED fired!");
    void FireGREEN() => Debug.Log("GREEN fired!");
    void FireBLUE() => Debug.Log("BLUE fired!");
}
public enum TargetName
{
    RED,
    GREEN,
    BLUE
}
