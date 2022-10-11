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
    public DiceType type_;
    public int min_point_ = 1;
    public int max_point_ = 6;
    public int point_ = -1;
}
