using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralBeing : MonoBehaviour
{

    private RaycastHit hit;
    private Vector3 eyePosition;
    private Vector3 lookTowards;
    private float lookDistance = 17.0f;
    private int moveDirection = 3;
    public int moveSpeed = 3;
    private Vector3 initPos;
    private bool canChomp;
    public int timeBetweenChomps = 5;
    // Use this for initialization
    void Start()
    {
        canChomp = true;
        initPos = transform.position;
        moveSpeed = moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+ moveDirection);
        eyePosition = transform.position + transform.forward * 0.4f;
        lookTowards = transform.forward;

        if (Physics.Raycast(eyePosition, lookTowards, out hit, lookDistance))
        {
            if (hit.collider.tag == "FoodNormal")
            {
                moveDirection = -moveSpeed;
                hit.transform.gameObject.SetActive(false);
                canChomp = true;

            }

        }
        if (transform.position.z < initPos.z && canChomp)
        {
            Debug.Log("back at init poe");
            StartCoroutine(WaitBeforeChomp(timeBetweenChomps));
        }

    }

    IEnumerator WaitBeforeChomp(int time)
    {
        canChomp = false;
        moveDirection = 0;
        yield return new WaitForSeconds(time);
        moveDirection = moveSpeed;
    }
}
