﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAssembly : MonoBehaviour
{
    private PlayerBehavior player { get { return PlayerBehavior.Instance; } }

    private int OliveNeeded = 0;
    private int CheesNeeded = 0;
    private int MozzaNeeded = 0;
    private int AnchoNeeded = 0;
    private int OilllNeeded = 0;

    [SerializeField] private Renderer[] olivePlaceholders;
    [SerializeField] private Renderer[] cheesPlaceholders;
    [SerializeField] private Renderer[] mozzaPlaceholders;
    [SerializeField] private Renderer[] anchoPlaceholders;
    [SerializeField] private Renderer[] oilllPlaceholders;

    [SerializeField] private Material trnspMat;
    [SerializeField] private Material oliveMat;
    [SerializeField] private Material cheesMat;
    [SerializeField] private Material mozzaMat;
    [SerializeField] private Material anchoMat;
    [SerializeField] private Material oilllMat;

    private void Awake()
    {
        foreach (Renderer rd in GetComponentsInChildren<Renderer>())
        {
            rd.material = trnspMat;
        }

        OliveNeeded = olivePlaceholders.Length;
        CheesNeeded = cheesPlaceholders.Length;
        MozzaNeeded = mozzaPlaceholders.Length;
        AnchoNeeded = anchoPlaceholders.Length;
        OilllNeeded = oilllPlaceholders.Length;

    }

    public void PlaceIngredients()
    {
        PlaceOneIngred(ref OliveNeeded, ref player.oliveNbr, ref olivePlaceholders, ref oliveMat);
        PlaceOneIngred(ref CheesNeeded, ref player.cheesNbr, ref cheesPlaceholders, ref cheesMat);
        PlaceOneIngred(ref MozzaNeeded, ref player.mozzaNbr, ref mozzaPlaceholders, ref mozzaMat);
        PlaceOneIngred(ref AnchoNeeded, ref player.anchoNbr, ref anchoPlaceholders, ref anchoMat);
        PlaceOneIngred(ref OilllNeeded, ref player.oilllNbr, ref oilllPlaceholders, ref oilllMat);

        CheckWin();
    }

    private void CheckWin()
    {
        if (OliveNeeded == 0 &&
             CheesNeeded == 0 &&
             MozzaNeeded == 0 &&
             AnchoNeeded == 0 &&
             OilllNeeded == 0)
        {
            Debug.LogError("WINGAME NOT IMPLEMENTED");
        }
    }

    private void PlaceOneIngred(ref int nbrNeeded, ref int playerNbr, ref Renderer[] rdTab, ref Material newMat)
    {
        int need = nbrNeeded;
        for (int i = rdTab.Length - need; i < rdTab.Length; i++)
        {
            if (playerNbr > 0 && nbrNeeded > 0)
            {
                playerNbr--;
                nbrNeeded--;
                rdTab[i].material = newMat;
            }
        }
    }
}
