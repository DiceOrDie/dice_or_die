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

    private Image entity_image_;
    private Text entity_name_;

    void Start()
    {
        dice_image_ = GameObject.Find("InfoPanel/Dice/Title/Icon").GetComponent<Image>();
        dice_name_ = GameObject.Find("InfoPanel/Dice/Title/Name").GetComponent<Text>();
        entity_image_ = GameObject.Find("InfoPanel/Entity/Title/Icon").GetComponent<Image>();
        entity_name_ = GameObject.Find("InfoPanel/Entity/Title/Name").GetComponent<Text>();
        dice_area_.SetActive(false);
        entity_area_.SetActive(false);
    }//Start

    public void UpdateDiceInfo(Dice dice)
    {
        dice_image_.sprite = dice.sprite_;
        dice_name_.text = dice.name_;
        entity_area_.SetActive(false);
        dice_area_.SetActive(true);
    }//UpdateDiceInfo

    public void UpdateEntityInfo(Entity entity)
    {
        entity_image_.sprite = entity.entity_info.sprite_;
        entity_name_.text = entity.entity_info.name_;
        dice_area_.SetActive(false);
        entity_area_.SetActive(true);
    }//UpdateEntityInfo
}
