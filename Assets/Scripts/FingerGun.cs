using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
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

    void FireBall() {
        var ball = Instantiate(prefabBall, new Vector3(0, 2, 0), Quaternion.identity);
        var rb = ball.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 2, 10);
    }
}
