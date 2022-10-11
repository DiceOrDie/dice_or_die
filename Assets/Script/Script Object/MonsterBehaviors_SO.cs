using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Behaviors", menuName = "Entity Stats/Monster Behaviors")]
public class MonsterBehaviors_SO : ScriptableObject
{
    [Header("Monster behaviors")]
    public List<MonsterBehavior_SO> behaviors;
    public void AddBehavior(MonsterBehavior_SO behavior) {
        behaviors.Add(behavior);
    }
}
