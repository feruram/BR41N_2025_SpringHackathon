using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
//using UnityEditor.Callbacks;
using UnityEngine;
using Valve.VR.InteractionSystem.Sample;
public class TOffsetter : MonoBehaviour
{
    public Transform head;
    public Transform chest;
    public Transform waist;
    public Transform leftHand;
    public Transform leftElbow;
    public Transform rightHand;
    public Transform rightElbow;
    public Transform leftToe;
    public Transform leftKnee;
    public Transform rightToe;
    public Transform rightKnee;
    public Transform vrm;
    private Animator animator;
    public bool onEnter;
    public bool onUpArrow;
    public bool onDownArrow;
    public float headOffsetZ=0;
    private Reference reference;
    private bool alreadyRunning=false;
    private bool alreadySetOffset=false;
    public bool alreadySet=false;
    private  bool[] isTrackers;
    private Transform[] trackers;
    private TrackerData[] trackerData;
    private GameObject[] adjusted_trackers;
    
    private void Initialize()
    {
        animator=vrm.GetComponentInChildren<Animator>();
        reference=new Reference(animator);
        onEnter=false;
        isTrackers=new bool[11];
        isTrackers=NullCheckArray(head,chest,waist,leftHand,leftElbow,rightHand,rightElbow,leftToe,leftKnee,rightToe,rightKnee,true);
        
        for(int i=0;i<isTrackers.Length;i++){
            Debug.Log(isTrackers[i]);
            if(alreadySetOffset&&isTrackers[i]){
                Destroy(adjusted_trackers[i]);
            }
        }
        adjusted_trackers=new GameObject[11];
        trackers=GetTransform();
        alreadySet=false;
    }
    private Transform[] GetTransform(){
        return new Transform[11]{
            head,chest,waist,leftHand,leftElbow,rightHand,rightElbow,leftToe,leftKnee,rightToe,rightKnee
        };
    }
    public void StartCalibration(){
        if(!alreadyRunning){
            StartCoroutine(Calibration());
        }
    }
    public Transform[] GetTrackers(){
        if(alreadySetOffset){
            Transform[] send=new Transform[11];
            for(int i=0;i<isTrackers.Length;i++){
                if(isTrackers[i]){
                    send[i]=adjusted_trackers[i].transform;
                }else{
                    send[i]=null;
                }
                
            }
        return send;
        }else{
            return null;
        }
    }
    private IEnumerator Calibration(){
        alreadyRunning=true;
        Initialize();
        while(true){
            yield return null;
            Vector3 headPos=worldToRootLocal(reference.head.position);
            if(Input.GetKey(KeyCode.UpArrow)||onUpArrow){
                headOffsetZ+=0.01f;
            }
            if(Input.GetKey(KeyCode.DownArrow)||onDownArrow){
                headOffsetZ-=0.01f;
            }
            Vector3 headOffset=head.localPosition;
            headOffset.z+=headOffsetZ;
            Vector3 hmdPos=head.position+headOffset;
            hmdPos.y=0;
            headPos.y=0;
            vrm.position=hmdPos-headPos;

            if(onEnter){
                onEnter=false;
                CalcOffset();
                break;
            }
        }
        alreadyRunning=false;
        CalcDone();
    }
    private void CalcOffset(){
        Debug.Log("setting start");
        Transform[] references=new Transform[11]{
            reference.head,
            reference.chest,
            reference.pelvis,
            reference.leftHand,
            reference.leftForearm,
            reference.rightHand,
            reference.rightForearm,
            reference.leftToes,
            reference.leftCalf,
            reference.rightToes,
            reference.rightCalf
            };
            
        for(int i=0;i<trackers.Length;i++){
            if(isTrackers[i]){
                adjusted_trackers[i]=new GameObject();
                adjusted_trackers[i].transform.SetParent(trackers[i]);
                adjusted_trackers[i].name=Enum.GetName(typeof(TrackerName),i)+"Offset";
                adjusted_trackers[i].transform.localPosition=trackers[i].transform.InverseTransformPoint(references[i].position);
                Quaternion worldRotOfReference=references[i].rotation;
                Quaternion localRotForAdjustedTracker=Quaternion.Inverse(trackers[i].rotation)*worldRotOfReference;
                //adjusted_trackers[i].AddComponent<MeshFilter>().mesh = CreateCubeMesh();
                //adjusted_trackers[i].AddComponent<MeshRenderer>();
                adjusted_trackers[i].transform.localScale=new Vector3(1,1,1);
                adjusted_trackers[i].transform.localRotation=localRotForAdjustedTracker;
            }else{
                adjusted_trackers[i]=null;
            }
        }
        alreadySetOffset=true;
    }
    Mesh CreateCubeMesh()
    {
        return GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>().sharedMesh;
    }
    public void OnEnter(){
        if(onEnter){
            onEnter=false;
        }else{
            onEnter=true;
        }
    }
    private Vector3 worldToRootLocal(Vector3 pos){
        return vrm.transform.InverseTransformPoint(pos);
    }
    private class Reference{
        public Transform head;
        public Transform chest;
        public Transform pelvis;
        public Transform leftHand;
        public Transform leftForearm;
        public Transform rightHand;
        public Transform rightForearm;
        public Transform leftToes;
        public Transform leftCalf;
        public Transform rightToes;
        public Transform rightCalf;
        public Reference(Animator animator){
            this.head = animator.GetBoneTransform(HumanBodyBones.Head);
            this.chest = animator.GetBoneTransform(HumanBodyBones.Chest);
            this.pelvis = animator.GetBoneTransform(HumanBodyBones.Hips);
            this.leftForearm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            this.leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
            this.rightForearm=animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
            this.rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
            this.leftCalf = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
            this.leftToes = animator.GetBoneTransform(HumanBodyBones.LeftToes);
            this.rightCalf = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
            this.rightToes = animator.GetBoneTransform(HumanBodyBones.RightToes);
        }
    }
    private enum TrackerName{
        Head,
        Chest,
        Waist,
        LeftHand,
        LeftElbow,
        RightHand,
        RightElbow,
        LeftToes,
        LeftKnee,
        RightToes,
        RightKnee 
    }
    private class TrackerData{
        Vector3 pos;
        Quaternion rot;
    }
    private void CalcDone(){
        alreadySet=true;
    }
    private bool IsNull<T>(T obj) where T : class{
        var unityObj = obj as UnityEngine.Object;
        if (!object.ReferenceEquals(unityObj, null)){
            return unityObj == null;
        }
        else
        {
            return obj == null;
        }
    }
    private bool[] NullCheckArray<T>(T obj1,T obj2,T obj3, T obj4,T obj5,T obj6,T obj7,T obj8,T obj9,T obj10,T obj11,bool not) where T : class{
        bool[] checkArray=new bool[11];
        if(not){
            checkArray[0]=!IsNull(obj1);
            checkArray[1]=!IsNull(obj2);
            checkArray[2]=!IsNull(obj3);
            checkArray[3]=!IsNull(obj4);
            checkArray[4]=!IsNull(obj5);
            checkArray[5]=!IsNull(obj6);
            checkArray[6]=!IsNull(obj7);
            checkArray[7]=!IsNull(obj8);
            checkArray[8]=!IsNull(obj9);
            checkArray[9]=!IsNull(obj10);
            checkArray[10]=!IsNull(obj11);
        }else{
            checkArray[0]=IsNull(obj1);
            checkArray[1]=IsNull(obj2);
            checkArray[2]=IsNull(obj3);
            checkArray[3]=IsNull(obj4);
            checkArray[4]=IsNull(obj5);
            checkArray[5]=IsNull(obj6);
            checkArray[6]=IsNull(obj7);
            checkArray[7]=IsNull(obj8);
            checkArray[8]=IsNull(obj9);
            checkArray[9]=IsNull(obj10);
            checkArray[10]=IsNull(obj11);
        }
        return checkArray;
    }
}

