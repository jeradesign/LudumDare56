using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    AudioSource m_Bell;

    public GameObject ScriptHolder;
    public int scoreIncrement;
    private FingerGun fingerGun;

    void Start()
    {
        m_Bell = GetComponent<AudioSource>();
        fingerGun = ScriptHolder.GetComponent<FingerGun>();
    }
    private void OnCollisionEnter(Collision other)
    {
        m_Bell.Play();
        fingerGun.AddScore(scoreIncrement);
    }
}
