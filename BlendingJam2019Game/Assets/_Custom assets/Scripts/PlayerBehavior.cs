using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehavior : MonoBehaviour
{
    private Animator anim;

    public UnityEvent OnPickUpObject;
    public UnityEvent OnUseObject;

    public static PlayerBehavior Instance;
    public Transform Hand;

    [SerializeField]
    private Camera pointOfView;
    [SerializeField]
    private MissileBehavior missilePrefab;

    private Collectibles camTarget;

    private Vector3 eyePosition;
    private Vector3 lookTowards;
    private float lookDistance = 2.0f;

    private RaycastHit hit;

    Color colRay = Color.white;

    public const int maxWeight = 3;
    public Pizza pizza;

    public myUI ui;

    public int CurrentWeight
    {
        get
        {
            return
                oliveNbr * Collectibles.oliveWeight +
                cheesNbr * Collectibles.cheesWeight +
                mozzaNbr * Collectibles.mozzaWeight +
                pepniNbr * Collectibles.pepniWeight +
                oilllNbr * Collectibles.oilllWeight +
                pepprNbr * Collectibles.pepprWeight;
        }
    }

    public int oliveNbr = 0;
    public int cheesNbr = 0;
    public int mozzaNbr = 0;
    public int pepniNbr = 0;
    public int oilllNbr = 0;
    public int pepprNbr = 0;

    private void Awake()
    {
        Instance = this;
        if (OnPickUpObject == null)
        {
            OnPickUpObject = new UnityEvent();
        }
        if (OnUseObject == null)
        {
            OnUseObject = new UnityEvent();
        }

        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Debug.Log("Player instantiated");
    }

    void Update()
    {
        eyePosition = pointOfView.transform.position + pointOfView.transform.forward * 0.4f;
        lookTowards = pointOfView.transform.forward;

        colRay = Color.red;

        if (Physics.Raycast(eyePosition, lookTowards, out hit, lookDistance))
        {
            if (hit.collider.tag == "Collectibles")
            {
                Collectibles objLookedAt = hit.collider.gameObject.GetComponent<Collectibles>();

                colRay = Color.green;

                if (objLookedAt.canBePickedUp)
                {

                    objLookedAt.StartGlowing(maxWeight - (CurrentWeight + objLookedAt.Weight) >= 0);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        TryToPickUpObj(objLookedAt);
                    }

                    camTarget = objLookedAt;
                }
            }
            else if (hit.collider.tag == "SpaceShip")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    TryToPlaceObj(hit.collider.GetComponent<SpaceShipAssembly>());
                }
            }
            else if (hit.collider.tag == "Bin")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    ThrowAwayAllIngredient();
                }
            }
            else if (hit.collider.tag == "AxeCentral")
            {
                Debug.Log("hitting axe");
                if (Input.GetButtonDown("Fire1") && mozzaNbr > 0)
                {
                    //slow pillar
                    Debug.Log("slowed pillar");
                    pizza.StopTurning();
                    mozzaNbr--;
                    ui.UpdateValues();
                }
            }

        }
        else if (camTarget != null)
        {
            camTarget.StopGlowing();
            camTarget = null;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(ShootMissile());
        }
        if (Input.GetKeyDown(KeyCode.P) && pepniNbr > 0)
        {
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().MangerLePiment();
            pepprNbr--;
            OnUseObject.Invoke();
        }

        if (transform.position.y < -40)
        {
            PlayerFail();
        }


        Debug.DrawRay(eyePosition, lookTowards * lookDistance, colRay);
    }

    public void PlayerFail()
    {
        CanvasManagerBehaviour.instance.OnPlayerFailed();
    }

    // -------------------------------------------------------------

    private void ThrowAwayAllIngredient()
    {
        anim.SetTrigger("Place");
        oliveNbr = 0;
        cheesNbr = 0;
        mozzaNbr = 0;
        pepniNbr = 0;
        oilllNbr = 0;
        pepprNbr = 0;
        OnUseObject.Invoke();
    }

    private void TryToPickUpObj(Collectibles objLookedAt)
    {
        if (maxWeight - (CurrentWeight + objLookedAt.Weight) < 0)
        {
            Debug.LogWarning("REFUSE PICKUP NOT IMPLEMENTED ?");
        }
        else
        {
            anim.SetTrigger("Grab");
            objLookedAt.PickUp();
        }
        OnPickUpObject.Invoke();
    }
    private void TryToPlaceObj(SpaceShipAssembly ship)
    {
        anim.SetTrigger("Place");
        ship.PlaceIngredients();
        Debug.LogWarning("PLACE OBJ NOT IMPLEMENTED");
        OnUseObject.Invoke();
    }

    // -------------------------------------------------------------

    private IEnumerator ShootMissile()
    {
        if (pepprNbr > 0 && oilllNbr > 0)
        {
            anim.SetTrigger("Shake");
            pepprNbr--;
            oilllNbr--;
            yield return new WaitForSeconds(1.1f);

            MissileBehavior missile = Instantiate(missilePrefab, transform.position + transform.forward * 0.5f, Quaternion.identity);
            missile.transform.LookAt(transform.position + transform.forward * 2);
        }
        else
        {
            Debug.LogWarning("PUT NOPE SOUND HERE");
            yield return null;
        }
        OnUseObject.Invoke();
    }


}
