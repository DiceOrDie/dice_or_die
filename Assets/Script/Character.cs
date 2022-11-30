using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : Entity
{
    // C# 預設類別都是call by reference 所以其實不用加 ref
    // 雖然有點差別，但這邊不用(https://ithelp.ithome.com.tw/articles/10255682?sc=rss.qu)
    // public GameObject bullet;
    [SerializeField]
    private Character_SO character_data_so;
    // [HideInInspector]
    public Character_SO character_info;
    public AudioSource skill_audio;
    public Text skill_text;
    #region Read for CharacterData_SO
    public override int max_HP_
    {
        get { if(character_info) return character_info.max_HP_; else return 0; }
        set { character_info.max_HP_ = value; max_HP_text_.text = value.ToString(); }
    }
    public override int current_HP_
    {
        get { if(character_info) return character_info.current_HP_; else return 0; }
        set { character_info.current_HP_ = value; current_HP_text_.text = value.ToString(); }
    }
    public override int base_attack_
    {
        get { if(character_info) return character_info.base_attack_; else return 0; }
        set { character_info.base_attack_ = value; }
    }
    public override int fish_nums_
    {
        get { if(character_info) return character_info.fish_nums_; else return 0; }
        set { character_info.fish_nums_ = value; }
    }
    public List<Skill_base> skill_list
    {
        get { return character_info.skill_list; }
        set { character_info.skill_list = value; }
    } 
    #endregion
    private void Awake()
    {
        CharacterInit();
    }
    public void CharacterInit()
    {
        character_info = Instantiate<Character_SO>(character_data_so);
        entity_info = character_info;
        // character_info = new Character_SO(character_info);
        Debug.Log(debuffs_);
        List<Skill_base> new_skill_list = new List<Skill_base>();
        foreach (Skill_base skill in skill_list) {
            int index;
            switch(skill.name)
            {
                case SkillTable.SesamePunch: 
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_SesamePunch(skill_list[index]));
                    break;
                case SkillTable.AddPoint:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddPoint(skill_list[index]));
                    break;
                case SkillTable.AddHP:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddHP(skill_list[index]));
                    break;
                case SkillTable.AddAttack:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddAttack(skill_list[index]));
                    break;
                case SkillTable.AddRoundDice:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddRoundDice(skill_list[index]));
                    break;
                case SkillTable.AddHandDice:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddHandDice(skill_list[index]));
                    break;
                case SkillTable.AddDropFish:
                    index = skill_list.FindIndex(x => x.name == skill.name);
                    new_skill_list.Add(new Skill_AddDropFish(skill_list[index]));
                    break;
                default:
                    break;
            }
        }
        skill_list = new_skill_list;
    }
    public bool CanAttack()
    {
        if(debuffs_["frozen"] > 0 || debuffs_["paralysis"] > 0)
            return false;
        else
            return true;
    }
    public string Attack(List<Dice> rolled_dice_list, List<Monster> monsters)
    {
        if(!CanAttack())
            return "0";
        
        int attack_damage = base_attack_;
        foreach(Dice dice in rolled_dice_list)
        {
            attack_damage += dice.point_;
        }
        // GameObject b = Instantiate(bullet, this.transform);
        // b.SetActive(true);
        // b.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);

        // monsters[0].current_HP_ -= attack_damage;
        monsters[0].getDamage(-attack_damage);
        animator_.SetTrigger("attack");
        return attack_damage.ToString();
    }
    public void EarnFish(int fish_num) {
        this.fish_nums_ += fish_num;
    }
    public void UpdateState()
    {
        if(this.debuffs_["burn"] > 0)
        {
            current_HP_ -= this.debuffs_["burn"];
            this.debuffs_["burn"]--;
        }

        if(this.debuffs_["frozen"] > 0)
        {
            this.debuffs_["frozen"]--;
        }

        if(this.debuffs_["paralysis"] > 0)
        {
            this.debuffs_["paralysis"]--;
        }

        if(this.debuffs_["cold"] >= 8) {
            this.debuffs_["cold"] -= 8;
            this.debuffs_["frozen"]++;
        }
    }
    public IEnumerator UseSkill() {
        skill_audio.Play();
        skill_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        skill_text.gameObject.SetActive(false);
    }
    
}
