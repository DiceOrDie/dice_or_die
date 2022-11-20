using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : Entity
{
    // Monster 已經繼承 Entity 所以不用再加這個了
    // EntityData_SO entity_info;
    // #region Read for EntityData_SO
    [SerializeField]
    
    System.Random random = new System.Random();
    
    public MonsterBehaviorList_SO monsterBehavior_;

    public Queue<MonsterBehavior_SO> attack_queue_;

    private void Start() {
        EntityInit();
        attack_queue_ = new Queue<MonsterBehavior_SO>();
    }
    public void init(string monster_name)
    {
        switch(monster_name)
        {
            // case "purple_slime":
            //     max_HP_ = 20;
            //     current_HP_ = 20;
            //     base_attack_ = 0;
            //     monsterBehavior_.behaviors_list.Add( new MonsterBehaviors_SO());
            //     monsterBehavior_.behaviors_list[0].AddBehavior( new MonsterBehavior_SO(){
            //         name = "base_attack",
            //         damage = 2,
            //         cold = 0,
            //         frozen = 0,
            //         burn = 0,
            //         heal = 0,
            //         fragile = 0,                    
            //         paralysis = 0
            //     });
            //     break;
            default:
                Console.WriteLine("No such monster name: ", monster_name);
                break;
        }
    }
    public void ReloadAttack()
    {
        
        foreach(MonsterBehavior_SO behaviour in monsterBehavior_.behaviors_list[random.Next(monsterBehavior_.behaviors_list.Count)].behaviors)
        {
            attack_queue_.Enqueue(behaviour);
        }
    }

    public bool CanAttack()
    {
        if(this.debuffs_["frozen"] > 0 || this.debuffs_["paralysis"] > 0)
            return false;
        else
            return true;
    }
    public string Attack(Character character)
    {
        if(!CanAttack())
            return "0";

        if(attack_queue_.Count == 0)
            ReloadAttack();

        MonsterBehavior_SO now_attack = attack_queue_.Dequeue();

        // character.current_HP_ -= now_attack.damage;
        character.getDamage(-now_attack.damage);
        character.debuffs_["cold"] += now_attack.cold;
        character.debuffs_["burn"] += now_attack.burn;
        character.debuffs_["frozen"] += now_attack.frozen;
        character.debuffs_["fragile"] += now_attack.fragile;
        character.debuffs_["paralysis"] += now_attack.paralysis;
        current_HP_ += now_attack.heal;
        if(current_HP_ > max_HP_)
            current_HP_ = max_HP_;
        animator_.SetTrigger("attack");


        return now_attack.damage.ToString();
    }

    public void UpdateState(){
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

    // #endregion
    void OnTriggerEnter2D(Collider2D Other)
    {
        print("Hit monster:" + gameObject.name);
        Other.gameObject.SetActive(false);
    }
}
