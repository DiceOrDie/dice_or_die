// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    normal,
    fire,
    water,
    grass
}

public class Dice
{
    public Dice(DiceType _t) { type = _t; }

    public DiceType type;

    public int point_;
}
