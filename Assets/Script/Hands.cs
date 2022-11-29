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
    public int hands_limit_ = 10;
    public AudioSource select_audio;
    [SerializeField] GameObject mask_;
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
        mask_.SetActive(false);
        selected_dice_ = new List<Dice>();
        is_select_on_going = true;
    }

    public List<Dice> GetSelectedDice(){
        return selected_dice_;
    }

    public void OnRollButton()
    {

        Debug.Log("Dice or Die!");
        mask_.SetActive(true);
        for(int i = 0; i < dice_list_.Count; i++){
            if(dice_list_[i].selected_)
            {
                selected_dice_.Add(dice_list_[i]);
                // dice_list_[i].gameObject.SetActive(false);
                dice_list_.RemoveAt(i);
                i--;
            }
        }
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

    public bool Add(GameObject dice_gameobject)
    {
        if (dice_list_.Count >= hands_limit_)
        {
            Destroy(dice_gameobject);
            Debug.Log("Hands full.");
            return false;
        }//if

        dice_gameobject.transform.parent = itemsParent;
        dice_o_list_.Add(dice_gameobject);
        dice_list_.Add(dice_gameobject.GetComponent<Dice>());
        return true;
        // UpdateUI();
    }//Add

    public void Clear()
    {
        foreach(GameObject dice_gameobject in dice_o_list_){
            Destroy(dice_gameobject);
        }
        dice_list_.Clear();
        dice_o_list_.Clear();
        print(dice_list_.Count);
    }

    public void OnDiceSelect(Dice dice) {
        try{
            select_audio.Play();
        }catch{}
        dice.SwitchSelected();
    }
}
