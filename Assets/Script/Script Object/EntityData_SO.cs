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
    public Dictionary<string, int> debuffs_;
    
    public EntityData_SO(EntityData_SO data) {
        sprite_ = data.sprite_;
        name_ = data.name_;
        max_HP_ = data.max_HP_;
        current_HP_ = data.current_HP_;
        base_attack_ = data.base_attack_;
        if(data.debuffs_ != null)
            debuffs_ = data.debuffs_;
        else
            debuffs_ = new Dictionary<string, int>()
                        {
                            { "cold",      0 }, //寒冷
                            { "frozen",    0 }, //凍結
                            { "burn",      0 }, //燃燒
                            { "fragile",   0 }, //易碎
                            { "paralysis", 0 }, //麻痺
                        };
    }
}