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
    private Button drawButton;
    private Image drawButtonImage;
    public Transform itemsParent;
    public Dice[] diceInitial = { new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal), new Dice(DiceType.normal),
                                  new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire), new Dice(DiceType.fire),
                                  new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water), new Dice(DiceType.water),
                                  new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass), new Dice(DiceType.grass) };

    private List<Dice> diceAvailable = new List<Dice>();
    DiceSlot[] slots;

    public bool drawOnGoing = false;

    public GameObject backpack_go_;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<DiceSlot>();
        drawButton = draw_button_ob_.GetComponent<Button>();
        drawButtonImage = draw_button_ob_.GetComponent<Image>();
        Refill();
    }//Start

    public void startDraw()
    {
        drawOnGoing = true;
        // drawButtonImage.enabled = true;
        backpack_go_.SetActive(true);
    }//startDraw

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < diceAvailable.Count)
            {
                slots[i].SetVisible();
            }//if
            else
            {
                slots[i].SetInvisible();
            }//else
        }//for i
    }//UpdateUI
    
    void Refill()
    {
        if (diceAvailable.Count <= 0)
        {
            Debug.Log("Backpack empty. Refilling dice.");
            foreach (Dice dice in diceInitial)
            {
                //Debug.Log(dice);
                diceAvailable.Add(dice);
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

    public void OnDrawButton()
    {
        Refill();

        int rand;
        Dice diceDrawn1, diceDrawn2;

        Debug.Log("Draw!\n");

        rand = Random.Range(0, diceAvailable.Count);
        diceDrawn1 = diceAvailable[rand];
        diceAvailable.RemoveAt(rand);
        Debug.Log(rand);

        rand = Random.Range(0, diceAvailable.Count);
        diceDrawn2 = diceAvailable[rand];
        diceAvailable.RemoveAt(rand);
        Debug.Log(rand);

        Hands.instance.Add(diceDrawn1);
        Hands.instance.Add(diceDrawn2);

        Debug.Log("Remaining dice count: " + diceAvailable.Count);

        // drawButtonImage.enabled = false;
        backpack_go_.SetActive(false);
        
        UpdateUI();

        drawOnGoing = false;
    }//OnDrawButton
}
