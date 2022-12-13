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
    public Text addition_text_;
    // Image[] slots;
    public List<GameObject> dice_o_list_;
    List<Dice> dice_list_;
    List<Dice> selected_dice_;
    string addition_base_ = "基礎傷害：{0}\n骰子結果：{1}~{2}\n\n回合結束後\n芝麻拳機率{3}%\n傷害{4}";


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
        addition_text_.text = string.Format(addition_base_, GameManager.instance.player.base_attack_, 0, 0, 0, 0);
    }

    public List<Dice> GetSelectedDice(){
        return selected_dice_;
    }

    public void OnRollButton()
    {

        Debug.Log("Dice or Die!");
        mask_.SetActive(true);
        // foreach(Dice dice in GameManager.instance.result_bar_.GetComponentsInChildren<Dice>()) {
        //     selected_dice_.Add(dice);
        //     dice_o_list_.Remove(dice.gameObject);
        //     dice_list_.Remove(dice);
        // }
        // for(int i = 0; i < dice_list_.Count; i++){
        //     if(dice_list_[i].selected_)
        //     {
        //         selected_dice_.Add(dice_list_[i]);
        //         // dice_list_[i].gameObject.SetActive(false);
        //         dice_list_.RemoveAt(i);
        //         i--;
        //     }
        // }
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
        select_audio.Play();
        selected_dice_.Add(dice);
        dice_o_list_.Remove(dice.gameObject);
        dice_list_.Remove(dice);
        UpdateAddition();
    }
    public void OnDiceDeselect(Dice dice) {
        select_audio.Play();
        selected_dice_.Remove(dice);
        dice_o_list_.Add(dice.gameObject);
        dice_list_.Add(dice);
        UpdateAddition();
    }
    void UpdateAddition() {
        int base_attack = GameManager.instance.player.base_attack_;
        int min_attack = 0;
        int max_attack = 0;
        string total_probobility_str;
        int total_probobility = 0;
        int skill_attack =  0;
        int odd_probobility = 10000;
        int even_probobility = 10000;

        foreach(Dice dice in selected_dice_) {
            switch(dice.type_){
                case DiceType.normal:
                    odd_probobility >>= 1;
                    even_probobility >>= 1;
                    break;
                case DiceType.odd:
                    even_probobility = 0;
                    break;
                case DiceType.even:
                    odd_probobility = 0;
                    break;
                case DiceType.cheat:
                    break;
            }
            min_attack += dice.min_point_;
            max_attack += dice.max_point_;
        }
        total_probobility = (odd_probobility + even_probobility > 10000) ? 10000 : odd_probobility + even_probobility;
        skill_attack = 1 << selected_dice_.Count;

        if(selected_dice_.Count < 2){
            total_probobility = 0;
            skill_attack = 0;
        }
        total_probobility_str = (total_probobility/100).ToString(); 
        if(total_probobility % 100 != 0) {
            total_probobility_str += '.' + (total_probobility%100).ToString();
        }
        addition_text_.text = string.Format(addition_base_, base_attack, min_attack, max_attack, total_probobility_str, skill_attack);
    }
}
