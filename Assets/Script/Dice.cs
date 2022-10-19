// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Dice : MonoBehaviour
{
    [SerializeField]
    Dice_SO dice_so_;
    [SerializeField]
    private Image selected_pannel_;
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
    private Dice_SO dice_info_;
    public void DiceInit() {
        dice_info_ = new Dice_SO(dice_so_);
    }
    private void Start() {
        DiceInit();
    }
    public bool SwitchSelected() {
        selected_ = !selected_;
        selected_pannel_.gameObject.SetActive(!selected_pannel_.gameObject.activeSelf);
        return selected_;
    }
    public int RollDice () {
        point_ = Random.Range(min_point_, max_point_);
        return point_;
    }
}