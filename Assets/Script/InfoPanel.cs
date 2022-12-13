using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    #region Singleton

    public static InfoPanel instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one InfoPanel!!!");
            return;
        }//if

        instance = this;
    }//Awake

    #endregion

    public GameObject dice_area_;
    public GameObject entity_area_;
    
    private Image dice_image_;
    private Text dice_name_;
    private Text dice_level_;
    private Text dice_face_;
    private Text dice_description_;

    private Image entity_image_;
    private Text entity_name_;
    private Text[] entity_states_;
    private Text entity_currenthp_;
    private Text entity_maxhp_;
    private Text entity_base_attack_;
    private Text entity_description_;

    void Start()
    {
        dice_image_ = GameObject.Find("InfoPanel/Dice/Title/Icon").GetComponent<Image>();
        dice_name_ = GameObject.Find("InfoPanel/Dice/Title/Name").GetComponent<Text>();
        dice_level_ = GameObject.Find("InfoPanel/Dice/Info/Level/Value").GetComponent<Text>();
        dice_face_ = GameObject.Find("InfoPanel/Dice/Info/Face/Value").GetComponent<Text>();
        dice_description_ = GameObject.Find("InfoPanel/Dice/Info/Description/Content").GetComponent<Text>();

        entity_image_ = GameObject.Find("InfoPanel/Entity/Title/Icon").GetComponent<Image>();
        entity_name_ = GameObject.Find("InfoPanel/Entity/Title/Name").GetComponent<Text>();
        entity_states_ = GameObject.Find("InfoPanel/Entity/Info/State").GetComponentsInChildren<Text>();
        entity_currenthp_ = GameObject.Find("InfoPanel/Entity/Info/HP/Current HP").GetComponent<Text>();
        entity_maxhp_ = GameObject.Find("InfoPanel/Entity/Info/HP/Max HP").GetComponent<Text>();
        entity_base_attack_ = GameObject.Find("InfoPanel/Entity/Info/Attack/Value").GetComponent<Text>();
        entity_description_ = GameObject.Find("InfoPanel/Entity/Info/Description/Content").GetComponent<Text>();
        dice_area_.SetActive(false);
        entity_area_.SetActive(false);
    }//Start

    public void UpdateDiceInfo(Dice dice)
    {
        dice_image_.sprite = dice.sprite_;
        dice_name_.text = dice.name_;
        dice_level_.text = dice.level_.ToString();
        int[] face = new int[6];
        for(int i = 0; i < 6; i++) {
            switch(dice.type_) {
                case DiceType.even:
                    face[i] = ((i+1) % 2 == 0) ? i+1 : i+2;
                    break;
                case DiceType.odd:
                    face[i] = ((i+1) % 2 == 0) ? i+2 : i+1;
                    break;
                case DiceType.cheat:
                    face[i] = dice.min_point_;
                    break;
                case DiceType.normal:
                    face[i] = i + dice.min_point_;
                    break;
        }
        }
        dice_face_.text = string.Format("{0} {1} {2} {3} {4} {5}", face[0], face[1], face[2], face[3], face[4], face[5]);
        dice_description_.text = dice.description_;
        entity_area_.SetActive(false);
        dice_area_.SetActive(true);
    }//UpdateDiceInfo

    public void UpdateEntityInfo(Entity entity)
    {
        entity_image_.sprite = entity.entity_info.sprite_;
        entity_name_.text = entity.entity_info.name_;
        entity_currenthp_.text = entity.current_HP_.ToString();
        entity_maxhp_.text = entity.max_HP_.ToString();
        entity_base_attack_.text = entity.base_attack_.ToString();
        entity_description_.text = entity.description_;
        dice_area_.SetActive(false);
        entity_area_.SetActive(true);
    }//UpdateEntityInfo
}
