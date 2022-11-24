using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public delegate State Skill(State state);

public enum CharacterTable {
    SesameTangyuan,
}


[CreateAssetMenu(fileName = "New Character", menuName = "Entity Stats/Character")]
public class Character_SO : EntityData_SO
{
    public CharacterTable character;
    public List<Skill_base> skill_list;
    public Character_SO(Character_SO data) : base(data) {
        sprite_ = data.sprite_;
        name_ = data.name_;
        max_HP_ = data.max_HP_;
        current_HP_ = data.current_HP_;
        base_attack_ = data.base_attack_;
    }
    // public Character_SO(EntityData_SO data) : base(data) {
    //     foreach (Skill_base skill in skill_list) {
    //         if(skill.name == SkillTable.SesamePunch) {
    //             int s = skill_list.FindIndex(x => x.name == skill.name);
    //             skill_list[s] = new Skill_SesamePunch();
    //         }
    //     }
    // }
}


// [System.Serializable]
// public class Skill_base {
//     public int id;
//     public SkillTable name;
//     public int level;
// }
// [System.Serializable]
// public class Skills : Skill_base{
//     public class Skill_SesamePunch : Skill_base{

//     }
// }