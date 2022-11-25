using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillTable{
    SesamePunch,
    AddPoint,
    AddHP,
    AddAttack,
    AddRoundDice,
    AddHandDice,
    AddDropFish

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
            last_used = state.round_count;
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
    public Skill_AddPoint(Skill_base data) : base(data){
        id = 2;
        name = SkillTable.AddPoint;
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("髒髒檢測");
        if(state.game_state != action_state && level > 0) return false;
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

public class Skill_AddHP : Skill_base
{
    public Skill_AddHP(Skill_base data) : base(data){
        id = 3;
        name = SkillTable.AddHP;
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("胖胖檢測");
        if(state.game_state != action_state && level > 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameManager.instance.player.max_HP_ += (10 + 10 * level);
            GameManager.instance.player.current_HP_ = GameManager.instance.player.max_HP_;
            yield return null;
        }
    }
}

public class Skill_AddAttack : Skill_base
{
    public Skill_AddAttack(Skill_base data) : base(data){
        id = 4;
        name = SkillTable.AddAttack;
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("爪爪檢測");
        if(state.game_state != action_state && level > 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameManager.instance.player.base_attack_ += level;
            yield return null;
        }
    }
}

public class Skill_AddRoundDice : Skill_base
{
    public Skill_AddRoundDice(Skill_base data) : base(data){
        id = 4;
        name = SkillTable.AddRoundDice;
        action_state = GameState.kPlayerDrawDice;
    }
    public override bool isValid(State state)
    {
        Debug.Log("飯飯檢測");
        if(state.game_state != action_state && level > 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameObject dice1 = GameManager.Instantiate(GameManager.instance.backpack.basic_dice_prefab_, GameManager.instance.backpack.hands_parent_);
            Hands.instance.Add(dice1);
            if(level >= 2){
                if(UnityEngine.Random.Range(0,1) == 1)
                {
                    GameObject dice2 = GameManager.Instantiate(GameManager.instance.backpack.basic_dice_prefab_, GameManager.instance.backpack.hands_parent_);
                    Hands.instance.Add(dice2);
                }
                else if(level == 3) {
                    GameObject dice2 = GameManager.instance.backpack.PickDice();
                    Hands.instance.Add(dice2);
                }
            }
            yield return null;
        }
    }
}

public class Skill_AddHandDice : Skill_base
{
    public Skill_AddHandDice(Skill_base data) : base(data){
        id = 4;
        name = SkillTable.AddHandDice;
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("倉鼠檢測");
        if(state.game_state != action_state && level > 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameManager.instance.hands.hands_limit_ += 1 + 1 * level;
            yield return null;
        }
    }
}