using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBehavior : MonoBehaviour
{
    private const int dist = 60;
    ParticleSystem pS;

    private void Awake()
    {
        pS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (pS.isPlaying && Vector3.Distance(PlayerBehavior.Instance.transform.position, transform.position) > dist)
        {
            pS.Pause();
        }
        else if (pS.isStopped && Vector3.Distance(PlayerBehavior.Instance.transform.position, transform.position) < dist)
        {
            pS.Play();
        }
    }
}
