using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustacheSwirl : MonoBehaviour
{
    [SerializeField] RectTransform leftMustachePivot;
    [SerializeField] RectTransform rightMustachePivot;

    private void Update()
    {
        leftMustachePivot.localRotation = Quaternion.Euler(0, 0, Camera.main.transform.localPosition.x * 10f + 2.5f);
        rightMustachePivot.localRotation = Quaternion.Euler(0, 0, -Camera.main.transform.localPosition.x * 10f - 2.5f);
    }
}
