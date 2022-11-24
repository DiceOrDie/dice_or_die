using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buff
{
    None
}

public enum DebuffName
{
    cold,
    frozen,   
    burn,     
    fragile,  
    paralysis,
    None
}

[CreateAssetMenu(fileName = "New Data", menuName = "Entity Stats/Data")]
public class EntityData_SO : ScriptableObject
{
    
    [Header("Entity Info")]
    public Sprite sprite_;
    public string name_;
    public int max_HP_;
    public int current_HP_;
    public int base_attack_;

    
    public EntityData_SO(EntityData_SO data) {
        sprite_ = data.sprite_;
        name_ = data.name_;
        max_HP_ = data.max_HP_;
        current_HP_ = data.current_HP_;
        base_attack_ = data.base_attack_;
    }
}