using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [SerializeField] private AudioClip sfx_explosion;
    [SerializeField] private AudioClip sfx_rocketLoop;
    [SerializeField] private AudioClip sfx_cork;
    [SerializeField] private AudioClip sfx_growlLoop;
    [SerializeField] private AudioClip sfx_eat;
    [SerializeField] private AudioClip[] sfx_squish;

    public AudioClip Sfx_rocketLoop
    {
        get
        {
            return sfx_rocketLoop;
        }

        set
        {
            sfx_rocketLoop = value;
        }
    }
    public AudioClip Sfx_explosion
    {
        get
        {
            return sfx_explosion;
        }

        set
        {
            sfx_explosion = value;
        }
    }
    public AudioClip Sfx_squish
    {
        get
        {
            return sfx_squish[Random.Range(0, sfx_squish.Length)];
        }
    }
    public AudioClip Sfx_cork
    {
        get
        {
            return sfx_cork;
        }
    }
    public AudioClip Sfx_growlLoop
    {
        get
        {
            return sfx_growlLoop;
        }
    }
    public AudioClip Sfx_eat
    {
        get
        {
            return sfx_eat;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
