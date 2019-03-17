using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSliceBehaviour : MonoBehaviour
{

    // morceaux faisant parti de la slice
    public List<IngredientSpawnerBehaviour> pizzaPartsSpawner = new List<IngredientSpawnerBehaviour>();

    private Collectibles[] ingredientsType { get { return PrefabManager.Instance.collectibles; } }

    // pourcentage de presence de chaque ingredient
    [Range(0.0f, 1.0f)]
    public List<float> pourcentages = new List<float>();

    // Use this for initialization
    void Start()
    {
        // remplissage des pourcentages manquant a 0.0f
        if (pourcentages.Count < ingredientsType.Length)
        {
            int initialCount = pourcentages.Count;
            for (int i = initialCount; i < ingredientsType.Length; ++i)
                pourcentages.Add(0.0f);
        }
        else if (pourcentages.Count > ingredientsType.Length)
        {
            // suppression des pourcentages supplementaires
            int initialCount = pourcentages.Count;
            for (int i = initialCount - 1; i >= ingredientsType.Length; ++i)
                pourcentages.RemoveAt(i);
        }

        float sum = 0.0f;
        for (int i = 0; i < pourcentages.Count; ++i)
            sum += pourcentages[i];

        // envoyer les pourcentages vers les spawners de chaque pizza
        for (int i = 0; i < pizzaPartsSpawner.Count; ++i)
        {
            pizzaPartsSpawner[i].tag = tag;
            if(sum != 0.0f)
                pizzaPartsSpawner[i].setIngredients(pourcentages, ingredientsType);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        float sum = 0;
        for (int i = 0; i < pourcentages.Count; ++i)
            sum += pourcentages[i];

        if (sum > 1.0f)
        {
            float diff = (sum - 1.0f);
            int i = 0;
            while (diff > 0)
            {
                if (pourcentages[i] >= diff)
                {
                    pourcentages[i] -= diff;
                    diff = 0;
                }
                else if (pourcentages[i] > 0.0f)
                {
                    diff -= pourcentages[i];
                    pourcentages[i] = 0.0f;
                }
                ++i;
            }

        }
    }

}


