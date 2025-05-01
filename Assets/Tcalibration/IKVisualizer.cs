using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKVisualizer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public Transform chest;
    public Transform waist;
    public Transform rightKnee;
    public Transform leftKnee;
    public Transform rightToe;
    public Transform leftToe;
    public ControllerInputManager input;
    public Transform bodyRoot;
    public Transform ikHead;
    public Transform ikLhand;
    public Transform ikRHand;
    public Transform ikChest;
    public Transform ikHips;
    public Transform ikLLLeg;
    public Transform ikRLLeg;
    public Transform ikLToes;
    public Transform ikRToes;
    public Animator bodyAnimator;
    private void Start(){
        bodyAnimator=bodyRoot.GetComponentInChildren<Animator>();
        ikHead=bodyAnimator.GetBoneTransform(HumanBodyBones.Head);
        Debug.Log(ikHead.position);
        ikChest=bodyAnimator.GetBoneTransform(HumanBodyBones.Chest);
        ikHips=bodyAnimator.GetBoneTransform(HumanBodyBones.Hips);
        ikLhand=bodyAnimator.GetBoneTransform(HumanBodyBones.LeftHand);
        ikRHand=bodyAnimator.GetBoneTransform(HumanBodyBones.RightHand);
        ikLLLeg=bodyAnimator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        ikRLLeg=bodyAnimator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        ikLToes=bodyAnimator.GetBoneTransform(HumanBodyBones.LeftToes);
        ikRToes=bodyAnimator.GetBoneTransform(HumanBodyBones.RightToes);

    }
    private void Update(){
        DCTransform(ikHead,head);
        DCTransform(ikChest,chest);
        DCTransform(ikHips,waist);
        DCTransform(ikLhand,leftHand);
        DCTransform(ikRHand,rightHand);
        DCTransform(ikLLLeg,leftKnee);
        DCTransform(ikRLLeg,rightKnee);
        DCTransform(ikLToes,leftToe);
        DCTransform(ikRToes,rightToe);
    }
    private void DCTransform(Transform src,Transform dsc){
        Vector3 pos=src.position;
        Quaternion rot=src.rotation;
        dsc.position=pos;
        dsc.rotation=rot;
    }
    
    
}
