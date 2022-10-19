using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Entity Stats/Data")]
public class EntityData_SO : ScriptableObject
{
    
    [Header("Entity Info")]
    public int max_HP_;
    public int current_HP_;
    public int base_attack_;

    public EntityData_SO(EntityData_SO data) {
        max_HP_ = data.max_HP_;
        current_HP_ = data.current_HP_;
        base_attack_ = data.base_attack_;
    }
}