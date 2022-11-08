using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] EntityData_SO entity_data_so;
    [SerializeField] Text current_HP_text_;
    [SerializeField] Text max_HP_text_;
    [SerializeField] Text damage_text_;
    [SerializeField] Animator damage_anaimator_;
    [SerializeField] AudioSource hurt_sound_;
    EntityData_SO entity_info;

    #region Read for EntityData_SO
    public int max_HP_
    {
        get { if(entity_info) return entity_info.max_HP_; else return 0; }
        set { entity_info.max_HP_ = value; max_HP_text_.text = value.ToString(); }
    }
    public int current_HP_
    {
        get { if(entity_info) return entity_info.current_HP_; else return 0; }
        set { entity_info.current_HP_ = value; current_HP_text_.text = value.ToString(); }
    }
    public int base_attack_
    {
        get { if(entity_info) return entity_info.base_attack_; else return 0; }
        set { entity_info.base_attack_ = value; }
    }
    #endregion
    public void EntityInit() {
        entity_info = new EntityData_SO(entity_data_so);
        current_HP_text_.text = current_HP_.ToString();
        max_HP_text_.text = max_HP_.ToString();
    }
    public bool IsAlive()
    {
        return this.current_HP_ > 0;
    }

    public Dictionary<string, int> state_ = new Dictionary<string, int>()
    {
        { "cold",      0 }, //寒冷
        { "frozen",    0 }, //凍結
        { "burn",      0 }, //燃燒
        { "fragile",   0 }, //易碎
        { "paralysis", 0 }, //麻痺
    };
    public void PlayHurtSound() {
        hurt_sound_.Play();
    }
    public int getDamage(int damage) {
        current_HP_ += damage;
        damage_text_.text = damage.ToString();
        if(damage <= 0) {
            damage_text_.color = Color.red;
        }
        else {
            damage_text_.color = Color.green;
        }
        return current_HP_;
    }
    public IEnumerator ShowDamageText() {
        damage_anaimator_.SetBool("get damage", true);
        float animationLength = damage_anaimator_.GetCurrentAnimatorStateInfo(0).length - 0.1f;
        yield return new WaitForSecondsRealtime(animationLength);
        damage_anaimator_.SetBool("get damage", false);
    }
    // Start is called before the first frame update

    // public int DamageCalculate(int rollnum = 0, int skill_damage = 0, int debuff_damage = 0, int damage_rate = 100)
    // {
    //     // 造成傷害=(基礎攻擊+骰子點數總和+技能傷害+異常效果傷害)*(傷害加成%)
    //     int damage = (base_attack + rollnum + skill_damage + debuff_damage) * damage_rate / 100;
    //     return damage;
    // }

    
}
