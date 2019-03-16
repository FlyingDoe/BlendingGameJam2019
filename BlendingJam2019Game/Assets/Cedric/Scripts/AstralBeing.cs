﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralBeing : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 eyePosition;
    private Vector3 lookTowards;
    private float lookDistance = 50.0f;
    private int moveDirection = 3;
    public int moveSpeed = 3;
    private Vector3 initPos;
    private bool canChomp;
    public int timeBetweenChomps = 5;
    public ParticleSystem burst;
    private bool hitByMissile;

    AudioSource aS_growling;
    AudioSource aS_effect;

    private void Awake()
    {
        aS_growling = GetComponents<AudioSource>()[0];
        aS_effect = GetComponents<AudioSource>()[1];

        aS_growling.loop = true;
        aS_growling.loop = true;
    }

    void Start()
    {
        hitByMissile = false;
        canChomp = true;
        initPos = transform.position;
        moveSpeed = moveDirection;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) HitByMissile();
        if (!hitByMissile)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDirection);
            eyePosition = transform.position + transform.forward * 0.4f;
            lookTowards = transform.forward;

            if (Physics.Raycast(eyePosition, lookTowards, out hit, transform.localScale.x /1.5f))
            {
                if (hit.collider.tag == "FoodNormal" || hit.collider.tag == "FoodCollant" || hit.collider.tag == "FoodGlissant" || hit.collider.tag == "JumpingMozza")
                {
                    moveDirection = -moveSpeed;
                    hit.transform.gameObject.SetActive(false);
                    canChomp = true;
                    burst.Play();
                    transform.localScale *= 1.03f;
                }
                else if(hit.collider.tag == "FoodCentralPiece")
                {
                    moveDirection = -moveSpeed;
                    canChomp = true;
                }

            }
            if (transform.position.z < initPos.z && canChomp)
            {
                StartCoroutine(WaitBeforeChomp(timeBetweenChomps));
            }
        }


    }

    public void HitByMissile()
    {
        hitByMissile = true;
        StartCoroutine(WeirdRotation());
    }

    IEnumerator WeirdRotation()
    {
        moveDirection = 0;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(1);
        moveDirection = moveSpeed;
        hitByMissile = false;
    }

    IEnumerator WaitBeforeChomp(int time)
    {
        canChomp = false;
        moveDirection = 0;
        yield return new WaitForSeconds(time);
        moveDirection = moveSpeed;
    }
}
