using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior_SO : ScriptableObject
{
    public class Behavior {
        public string name;
        public int damage;
        public int cold;
        public int frozen;
        public int burn;
        public int heal;
        
        public int fragile;
        
        public int paralysis;
    }
    [Header("Entity Info")]
    public List<List<Behavior>> behaviors_list = new List<List<Behavior>>();
}

public class Monster : Entity
{
    // Monster 已經繼承 Entity 所以不用再加這個了
    // EntityData_SO entity_info;
    // #region Read for EntityData_SO
    [SerializeField]
    
    System.Random random = new System.Random();
    
    public MonsterBehavior_SO monsterBehavior_;

    public Queue<MonsterBehavior_SO.Behavior> attack_queue_;

    public void init(string monster_name)
    {
        switch(monster_name)
        {
            case "purple_slime":
                max_HP_ = 20;
                current_HP_ = 20;
                base_attack_ = 0;
                monsterBehavior_.behaviors_list.Add( new List<MonsterBehavior_SO.Behavior>());
                monsterBehavior_.behaviors_list[0].Add( new MonsterBehavior_SO.Behavior(){
                    name = "base_attack",
                    damage = 2,
                    cold = 0,
                    frozen = 0,
                    burn = 0,
                    heal = 0,
                    fragile = 0,                    
                    paralysis = 0
                });
                break;
            default:
                Console.WriteLine("No such monster name: ", monster_name);
                break;
        }
    }
    public void ReloadAttack()
    {
        
        foreach(MonsterBehavior_SO.Behavior behaviour in monsterBehavior_.behaviors_list[random.Next(monsterBehavior_.behaviors_list.Count)])
        {
            attack_queue_.Enqueue(behaviour);
        }
    }
    public void Attack(Character character)
    {
        if(attack_queue_.Count == 0)
            ReloadAttack();

        MonsterBehavior_SO.Behavior now_attack = attack_queue_.Dequeue();

        character.current_HP_ -= now_attack.damage;
        character.state_["cold"] += now_attack.cold;
        character.state_["burn"] += now_attack.burn;
        character.state_["frozen"] += now_attack.frozen;
        character.state_["fragile"] += now_attack.fragile;
        character.state_["paralysis"] += now_attack.paralysis;
        current_HP_ += now_attack.heal;
    }

    public void UpdateState(){
        // TODO
    }

    // #endregion

}
