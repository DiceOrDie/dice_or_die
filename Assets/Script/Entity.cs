using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    EntityData_SO entity_info;
    #region Read for EntityData_SO
    public int max_HP_
    {
        get { if(entity_info) return entity_info.max_HP_; else return 0; }
        set { entity_info.max_HP_ = value; }
    }
    public int current_HP_
    {
        get { if(entity_info) return entity_info.current_HP_; else return 0; }
        set { entity_info.current_HP_ = value; }
    }
    public int base_attack_
    {
        get { if(entity_info) return entity_info.base_attack_; else return 0; }
        set { entity_info.base_attack_ = value; }
    }
    #endregion

    public bool IsAlive()
    {
        return this.current_HP_ <= 0;
    }

    public Dictionary<string, int> state_ = new Dictionary<string, int>()
    {
        { "cold",      0 }, //寒冷
        { "freeze",    0 }, //凍結
        { "burn",      0 }, //燃燒
        { "fragile",   0 }, //易碎
        { "paralysis", 0 }, //麻痺
        { "heal",      0 }  //回復
    };
    // Start is called before the first frame update

    // public int DamageCalculate(int rollnum = 0, int skill_damage = 0, int debuff_damage = 0, int damage_rate = 100)
    // {
    //     // 造成傷害=(基礎攻擊+骰子點數總和+技能傷害+異常效果傷害)*(傷害加成%)
    //     int damage = (base_attack + rollnum + skill_damage + debuff_damage) * damage_rate / 100;
    //     return damage;
    // }

    
}

