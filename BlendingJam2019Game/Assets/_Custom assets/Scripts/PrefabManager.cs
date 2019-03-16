using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {

    public static PrefabManager Instance;

    public Collectibles[] collectibles;

    private void Awake()
    {
        Instance = this;
    }
}
