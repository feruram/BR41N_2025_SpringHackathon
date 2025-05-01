using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class IKSetter : MonoBehaviour
{
    private VRIK ik;
    public TOffsetter offsetter;
    [SerializeField]private Transform[] trackers;
    public void beforeCalibration(){
        ik=offsetter.vrm.GetComponent<VRIK>();
        ik.fixTransforms=true;
        ik.solver.IKPositionWeight=0;
    }
    public void afterCalibration(){
        offsetter.vrm.localPosition=new Vector3(0,0,0);
        ik=offsetter.vrm.GetComponent<VRIK>();
        ik.fixTransforms=false;;
        ik.solver.IKPositionWeight=1;
    }
    public void SetIKTarget(){
        ik=offsetter.vrm.GetComponent<VRIK>();
        trackers=offsetter.GetTrackers();
        if(trackers[0]!=null)
            ik.solver.spine.headTarget=trackers[0];
        if(trackers[1]!=null)
            ik.solver.spine.chestGoal=trackers[1];
        if(trackers[2]!=null)
            ik.solver.spine.pelvisTarget=trackers[2];
        if(trackers[3]!=null)
            ik.solver.leftArm.target=trackers[3];
        if(trackers[4]!=null)
            ik.solver.leftArm.bendGoal=trackers[4];
        if(trackers[5]!=null)
            ik.solver.rightArm.target=trackers[5];
        if(trackers[6]!=null)
            ik.solver.rightArm.bendGoal=trackers[6];
        if(trackers[7]!=null)
            ik.solver.leftLeg.target=trackers[7];
        if(trackers[8]!=null)
            ik.solver.leftLeg.bendGoal=trackers[8];
        if(trackers[9]!=null)
            ik.solver.rightLeg.target=trackers[9];
        if(trackers[10]!=null)
            ik.solver.rightLeg.bendGoal=trackers[10];
        
    }
}
