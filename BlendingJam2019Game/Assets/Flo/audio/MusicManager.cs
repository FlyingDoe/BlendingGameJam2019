using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    //public static MusicManager Instance;
    AudioSource aS;

    private void Awake()
    {
        //if (Instance)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}

        aS = GetComponent<AudioSource>();
        aS.loop = true;
        aS.playOnAwake = true;
    }
}
