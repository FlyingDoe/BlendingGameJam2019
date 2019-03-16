using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerBehaviour : MonoBehaviour
{
    // parent contenant les SpawnPosition
    private GameObject pizzaPart;

    // ingredients a spwaner : prefab a glisser dans le spawner
    private List<GameObject> ingredientsType = new List<GameObject>();
    
    // pourcentage de presence de chaque ingredient
    //[Range(0.0f, 1.0f)]
    private List<float> pourcentages = new List<float>();
    

    // points de spawn
    private List<GameObject> spawnPoints = new List<GameObject>();

    // nombre d'ingredient de chaque type
    [SerializeField]
    private List<int> numIngredients;

    // list d'ingredients spawnes
    [SerializeField]
    private List<GameObject> ingredientSpawned = new List<GameObject>();


    // Use this for initialization
    void Start()
    {
        //// remplissage des pourcentages manquant a 0.0f
        //if(pourcentages.Count < ingredientsType.Count)
        //{
        //    int initialCount = pourcentages.Count;
        //    for (int i = initialCount; i < ingredientsType.Count; ++i)
        //        pourcentages.Add(0.0f);
        //}
        //else if(pourcentages.Count > ingredientsType.Count)
        //{
        //    // suppression des pourcentages supplementaires
        //    int initialCount = pourcentages.Count;
        //    for (int i = initialCount - 1; i >= ingredientsType.Count; ++i)
        //        pourcentages.RemoveAt(i);
        //}

        //// recuperation des positions de spawn dans la part de pizza
        //Transform[] tr = pizzaPart.GetComponentsInChildren<Transform>();
        //print("numChildren = " + tr.Length);
        //for(int i = 1; i < tr.Length; ++i) // commence a 1 car 0 = objet lui-mm
        //{
        //    if (tr[i].gameObject.tag == "SpawnPosition")
        //        spawnPoints.Add(tr[i].gameObject);
        //}
        //print("num spawnPoints = " + spawnPoints.Count);

        //// calcul du nombre d'ingredient a spawnes de chaque type
        //numIngredients = new List<int>();
        //int sumIngredient = 0;
        //for(int i = 0; i < ingredientsType.Count; ++i)
        //{
        //    numIngredients.Add((int)(spawnPoints.Count * pourcentages[i]));
        //    sumIngredient += numIngredients[i];
        //    print("ingredients " + i + " : " + numIngredients[i]);
        //}
        //// check somme du nombre total d'ingredient
        //while(sumIngredient < spawnPoints.Count)
        //{
        //    for(int i = 0; i < ingredientsType.Count; ++i)
        //    {
        //        if(numIngredients[i] < spawnPoints.Count * pourcentages[i])
        //        {
        //            numIngredients[i] += 1;
        //            sumIngredient += 1;
        //            if (sumIngredient == spawnPoints.Count)
        //                break;
        //        }
        //    }
        //}

        //// instantiation des ingredients
        //for(int i = 0; i < ingredientsType.Count; ++i)
        //{
        //    for (int j = 0; j < numIngredients[i]; ++j)
        //    {
        //        GameObject currentObject = Instantiate(ingredientsType[i], transform);
        //        ingredientSpawned.Add(currentObject);
        //    }
        //}



        //List<GameObject> spawnCopy = spawnPoints;

        //// attribution des positions
        //for(int i = 0; i < ingredientSpawned.Count; ++i)
        //{
        //    int idx = Random.Range(0, spawnCopy.Count - 1);

        //    ingredientSpawned[i].transform.position = spawnCopy[idx].transform.position;
        //    ingredientSpawned[i].transform.parent = pizzaPart.transform;
        //    spawnCopy.RemoveAt(idx);
        //}
        pizzaPart = transform.gameObject;
        //Mesh pizzaPartMesh = pizzaPart.GetComponent<MeshFilter>().mesh;


        
    }

    public void setIngredients(List<float> _percentages, List<GameObject> _ingredientTypes)
    {
        // remplissage des pourcentages manquant a 0.0f
        pourcentages = _percentages;
        ingredientsType = _ingredientTypes;

        // recuperation des positions de spawn dans la part de pizza
        GetSpawnPositions();

        // calcul du nombre d'ingredient a spawnes de chaque type
        IngredientsInventory();

        // instantiation des ingredients
        for (int i = 0; i < ingredientsType.Count; ++i)
        {
            print("instantiate " + numIngredients[i] + " x " + i);
            for (int j = 0; j < numIngredients[i]; ++j)
            {
                GameObject currentObject = Instantiate(ingredientsType[i], transform);
                ingredientSpawned.Add(currentObject);
            }
        }



        List<GameObject> spawnCopy = spawnPoints;

        // attribution des positions
        for (int i = 0; i < ingredientSpawned.Count; ++i)
        {
            int idx = Random.Range(0, spawnCopy.Count - 1);

            ingredientSpawned[i].transform.position = spawnCopy[idx].transform.position;
            ingredientSpawned[i].transform.parent = pizzaPart.transform;
            ingredientSpawned[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //print( i + " : " + ingredientSpawned[i].transform.localScale.x + " " + ingredientSpawned[i].transform.localScale.y + " " + ingredientSpawned[i].transform.localScale.z);
            spawnCopy.RemoveAt(idx);
        }

    }

    private void IngredientsInventory()
    {
        numIngredients = new List<int>();
        int sumIngredient = 0;
        for (int i = 0; i < ingredientsType.Count; ++i)
        {
            numIngredients.Add((int)(spawnPoints.Count * pourcentages[i]));
            sumIngredient += numIngredients[i];
            print("ingredients " + i + " : " + numIngredients[i]);
        }
        // check somme du nombre total d'ingredient
        while (sumIngredient < spawnPoints.Count)
        {
            for (int i = 0; i < ingredientsType.Count; ++i)
            {
                if (numIngredients[i] < spawnPoints.Count * pourcentages[i])
                {
                    numIngredients[i] += 1;
                    sumIngredient += 1;
                    if (sumIngredient == spawnPoints.Count)
                        break;
                }
            }
        }
    }

    private void GetSpawnPositions()
    {
        Transform[] tr = pizzaPart.GetComponentsInChildren<Transform>();
        //print("numChildren = " + tr.Length);
        for (int i = 1; i < tr.Length; ++i) // commence a 1 car 0 = objet lui-mm
        {
            if (tr[i].gameObject.tag == "SpawnPosition")
                spawnPoints.Add(tr[i].gameObject);
        }
        print("num spawnPoints = " + spawnPoints.Count);
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
            while(diff > 0)
            {
                if (pourcentages[i] >= diff)
                {
                    pourcentages[i] -= diff;
                    diff = 0;
                }
                else if(pourcentages[i] > 0.0f)
                {
                    diff -= pourcentages[i];
                    pourcentages[i] = 0.0f;
                }
                ++i;
            }
            
        }
    }

}
