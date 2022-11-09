using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Behavior List", menuName = "Entity Stats/Monster Behavior List")]
public class MonsterBehaviorList_SO : ScriptableObject
{
    [Header("Monster behaviors list")]
    
    public List<MonsterBehaviors_SO> behaviors_list;
}
