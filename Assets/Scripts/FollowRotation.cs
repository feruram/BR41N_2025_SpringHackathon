using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace raspberly.ovr
{
    public class FollowRotation : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float followRotateSpeed = 0.02f;
        [SerializeField] private float rotateSpeedThreshold = 0.9f;
        private Quaternion rot;
        private Quaternion rotDif;

        private void Start ()
        {
            if(!target) target = Camera.main.transform;
        }

        private void LateUpdate ()
        {
            rotDif = target.rotation * Quaternion.Inverse(transform.rotation);
            rot = target.rotation;
            rot.x = 0;
            rot.z = 0;
            transform.rotation=rot;
        }
    }
}