using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField]
    private Dice_SO dice_so_;
    [SerializeField]
    private Image selected_pannel_;
    private InfoPanel info_panel_ = InfoPanel.instance;
    public bool selected_ = false;
    public int level_ = 1;
    
    #region Read for dice_so_
    public DiceType type_
    {
        get { if(dice_info_) return dice_info_.type_; else return 0; }
        set { dice_info_.type_ = value; }
    }
    public int min_point_
    {
        get { if(dice_info_) return dice_info_.min_point_+level_-1; else return 0; }
        set { dice_info_.min_point_ = value; }
    }
    public int max_point_
    {
        get { if(dice_info_) return dice_info_.max_point_+level_-1; else return 0; }
        set { dice_info_.max_point_ = value; }
    }
    public int point_
    {
        get { if(dice_info_) return dice_info_.point_; else return 0; }
        set { dice_info_.point_ = value; }
    }
    public Sprite sprite_
    {
        get { if(dice_info_) return dice_info_.sprite_; else return null; }
        set { dice_info_.sprite_ = value; }
    }
    public string name_
    {
        get { if(dice_info_) return dice_info_.name_; else return null; }
        set { dice_info_.name_ = value; }
    }
    public string description_
    {
        get { if(dice_info_) return dice_info_.description_; else return null; }
    }
    #endregion
    private Dice_SO dice_info_;
    public void DiceInit() {
        dice_info_ = Instantiate(dice_so_);
    }
    private void Awake() {
        DiceInit();
    }
    public void SwitchSelected() {
        info_panel_.UpdateDiceInfo(this);
        selected_ = !selected_;
        if(selected_) {
            transform.SetParent(GameManager.instance.result_bar_.GetComponentInChildren<GridLayoutGroup>().transform);
            GameManager.instance.hands_go_.GetComponent<Hands>().OnDiceSelect(this);
            // transform.parent = GameManager.instance.result_bar_.GetComponentInChildren<GridLayoutGroup>().transform;
        }
        else {
            transform.SetParent(GameManager.instance.hands_go_.GetComponentInChildren<GridLayoutGroup>().transform);
            GameManager.instance.hands_go_.GetComponent<Hands>().OnDiceDeselect(this);
            // transform.parent = GameManager.instance.hands_go_.GetComponentInChildren<GridLayoutGroup>().transform;
        }
        // selected_pannel_.gameObject.SetActive(!selected_pannel_.gameObject.activeSelf);
        // return selected_;
    }
    public int RollDice () {
        int tmp_min = min_point_;
        int tmp_max = max_point_;
        switch(type_) {
            case DiceType.even:
                tmp_min -= 1;
                break;
            case DiceType.odd:
                tmp_max -= 1;
                break;
        }
        point_ = Random.Range(tmp_min, tmp_max+1);
        switch(type_) {
            case DiceType.even:
                point_ = (point_ % 2 == 0) ? point_ : point_ + 1;
                break;
            case DiceType.odd:
                point_ = (point_ % 2 == 0) ? point_ + 1 : point_;
                break;
            default:
                break;
        }

        return point_;
    }
}