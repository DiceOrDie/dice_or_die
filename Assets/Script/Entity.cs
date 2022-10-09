using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    EntityData_SO entity_info;
    #region Read for EntityData_SO
    public int max_HP
    {
        get { if(entity_info) return entity_info.max_HP; else return 0; }
        set { entity_info.max_HP = value; }
    }
    public int current_HP
    {
        get { if(entity_info) return entity_info.current_HP; else return 0; }
        set { entity_info.current_HP = value; }
    }
    public int base_attack
    {
        get { if(entity_info) return entity_info.base_attack; else return 0; }
        set { entity_info.base_attack = value; }
    }
    public int buff
    {
        get { if(entity_info) return entity_info.buff; else return 0; }
        set { entity_info.buff = value; }
    }
    public int debuff
    {
        get { if(entity_info) return entity_info.debuff; else return 0; }
        set { entity_info.debuff = value; }
    }
    #endregion

    public enum Buff
    {
        None = 0b_0000_0000
    }
    public enum Debuff
    {
        None =      0b_0000_0000,
        Freeze =    0b_0000_0001,
        Burn =      0b_0000_0010
    }
    // Start is called before the first frame update

    public int DamageCalculate(int rollnum = 0, int skill_damage = 0, int debuff_damage = 0, int damage_rate = 100)
    {
        // 造成傷害=(基礎攻擊+骰子點數總和+技能傷害+異常效果傷害)*(傷害加成%)
        int damage = (base_attack + rollnum + skill_damage + debuff_damage) * damage_rate / 100;
        return damage;
    }
    public Entity AttacktoEntity(ref Entity entity, int damage)
    {
        
        // entity.health -= damage;
        //entity.health = (this.attack - 
        return this;
    }

    
}
