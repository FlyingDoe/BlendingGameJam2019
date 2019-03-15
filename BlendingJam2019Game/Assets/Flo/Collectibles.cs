using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public bool canBePickedUp;
    private Renderer rd;

    public enum Ingredient { cheese, mozzarella, olive, oil, crust, anchovy }
    public Ingredient typeOfIngredient;

    public const int oliveWeight = 1;
    public const int cheesWeight = 1;
    public const int mozzaWeight = 1;
    public const int anchoWeight = 2;
    public const int crustWeight = 3;
    public const int oilllWeight = 1;

    public int Weight
    {
        get
        {
            switch (typeOfIngredient)
            {
                case Ingredient.cheese:
                    return cheesWeight;
                case Ingredient.mozzarella:
                    return mozzaWeight;
                case Ingredient.olive:
                    return oliveWeight;
                case Ingredient.oil:
                    return oilllWeight;
                case Ingredient.crust:
                    return crustWeight;
                case Ingredient.anchovy:
                    return anchoWeight;
                default:
                    Debug.LogWarning("INGRDIENT NOT IMPLEMENTED, WEIGHT SET TO 1");
                    return 1;
            }
        }
    }

    private void Awake()
    {
        gameObject.tag = "Collectibles";
        rd = GetComponent<Renderer>();

        gameObject.name = typeOfIngredient.ToString();
    }

    void Start()
    {
        canBePickedUp = true;
    }

    public void PickUp()
    {
        StopGlowing();

        // HERE ADD TO INVENTORY
        switch (typeOfIngredient)
        {
            case Ingredient.cheese:
                PlayerBehavior.Instance.cheesNbr++;
                break;
            case Ingredient.mozzarella:
                PlayerBehavior.Instance.mozzaNbr++;
                break;
            case Ingredient.olive:
                PlayerBehavior.Instance.oliveNbr++;
                break;
            case Ingredient.oil:
                PlayerBehavior.Instance.oliveNbr++;
                break;
            case Ingredient.crust:
                PlayerBehavior.Instance.crustNbr++;
                break;
            case Ingredient.anchovy:
                PlayerBehavior.Instance.anchoNbr++;
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }

    public void StartGlowing(bool positive)
    {
        if (positive)
        {
            rd.material.color = Color.yellow;
        }
        else
        {
            rd.material.color = Color.red;
        }
    }
    public void StopGlowing()
    {
        rd.material.color = Color.white;
    }
}
