using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseCopy : MonoBehaviour
{
    public Animator origin;
    public Animator copy;
    public HumanPoseHandler origin_HPH;
    public HumanPoseHandler copy_HPH;
    public HumanPose humanPose;
    void Start()
    {
        origin_HPH=new HumanPoseHandler(origin.avatar,origin.transform);
        copy_HPH=new HumanPoseHandler(copy.avatar,copy.transform);
        copy.enabled=false;
        humanPose=new HumanPose();
    }

    void Update()
    {
        origin_HPH.GetHumanPose(ref humanPose);
        copy_HPH.SetHumanPose(ref humanPose);
    }
}
