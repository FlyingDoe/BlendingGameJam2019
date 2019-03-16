using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = 0.01f;
    private float lifetime = 5f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Go forward
        transform.Translate(Vector3.forward * speed);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Ennemy")
        {
            Explode();
        }
    }

    private void Explode()
    {
        throw new NotImplementedException();
    }
}
