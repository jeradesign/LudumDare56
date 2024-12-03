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
        if (other.contacts.Length == 0)
        {
            return;
        }

        ContactPoint collisionPoint = other.contacts[0];
        Vector2 my2dPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 collision2dPosition = new Vector2(collisionPoint.point.x, collisionPoint.point.y);
        float distance = Vector2.Distance(my2dPosition, collision2dPosition);
        Debug.Log("distance = " + distance);
        m_Bell.Play();
        if (distance < .1)
        {
            fingerGun.AddScore(50);
        }
        else if (distance < 0.2)
        {
            fingerGun.AddScore(40);
        }
        else if (distance < 0.3)
        {
            fingerGun.AddScore(30);
        }
        else if (distance < 0.4)
        {
            fingerGun.AddScore(20);
        }
        else
        {
            fingerGun.AddScore(10);
        }
    }
}
