using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate State Skill(State state);

[CreateAssetMenu(fileName = "New Skill List", menuName = "Entity Stats/Skill List")]
public class PlayerSkillList_SO : ScriptableObject
{
    
    [Header("Skill List")]
    public List<Skill> game_start_ = new List<Skill>();
    public List<Skill> round_start_ = new List<Skill>();
    public List<Skill> after_player_attack_ = new List<Skill>();
    public List<Skill> after_enemy_attack_ = new List<Skill>();
    // public List<Skill> gamestart_ = new List<Skill>();
    // public List<Skill> gamestart_ = new List<Skill>();

}

public interface IPlayerSkill {
}