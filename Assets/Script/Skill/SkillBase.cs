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
        type = data.type;
        name = data.name;
        level = data.level;
        action_state = data.action_state;
    }
    public SkillTable type;
    [HideInInspector] public int id;
    public int level;
    [HideInInspector] public string name;
    [HideInInspector] public GameState action_state;
    public virtual bool isValid(State state) { Debug.Log("普通檢測"); return true;}
    public virtual IEnumerator Effect(State state) { isValid(state); yield return null;}
    public void Upgrade() {
        level += 1;
    }
    public bool isUpgradable() { return level < 3; }
}

public class Skill_SesamePunch : Skill_base
{
    public int last_used = 0;
    public Skill_SesamePunch(Skill_base data) : base(data){
        id = 1;
        type = SkillTable.SesamePunch;
        name = "芝麻拳";
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
            yield return new WaitForSeconds(0.2f);
            last_used = state.round_count;
            int damage = (int)Math.Pow(2, state.roll_result.Count);
            Debug.Log("芝麻拳 : -"+damage.ToString());
            GameManager.instance.StartCoroutine(GameManager.instance.player.UseSkill());
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
        type = SkillTable.AddPoint;
        name = "本喵骰子只是沾到髒東西了";
        action_state = GameState.kRoomStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("髒髒檢測");
        if(state.game_state != action_state || level == 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameManager.instance.backpack.dice_min_bouns = level;
            GameManager.instance.backpack.dice_max_bouns = level;
            foreach (GameObject dice_o in GameManager.instance.backpack.own_dice_gameobject_)
            {
                Dice dice = dice_o.GetComponent<Dice>();
                if(dice.type_ == DiceType.normal)
                {
                    Debug.Log(dice.name);
                    dice.min_point_bonus = level;
                    dice.max_point_bonus = level;
                } 
            }
            foreach (GameObject dice_o in GameManager.instance.hands.dice_o_list_)
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
        type = SkillTable.AddHP;
        name = "本喵才不是胖，是忘記剪毛";
        action_state = GameState.kGameStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("胖胖檢測");
        if(state.game_state != action_state || level == 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            Debug.Log("原本血量: " + GameManager.instance.player.max_HP_.ToString());
            Debug.Log("增加血量: " + (10 + 10 * (1 << (level - 1))).ToString());
            GameManager.instance.player.max_HP_ += 10 + 10 * (1 << (level - 1));
            GameManager.instance.player.current_HP_ = GameManager.instance.player.max_HP_;
            yield return null;
        }
    }
}

public class Skill_AddAttack : Skill_base
{
    public Skill_AddAttack(Skill_base data) : base(data){
        id = 4;
        type = SkillTable.AddAttack;
        name = "伸出本喵的爪爪";
        action_state = GameState.kGameStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("爪爪檢測");
        if(state.game_state != action_state || level == 0) return false;
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
        id = 5;
        type = SkillTable.AddRoundDice;
        name = "給本喵加餐喵";
        action_state = GameState.kPlayerDrawDice;
    }
    public override bool isValid(State state)
    {
        Debug.Log("飯飯檢測");
        if(state.game_state != action_state || level == 0) return false;
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
        id = 6;
        type = SkillTable.AddHandDice;
        name = "雖然本喵不是倉鼠";
        action_state = GameState.kGameStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("倉鼠檢測");
        if(state.game_state != action_state || level == 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            GameManager.instance.hands.hands_limit_ += 1 + (1 << (level-1));
            yield return null;
        }
    }
}

public class Skill_AddDropFish : Skill_base
{
    public Skill_AddDropFish(Skill_base data) : base(data){
        id = 7;
        type = SkillTable.AddDropFish;
        name = "快給本喵小魚乾喵";
        action_state = GameState.kGameStart;
    }
    public override bool isValid(State state)
    {
        Debug.Log("小魚乾檢測");
        if(state.game_state != action_state || level == 0) return false;
        return true;
    }
    public override IEnumerator Effect(State state)
    {
        if(isValid(state))
        {
            yield return null;
        }
    }
}