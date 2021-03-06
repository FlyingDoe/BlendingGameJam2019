﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralBeing : MonoBehaviour
{
    private const int rotationLoopNbr = 82;
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

    int partEaten = 0;

    AudioSource aS_growling;
    AudioSource aS_effect;
    Animator anim;

    Quaternion initRotation;

    private void Awake()
    {
        initRotation = transform.rotation;
        anim = GetComponent<Animator>();

        aS_growling = GetComponents<AudioSource>()[0];
        aS_effect = GetComponents<AudioSource>()[1];

        aS_growling.loop = true;
        aS_growling.playOnAwake = true;

        aS_growling.clip = SfxManager.Instance.Sfx_growlLoop;
        aS_growling.Play();

        aS_effect.loop = false;
        aS_effect.playOnAwake = false;
    }

    void Start()
    {
        hitByMissile = false;
        canChomp = true;
        initPos = transform.position;
        moveSpeed = moveDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Missile" && !hitByMissile)
        {
            Debug.Log("triggerhit");

            HitByMissile();
        }
        else if(other.tag == "Player")
        {
            PlayerBehavior.Instance.PlayerFail();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) HitByMissile();
        if (!hitByMissile)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDirection);
            transform.Translate(Vector3.forward * moveDirection);
            eyePosition = transform.position + transform.forward * 0.4f;
            lookTowards = transform.forward;

            if (Physics.Raycast(eyePosition, lookTowards, out hit, transform.localScale.x / 1.5f))
            {
                if (hit.collider.tag == "FoodNormal" || hit.collider.tag == "FoodCollant" || hit.collider.tag == "FoodGlissant" || hit.collider.tag == "JumpingMozza")
                {
                    anim.SetTrigger("nomTrig");
                    moveDirection = -moveSpeed;
                    hit.transform.gameObject.SetActive(false);
                    canChomp = true;
                    burst.Play();
                    partEaten++;
                    if (partEaten >=24)
                    {
                        PlayerBehavior.Instance.PlayerFail();
                    }
                    transform.localScale *= 1.03f;
                }
                else if (hit.collider.tag == "FoodCentralPiece")
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
        PlayWeirdSound();
        anim.SetTrigger("OpenMouth");
        moveDirection = 0;
        for (int i = 0; i < rotationLoopNbr; i++)
        {
            yield return new WaitForSeconds(.3f);
            transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
        transform.rotation = initRotation;
        yield return new WaitForSeconds(1);
        moveDirection = moveSpeed;
        hitByMissile = false;
        anim.SetTrigger("CloseMouth");
    }

    IEnumerator WaitBeforeChomp(int time)
    {
        canChomp = false;
        moveDirection = 0;
        yield return new WaitForSeconds(time);
        moveDirection = moveSpeed;
        anim.SetTrigger("OpenMouth");
    }

    public void PlayNom()
    {
        aS_effect.clip = SfxManager.Instance.Sfx_eat;
        aS_effect.Play();
    }
    public void PlayWeirdSound()
    {
        aS_effect.clip = SfxManager.Instance.Sfx_pain;
        aS_effect.Play();
    }
}
