using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.XR.Hands;

public class FingerGun : MonoBehaviour
{
    private XRHandSubsystem _mHandSubsystem;

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

        if (_mHandSubsystem != null)
            _mHandSubsystem.updatedHands += OnUpdatedHands;

    }

    // Update is called once per frame
    void OnUpdatedHands(XRHandSubsystem subsystem,
        XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags,
        XRHandSubsystem.UpdateType updateType)
    {
        switch(updateType)
        {
            case XRHandSubsystem.UpdateType.BeforeRender:
                var trackingData = subsystem.rightHand.GetJoint(XRHandJointID.IndexTip);
                if (trackingData.TryGetPose(out Pose pose))
                {
                    this.gameObject.transform.position = pose.position;
                    this.gameObject.transform.rotation = pose.rotation;
                }
            break;
        }
    }
}
