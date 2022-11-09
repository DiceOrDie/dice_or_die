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

public struct Dice
{
    public Dice(DiceType _t) { type = _t; }

    public DiceType type;
}
