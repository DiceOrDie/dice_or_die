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
<<<<<<< Updated upstream
        diceInHands.RemoveAt(0);
        UpdateUI();
=======
        /* move selected dice from diceInHands to selectedDice */
        for(int i = 0; i < dice_list_.Count; i++){
            if(dice_list_[i].selected_)
            {
                selected_dice_.Add(dice_list_[i]);
                // dice_list_[i].gameObject.SetActive(false);
                dice_list_.RemoveAt(i);
                i--;
            }
        }

        // UpdateUI();
>>>>>>> Stashed changes
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
