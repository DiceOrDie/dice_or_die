//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackScript : MonoBehaviour
{
    public Button drawButton;
    public Transform itemsParent;

    int diceCount = 20;
    DiceSlot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<DiceSlot>();
    }//Start

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < diceCount)
            {
                slots[i].SetVisible();
            }//if
            else
            {
                slots[i].SetInvisible();
            }//else
        }//for i
    }//UpdateUI

    public void OnDrawButton()
    {
        Debug.Log("Draw!\n");
        diceCount -= 2;

        if (diceCount <= 0)
        {
            Debug.Log("Backpack empty. Refilling dice.");
            diceCount = 20;
        }//if

        UpdateUI();
    }//OnDrawButton
}
