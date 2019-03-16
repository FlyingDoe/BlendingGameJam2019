using System.Collections;
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
                    anim.SetTrigger("nomTrig");
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
        anim.SetTrigger("OpenMouth");
        moveDirection = 0;
        for (int i = 0; i < 100; i++)
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
}
