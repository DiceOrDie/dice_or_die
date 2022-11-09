using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField]
    Dice_SO dice_so_;
    [SerializeField]
    private Image selected_pannel_;
    private InfoPanel info_panel_ = InfoPanel.instance;
    public bool selected_ = false;
    
    #region Read for dice_so_
    public DiceType type_
    {
        get { if(dice_info_) return dice_info_.type_; else return 0; }
        set { dice_info_.type_ = value; }
    }
    public int min_point_
    {
        get { if(dice_info_) return dice_info_.min_point_; else return 0; }
        set { dice_info_.min_point_ = value; }
    }
    public int max_point_
    {
        get { if(dice_info_) return dice_info_.max_point_; else return 0; }
        set { dice_info_.max_point_ = value; }
    }
    public int point_
    {
        get { if(dice_info_) return dice_info_.point_; else return 0; }
        set { dice_info_.point_ = value; }
    }
    #endregion
    public Dice_SO dice_info_;
    public void DiceInit() {
        dice_info_ = new Dice_SO(dice_so_);
    }
    private void Awake() {
        DiceInit();
    }
    public bool SwitchSelected() {
        info_panel_.UpdateDiceInfo(this);
        selected_ = !selected_;
        if(selected_) {
            transform.SetParent(GameManager.instance.result_bar_.GetComponentInChildren<GridLayoutGroup>().transform);
            // transform.parent = GameManager.instance.result_bar_.GetComponentInChildren<GridLayoutGroup>().transform;
        }
        else {
            transform.SetParent(GameManager.instance.hands_go_.GetComponentInChildren<GridLayoutGroup>().transform);
            // transform.parent = GameManager.instance.hands_go_.GetComponentInChildren<GridLayoutGroup>().transform;
        }
        // selected_pannel_.gameObject.SetActive(!selected_pannel_.gameObject.activeSelf);
        return selected_;
    }
    public int RollDice () {
        point_ = Random.Range(min_point_, max_point_);
        gameObject.GetComponent<Image>().enabled = false;

        GameObject roll_result_text = Instantiate(new GameObject(gameObject.name + "_result"), transform);
       
        roll_result_text.AddComponent<Text>();
        roll_result_text.GetComponent<Text>().text = point_.ToString();
        roll_result_text.transform.parent = gameObject.transform;
        roll_result_text.transform.position = gameObject.transform.position;
        roll_result_text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        roll_result_text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        roll_result_text.GetComponent<Text>().fontSize = 60;
        return point_;
    }
}