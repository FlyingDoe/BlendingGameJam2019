using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = 0.01f;
    private float lifetime = 50f;
    private bool moving = true;

    AudioSource aS;
    private AstralBeing ennemy;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        ennemy = GameObject.Find("Planete").GetComponent<AstralBeing>();
    }

    void FixedUpdate()
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("triggered");
        if (other.gameObject == ennemy.gameObject)
        {
            Debug.Log("triggered ennemy");

            ennemy.HitByMissile();
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
