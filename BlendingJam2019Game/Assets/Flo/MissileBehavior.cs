using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = 0.01f;
    private float lifetime = 5f;
    private bool moving = true;

    AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Go forward
        if (moving)
        {
            transform.Translate(Vector3.forward * speed);
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Explode();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemy")
        {
            Explode();
        }
    }

    private void Explode()
    {
        moving = false;
        aS.Stop();
        aS.time = 0;
        aS.loop = false;
        aS.spatialBlend = 0.5f;
        aS.clip = SfxManager.Instance.Sfx_explosion;
        aS.Play();
        //yield return new WaitForSeconds(aS.clip.length);
        Destroy(gameObject, aS.clip.length);
    }
}
