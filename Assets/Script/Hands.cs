using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    #region Singleton
    public static Hands instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Hands!!!");
            return;
        }//if

        instance = this;
    }//Awake
    #endregion

    public Button rollButton;
    public Transform itemsParent;

    DiceSlot[] slots;
    List<Dice> diceInHands = new List<Dice>();

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<DiceSlot>();
        UpdateUI();
    }//Start

    public void OnRollButton()
    {
        diceInHands.RemoveAt(0);
        UpdateUI();
        Debug.Log("Dice or Die!");
    }//OnRollButton

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < diceInHands.Count)
            {
                slots[i].SetVisible();
            }//if
            else
            {
                slots[i].SetInvisible();
            }//else
        }//for i
    }//UpdateUI

    public void Add(Dice dice)
    {
        if (diceInHands.Count >= 10)
        {
            Debug.Log("Hands full.");
            return;
        }//if

        diceInHands.Add(dice);
        UpdateUI();
    }//Add
}
