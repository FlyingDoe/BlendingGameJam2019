using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAssembly : MonoBehaviour
{
    private PlayerBehavior player { get { return PlayerBehavior.Instance; } }

    public int oliveNeeded = 3;
    public int cheesNeeded = 1;
    public int mozzaNeeded = 1;
    public int anchoNeeded = 1;
    public int crustNeeded = 1;
    public int oilllNeeded = 1;

    private Renderer olivePlaceHolder;
    private Renderer cheesPlaceHolder;
    private Renderer mozzaPlaceHolder;
    private Renderer anchoPlaceHolder;
    private Renderer crustPlaceHolder;
    private Renderer oilllPlaceHolder;

    private void Awake()
    {
        Renderer[] rdArray = GetComponentsInChildren<Renderer>();
        olivePlaceHolder = rdArray[1];
        cheesPlaceHolder = rdArray[2];
        mozzaPlaceHolder = rdArray[3];
        anchoPlaceHolder = rdArray[4];
        crustPlaceHolder = rdArray[5];
        oilllPlaceHolder = rdArray[6];
    }

    public void PlaceIngredients()
    {
        if (oliveNeeded > 0 && player.oliveNbr > 0) { int tmp = oliveNeeded - player.oliveNbr; oliveNeeded = Mathf.Max(0, tmp); player.oliveNbr = Mathf.Max(0, -tmp); }
        if (cheesNeeded > 0 && player.cheesNbr > 0) { int tmp = cheesNeeded - player.cheesNbr; cheesNeeded = Mathf.Max(0, tmp); player.cheesNbr = Mathf.Max(0, -tmp); }
        if (mozzaNeeded > 0 && player.mozzaNbr > 0) { int tmp = mozzaNeeded - player.mozzaNbr; mozzaNeeded = Mathf.Max(0, tmp); player.mozzaNbr = Mathf.Max(0, -tmp); }
        if (anchoNeeded > 0 && player.anchoNbr > 0) { int tmp = anchoNeeded - player.anchoNbr; anchoNeeded = Mathf.Max(0, tmp); player.anchoNbr = Mathf.Max(0, -tmp); }
        if (crustNeeded > 0 && player.crustNbr > 0) { int tmp = crustNeeded - player.crustNbr; crustNeeded = Mathf.Max(0, tmp); player.crustNbr = Mathf.Max(0, -tmp); }
        if (oilllNeeded > 0 && player.oilllNbr > 0) { int tmp = oilllNeeded - player.oilllNbr; oilllNeeded = Mathf.Max(0, tmp); player.oilllNbr = Mathf.Max(0, -tmp); }

        if (oliveNeeded == 0) { olivePlaceHolder.material.color = Color.magenta; }
        if (cheesNeeded == 0) { cheesPlaceHolder.material.color = Color.magenta; }
        if (mozzaNeeded == 0) { mozzaPlaceHolder.material.color = Color.magenta; }
        if (anchoNeeded == 0) { anchoPlaceHolder.material.color = Color.magenta; }
        if (crustNeeded == 0) { crustPlaceHolder.material.color = Color.magenta; }
        if (oilllNeeded == 0) { oilllPlaceHolder.material.color = Color.magenta; }

    }
}
