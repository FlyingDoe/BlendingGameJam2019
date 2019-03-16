using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Collectibles : MonoBehaviour
{
    public bool canBePickedUp;
    private Renderer rd;
    private AudioSource aS;

    public enum Ingredient { cheese, mozzarella, olive, oil, pepnivy, pepper }
    public Ingredient typeOfIngredient;

    public const int oliveWeight = 1;
    public const int cheesWeight = 1;
    public const int mozzaWeight = 1;
    public const int pepniWeight = 2;
    public const int oilllWeight = 1;
    public const int pepprWeight = 1;

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
                case Ingredient.pepnivy:
                    return pepniWeight;
                case Ingredient.pepper:
                    return pepprWeight;
                default:
                    Debug.LogWarning("INGRDIENT NOT IMPLEMENTED, WEIGHT SET TO 1");
                    return 1;
            }
        }
    }

    private void Awake()
    {
        gameObject.tag = "Collectibles";
        rd = GetComponentInChildren<Renderer>();
        aS = GetComponentInChildren<AudioSource>();

        gameObject.name = typeOfIngredient.ToString();

        aS.playOnAwake = false;
        aS.loop = false;

        switch (typeOfIngredient)
        {
            case Ingredient.cheese:
                aS.clip = SfxManager.Instance.Sfx_squish;
                break;
            case Ingredient.mozzarella:
                aS.clip = SfxManager.Instance.Sfx_squish;
                break;
            case Ingredient.olive:
                aS.clip = SfxManager.Instance.Sfx_squish;
                break;
            case Ingredient.oil:
                aS.clip = SfxManager.Instance.Sfx_cork;
                break;
            case Ingredient.pepnivy:
                aS.clip = SfxManager.Instance.Sfx_squish;
                break;
            default:
                aS.clip = SfxManager.Instance.Sfx_squish;
                break;
        }
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
                PlayerBehavior.Instance.oilllNbr++;
                break;
            case Ingredient.pepnivy:
                PlayerBehavior.Instance.pepniNbr++;
                break;
            case Ingredient.pepper:
                PlayerBehavior.Instance.pepprNbr++;
                break;
            default:
                break;
        }
        rd.enabled = false;
        aS.Play();

        Destroy(gameObject, aS.clip.length);
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
