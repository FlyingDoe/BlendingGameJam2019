using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public static TextureManager Instance;

    public Texture OliveTex;

    private void Awake()
    {
        Instance = this;
    }
}
