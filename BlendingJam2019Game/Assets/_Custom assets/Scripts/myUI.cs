using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myUI : MonoBehaviour
{
    public static myUI Instance;

    [SerializeField]
    Slider weightSlider;

    [SerializeField] Text oliveNbr;
    [SerializeField] Text mozzaNbr;
    [SerializeField] Text pepniNbr;
    [SerializeField] Text oilllNbr;
    [SerializeField] Text pepprNbr;

    private void Awake()
    {
        weightSlider.minValue = 0;
        weightSlider.maxValue = PlayerBehavior.maxWeight;

        UpdateValues();
    }

    public void UpdateValues()
    {
        weightSlider.value = PlayerBehavior.Instance.CurrentWeight;

        oliveNbr.text = PlayerBehavior.Instance.oliveNbr.ToString();
        mozzaNbr.text = PlayerBehavior.Instance.mozzaNbr.ToString();
        pepniNbr.text = PlayerBehavior.Instance.pepniNbr.ToString();
        oilllNbr.text = PlayerBehavior.Instance.oilllNbr.ToString();
        pepprNbr.text = PlayerBehavior.Instance.pepprNbr.ToString();
    }
}
