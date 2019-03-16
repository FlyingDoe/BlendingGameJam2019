﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSliceBehaviour : MonoBehaviour
{

    // morceaux faisant parti de la slice
    public List<IngredientSpawnerBehaviour> pizzaPartsSpawner = new List<IngredientSpawnerBehaviour>();

    // ingredients a spwaner : prefab a glisser dans le spawner
    public List<GameObject> ingredientsType = new List<GameObject>();

    // pourcentage de presence de chaque ingredient
    [Range(0.0f, 1.0f)]
    public List<float> pourcentages = new List<float>();

    // Use this for initialization
    void Start()
    {
        // remplissage des pourcentages manquant a 0.0f
        if (pourcentages.Count < ingredientsType.Count)
        {
            int initialCount = pourcentages.Count;
            for (int i = initialCount; i < ingredientsType.Count; ++i)
                pourcentages.Add(0.0f);
        }
        else if (pourcentages.Count > ingredientsType.Count)
        {
            // suppression des pourcentages supplementaires
            int initialCount = pourcentages.Count;
            for (int i = initialCount - 1; i >= ingredientsType.Count; ++i)
                pourcentages.RemoveAt(i);
        }

        // envoyer les pourcentages vers les spawners de chaque pizza
        for(int i = 0; i < pizzaPartsSpawner.Count; ++i)
        {
            pizzaPartsSpawner[i].tag = tag;
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


