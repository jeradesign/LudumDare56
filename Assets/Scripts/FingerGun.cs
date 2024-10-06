using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Hands;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FingerGun : MonoBehaviour
{
    private XRHandSubsystem _mHandSubsystem;
    public GameObject prefabBall;

    // Start is called before the first frame update
    void Start()
    {
        var handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);

        for (var i = 0; i < handSubsystems.Count; ++i)
        {
            var handSubsystem = handSubsystems[i];
            if (handSubsystem.running)
            {
                _mHandSubsystem = handSubsystem;
                break;
            }
        }

        // if (_mHandSubsystem != null)
        //     _mHandSubsystem.updatedHands += OnUpdatedHands;

        InvokeRepeating("FireBall", 1f, 1f);
    }

// Finger joint order:
//      Tip
//      Distal
//      Intermediate?
//      Proximal
//      Metacarpal
//
    void FireBall() {
        var fingertip = _mHandSubsystem.rightHand.GetJoint(XRHandJointID.IndexTip);
        var baseKnuckle = _mHandSubsystem.rightHand.GetJoint(XRHandJointID.IndexIntermediate);

        if (fingertip.TryGetPose(out Pose fingertipPose) &&
                baseKnuckle.TryGetPose(out Pose baseKnucklePose)) {
            var ball = Instantiate(prefabBall, fingertipPose.position, Quaternion.identity);
            var directionVector = fingertipPose.position - baseKnucklePose.position;
            directionVector.Normalize();
            directionVector *= 10;
            var rb = ball.GetComponent<Rigidbody>();
            rb.velocity = directionVector;
        }
    }
}
