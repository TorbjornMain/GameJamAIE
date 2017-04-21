using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightBar : MonoBehaviour
{
    [SerializeField]
    private float weightBarFillAmount;

    [SerializeField]
    private Image content;

    void Update()
    {
        WeightBarFill();
        CalcWeightBar();
    }

    private void WeightBarFill()
    {
        content.fillAmount = weightBarFillAmount;
    }

    private void CalcWeightBar()
    {
        GameObject thePlayer = GameObject.FindWithTag("Player");
        PlayerInventory playerInventory = thePlayer.GetComponent<PlayerInventory>();
        weightBarFillAmount = playerInventory.curWeight /playerInventory.maxWeight;
    }
}
