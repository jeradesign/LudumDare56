using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    AudioSource m_Bell;
    void Start()
    {
        m_Bell = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        m_Bell.Play();
    }
}
