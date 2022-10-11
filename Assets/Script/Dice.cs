// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Dice : MonoBehaviour
{
    public Dice_SO dice_so_;
    #region Read for dice_so_
    public DiceType type_
    {
        get { if(dice_so_) return dice_so_.type_; else return 0; }
        set { dice_so_.type_ = value; }
    }
    public int min_point_
    {
        get { if(dice_so_) return dice_so_.min_point_; else return 0; }
        set { dice_so_.min_point_ = value; }
    }
    public int max_point_
    {
        get { if(dice_so_) return dice_so_.max_point_; else return 0; }
        set { dice_so_.max_point_ = value; }
    }
    public int point_
    {
        get { if(dice_so_) return dice_so_.point_; else return 0; }
        set { dice_so_.point_ = value; }
    }
    #endregion
    [SerializeField]
    private Image selected_pannel_;
    public bool selected_ = false;
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