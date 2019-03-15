using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField]
    Slider weightSlider;

    [SerializeField] Text oliveNbr;
    [SerializeField] Text cheesNbr;
    [SerializeField] Text mozzaNbr;
    [SerializeField] Text anchoNbr;
    [SerializeField] Text crustNbr;
    [SerializeField] Text oilllNbr;

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
        cheesNbr.text = PlayerBehavior.Instance.cheesNbr.ToString();
        mozzaNbr.text = PlayerBehavior.Instance.mozzaNbr.ToString();
        anchoNbr.text = PlayerBehavior.Instance.anchoNbr.ToString();
        crustNbr.text = PlayerBehavior.Instance.crustNbr.ToString();
        oilllNbr.text = PlayerBehavior.Instance.oilllNbr.ToString();
    }
}
