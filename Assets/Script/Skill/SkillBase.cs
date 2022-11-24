using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillTable{
    SesamePunch,
    AddPoint
}

[System.Serializable]
public class Skill_base {
    public Skill_base(Skill_base data) {
        id = data.id;
        name = data.name;
        level = data.level;
        action_state = data.action_state;
    }
    public SkillTable name;
    public int id;
    public int level;
    public GameState action_state;
    public virtual bool isValid(State state) { Debug.Log("普通檢測"); return true;}
    public virtual IEnumerator Effect(State state) { isValid(state); yield return null;}
}

public class Skill_SesamePunch : Skill_base
{
    public int last_used = 0;
    public Skill_SesamePunch(Skill_base data) : base(data){
        id = 1;
        name = SkillTable.SesamePunch;
        action_state = GameState.kPlayerAttack;
    }
    public override bool isValid(State state)
    {
        if(state.game_state != action_state) return false;
        Debug.Log("芝麻拳檢測");
        bool valid = false;
        bool all_odd = true;
        bool all_even = true;
        valid = state.roll_result.Count >= 2;
        foreach ( Dice dice in state.roll_result) {
            all_odd &= ((dice.point_ % 2) == 1 | (dice.type_ == DiceType.cheat));
            all_even &= ((dice.point_ % 2) == 0 | (dice.type_ == DiceType.cheat));
            Debug.Log("all_odd: " + all_odd);
            Debug.Log("all_even: " + all_even);
        }
        valid = valid & (all_odd | all_even);
        Debug.Log("檢測結果: " + valid);
        return valid;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state)) {
            int damage = (int)Math.Pow(2, state.roll_result.Count);
            Debug.Log("芝麻拳 : -"+damage.ToString());
            GameManager.instance.monsters[0].getDamage(-damage);
            GameManager.instance.player.animator_.SetTrigger("attack");;
            yield return GameManager.instance.monsters[0].ShowDamageText();
        }
    }
}

public class Skill_AddPoint : Skill_base
{
    public int last_used = 0;
    public Skill_AddPoint(Skill_base data) : base(data){
        id = 2;
        name = SkillTable.AddPoint;
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("髒髒檢測");
        if(state.game_state != action_state) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            foreach (GameObject dice_o in GameManager.instance.backpack.dice_initial_)
            {
                Dice dice = dice_o.GetComponent<Dice>();
                if(dice.type_ == DiceType.normal)
                {
                    Debug.Log(dice.name);
                    dice.min_point_bonus = level;
                    dice.max_point_bonus = level;
                }
            }
            yield return null;
        }
    }
}