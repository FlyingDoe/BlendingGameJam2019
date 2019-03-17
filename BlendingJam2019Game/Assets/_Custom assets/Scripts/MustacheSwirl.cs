using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustacheSwirl : MonoBehaviour
{
    [SerializeField] RectTransform leftMustachePivot;
    [SerializeField] RectTransform rightMustachePivot;

    [SerializeField] float multiplier=10f;
    [SerializeField] float offset=2.5f;

    private void Update()
    {
        leftMustachePivot.localRotation = Quaternion.Euler(0, 0, Camera.main.transform.localPosition.x * multiplier + offset);
        rightMustachePivot.localRotation = Quaternion.Euler(0, 0, -Camera.main.transform.localPosition.x * multiplier - offset);
    }
}
