using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource aS { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        aS = GetComponent<AudioSource>();
        aS.loop = true;
        aS.playOnAwake = true;
    }

    public void StopMenuAs()
    {
        aS.Stop();
    }

}
