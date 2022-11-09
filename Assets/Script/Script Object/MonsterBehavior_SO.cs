using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Entity Stats/Monster Behavior")]
public class MonsterBehavior_SO : ScriptableObject
{
    [Header("Monster behavior")]
    public string behavior_name;
    public int damage;
    public int cold;
    public int frozen;
    public int burn;
    public int heal;
    
    public int fragile;
    
    public int paralysis;
}
