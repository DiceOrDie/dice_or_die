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
    public bool is_select_on_going;

    // Image[] slots;
    List<GameObject> dice_o_list_;
    List<Dice> dice_list_;
    List<Dice> selected_dice_;

    void Start()
    {
        dice_list_  = new List<Dice>();
        dice_o_list_ = new List<GameObject>();
        // slots = itemsParent.GetComponentsInChildren<Image>();
        is_select_on_going = false;
        // UpdateUI();
    }//Start

    public void StartSelect()
    {
        selected_dice_ = new List<Dice>();
        is_select_on_going = true;
    }

    public List<Dice> GetSelectedDice(){
        return selected_dice_;
    }

    public void OnRollButton()
    {
        /* move selected dice from diceInHands to selectedDice */
        foreach(Dice dice in dice_list_) {
            Debug.Log(dice.gameObject.name);
            if(dice.selected_)
            {
                selected_dice_.Add(dice);
                dice.gameObject.SetActive(false);
            }
        }

        // UpdateUI();
        Debug.Log("Dice or Die!");
        is_select_on_going = false;
    }//OnRollButton

    // void UpdateUI()
    // {
    //     for (int i = 0; i < slots.Length; i++)
    //     {
    //         if (i < dice_list_.Count)
    //         {
    //             slots[i].enabled = true;
    //         }//if
    //         else
    //         {
    //             slots[i].enabled = false;
    //         }//else
    //     }//for i
    // }//UpdateUI

    public void Add(GameObject dice_gameobject)
    {
        if (dice_list_.Count >= 10)
        {
            Destroy(dice_gameobject);
            Debug.Log("Hands full.");
            return;
        }//if

        dice_o_list_.Add(dice_gameobject);
        dice_list_.Add(dice_gameobject.GetComponent<Dice>());
        // UpdateUI();
    }//Add

    public void OnDiceSelect(Dice dice) {
        dice.SwitchSelected();
    }
}
