using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] EntityData_SO entity_data_so;
    [SerializeField] protected Text current_HP_text_;
    [SerializeField] protected Text max_HP_text_;
    [SerializeField] protected Text damage_text_;
    [SerializeField] public Animator animator_;
    [SerializeField] protected AudioSource hurt_sound_;
    [SerializeField] protected AudioSource die_sound_;
    
    public Dictionary<string, int> debuffs_ = new Dictionary<string, int>()
    {
        { "cold",      0 }, //寒冷
        { "frozen",    0 }, //凍結
        { "burn",      0 }, //燃燒
        { "fragile",   0 }, //易碎
        { "paralysis", 0 }, //麻痺
    };
    [HideInInspector]
    public EntityData_SO entity_info;

    #region Read for EntityData_SO
    public virtual int max_HP_
    {
        get { if(entity_info) return entity_info.max_HP_; else return 0; }
        set { entity_info.max_HP_ = value; max_HP_text_.text = value.ToString(); }
    }
    public virtual int current_HP_
    {
        get { if(entity_info) return entity_info.current_HP_; else return 0; }
        set { entity_info.current_HP_ = value; current_HP_text_.text = value.ToString(); }
    }
    public virtual int base_attack_
    {
        get { if(entity_info) return entity_info.base_attack_; else return 0; }
        set { entity_info.base_attack_ = value; }
    }
    public virtual string description_
    {
        get { if(entity_info) return entity_info.description_; else return null; }
    }
    public virtual int fish_nums_
    {
        get { if(entity_info) return entity_info.fish_nums_; else return 0; }
        set { entity_info.fish_nums_ = value; }
    }
    #endregion
    public void EntityInit() {
        entity_info = Instantiate(entity_data_so);
        current_HP_text_.text = current_HP_.ToString();
        max_HP_text_.text = max_HP_.ToString();
    }
    public bool IsAlive()
    {
        return this.current_HP_ > 0;
    }
    public IEnumerator Die() {
        animator_.SetTrigger("die");
        float animationLength = animator_.GetCurrentAnimatorStateInfo(0).length - 0.1f;
        yield return new WaitForSecondsRealtime(animationLength);
        Destroy(gameObject);
    }
    public int GetFishNum() {
        return fish_nums_;
    }
    public void PlayHurtSound() {
        hurt_sound_.Play();
    }
    public void PlayDieSound() {
        die_sound_.Play();
    }
    public int getDamage(int damage) {
        current_HP_ += damage;
        if (current_HP_ > max_HP_) {
            damage = damage - (current_HP_ - max_HP_);
            current_HP_ = max_HP_;
        }
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
        animator_.SetTrigger("hurt");
        float animationLength = animator_.GetCurrentAnimatorStateInfo(0).length - 0.1f;
        yield return new WaitForSecondsRealtime(animationLength);
    }
    // Start is called before the first frame update

    // public int DamageCalculate(int rollnum = 0, int skill_damage = 0, int debuff_damage = 0, int damage_rate = 100)
    // {
    //     // 造成傷害=(基礎攻擊+骰子點數總和+技能傷害+異常效果傷害)*(傷害加成%)
    //     int damage = (base_attack + rollnum + skill_damage + debuff_damage) * damage_rate / 100;
    //     return damage;
    // }

    void OnMouseDown()
    {
        InfoPanel info_panel_ = InfoPanel.instance;
        info_panel_.UpdateEntityInfo(this);
        return;
    }
}
