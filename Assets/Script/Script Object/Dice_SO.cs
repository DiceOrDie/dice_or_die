using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    normal,
    fire,
    water,
    grass
}

[CreateAssetMenu(fileName = "Dice Data", menuName = "Dice/Dice Data")]
public class Dice_SO : ScriptableObject
{
    [Header("Dice Info")]
    public Sprite sprite_;
    public DiceType type_;
    public string name_;
    public int min_point_ = 1;
    public int max_point_ = 6;
    public int point_ = -1;

    public Dice_SO (Dice_SO dice_so){
        sprite_ = dice_so.sprite_;
        type_ = dice_so.type_;
        name_ = dice_so.name_;
        min_point_ = dice_so.min_point_;
        max_point_ = dice_so.max_point_;
        point_ = dice_so.point_;
    }
}
