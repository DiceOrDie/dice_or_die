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

    public int min_point_bonus=0;
    public int max_point_bonus=0;
    
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
    #endregion
    private Dice_SO dice_info_;
    public void DiceInit() {
        dice_info_ = Instantiate(dice_so_);
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
        point_ = Random.Range(min_point_+min_point_bonus, max_point_+max_point_bonus+1);
        switch(type_) {
            case DiceType.even:
                point_ = (point_ % 2 == 0) ? point_: point_ + 1;
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