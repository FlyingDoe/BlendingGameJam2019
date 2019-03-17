using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningSlowly : MonoBehaviour
{
    [SerializeField] float rotation = 0.02f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation, rotation, rotation);
    }
}
