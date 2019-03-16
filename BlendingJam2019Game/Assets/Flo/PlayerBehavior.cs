using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehavior : MonoBehaviour
{
    public UnityEvent OnPickUpObject;
    public UnityEvent OnUseObject;

    public static PlayerBehavior Instance;
    public Transform Hand;

    [SerializeField]
    private Camera pointOfView;

    private Collectibles camTarget;

    private Vector3 eyePosition;
    private Vector3 lookTowards;
    private float lookDistance = 2.0f;

    private RaycastHit hit;

    Color colRay = Color.white;

    public const int maxWeight = 3;

    public int CurrentWeight
    {
        get
        {
            return
                oliveNbr * Collectibles.oliveWeight +
                cheesNbr * Collectibles.cheesWeight +
                mozzaNbr * Collectibles.mozzaWeight +
                anchoNbr * Collectibles.anchoWeight +
                oilllNbr * Collectibles.oilllWeight +
                pepprNbr * Collectibles.pepprWeight;
        }
    }

    public int oliveNbr = 0;
    public int cheesNbr = 0;
    public int mozzaNbr = 0;
    public int anchoNbr = 0;
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

                if (Input.GetButtonDown("Fire1"))
                {
                    if(cheesNbr > 0)
                    {
                        cheesNbr--;
                        GameObject.Find("PizzaSpin").GetComponent<Pizza>().StopTurning();
                    }
                }
            }

        }
        else if (camTarget != null)
        {
            camTarget.StopGlowing();
            camTarget = null;
        }

        Debug.DrawRay(eyePosition, lookTowards * lookDistance, colRay);
    }

    private void ThrowAwayAllIngredient()
    {
        oliveNbr = 0;
        cheesNbr = 0;
        mozzaNbr = 0;
        anchoNbr = 0;
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
            objLookedAt.PickUp();
        }
        OnPickUpObject.Invoke();
    }
    private void TryToPlaceObj(SpaceShipAssembly ship)
    {
        ship.PlaceIngredients();
        Debug.LogWarning("PLACE OBJ NOT IMPLEMENTED");
        OnUseObject.Invoke();
    }
}
