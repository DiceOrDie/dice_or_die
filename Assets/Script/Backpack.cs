using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    #region Singleton

    public static Backpack instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Backpack!!!");
            return;
        }//if

        instance = this;
    }//Awake

    #endregion

    public GameObject draw_button_ob_;
    private Button draw_button_;
    private Image draw_button_image_;
    public Transform items_parent_;
    public Transform hands_parent_;
    // public Dice[] dice_initial_ = { new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal),
    //                               new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire),
    //                               new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water),
    //                               new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass) };
    public List<GameObject> dice_initial_;
    private List<GameObject> own_dice_gameobject_;
    // private List<Dice> diceAvailable = new List<Dice>();
    DiceSlot[] slots_;

    public bool is_draw_on_going_ = false;

    public GameObject backpack_gameobject_;
    public int draw_dice_count_ = 2;

    void Start()
    {
        own_dice_gameobject_ = new List<GameObject>();
        slots_ = items_parent_.GetComponentsInChildren<DiceSlot>();
        draw_button_ = draw_button_ob_.GetComponent<Button>();
        draw_button_image_ = draw_button_ob_.GetComponent<Image>();
        Refill();
    }//Start

    public void StartDraw()
    {
        is_draw_on_going_ = true;
        // drawButtonImage.enabled = true;
        backpack_gameobject_.SetActive(true);
    }//StartDraw

    void UpdateUI()
    {
        // for (int i = 0; i < slots.Length; i++)
        // {
        //     if (i < diceAvailable.Count)
        //     {
        //         slots[i].SetVisible();
        //     }//if
        //     else
        //     {
        //         slots[i].SetInvisible();
        //     }//else
        // }//for i
    }//UpdateUI
    
    void Refill()
    {
        if (own_dice_gameobject_.Count <= 0)
        {
            Debug.Log("Backpack empty. Refilling dice.");
            int i = 0;
            foreach (GameObject dice_o in dice_initial_)
            {
                //Debug.Log(dice);
                GameObject o = Instantiate(dice_o, items_parent_);
                o.name = "Dice" + i.ToString();
                own_dice_gameobject_.Add(o);
                i++;
            }//foreach dice
        }//if
        else
        {
            Debug.Log("There are still some dice in the backpack. Not refilling.");
        }//else
        
        //Debug.Log("Dice initial: " + diceAvailable.Count);
        //Debug.Log("Dice initial: " + DiceType.fire);
        //Debug.Log("Dice available: " + diceAvailable.Count);

        return;
    }//Refill
    public GameObject PickDice() {
        if (own_dice_gameobject_.Count == 0) {
            Refill();
        }
        int rand = Random.Range(0, own_dice_gameobject_.Count);
        return own_dice_gameobject_[rand];
    }
    public void OnDrawButton()
    {
        Refill();
        // Dice diceDrawn1, diceDrawn2;

        Debug.Log("Draw!\n");

        for (int i = 0; i < draw_dice_count_; i++){
            GameObject dice = PickDice();
            own_dice_gameobject_.Remove(dice);
            Hands.instance.Add(dice);
        }
        
        // rand = Random.Range(0, diceAvailable.Count);
        // diceDrawn1 = diceAvailable[rand];
        // diceAvailable.RemoveAt(rand);
        // Debug.Log(rand);

        // rand = Random.Range(0, diceAvailable.Count);
        // diceDrawn2 = diceAvailable[rand];
        // diceAvailable.RemoveAt(rand);
        // Debug.Log(rand);

        // Hands.instance.Add(diceDrawn1);
        // Hands.instance.Add(diceDrawn2);

        // Debug.Log("Remaining dice count: " + diceAvailable.Count);

        // drawButtonImage.enabled = false;
        backpack_gameobject_.SetActive(false);
        
        // UpdateUI();

        is_draw_on_going_ = false;
    }//OnDrawButton

}
