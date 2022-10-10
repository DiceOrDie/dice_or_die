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
    }
    [Header("Entity Info")]
    public List<Behavior> behaviors_list;

}

public class Monster : Entity
{
    // Monster 已經繼承 Entity 所以不用再加這個了
    // EntityData_SO entity_info;
    // #region Read for EntityData_SO
    [SerializeField]
    public MonsterBehavior_SO monsterBehavior_;
    
    public Q<BehaMonsterBehavior_SO.Behavior> attack_qu;

    public void ReloadAttack()
    {
        foreach(MonsterBehavior_SO.Behavior behaviour in monsterBehavior_.behaviors_list)
        {
            attack_queue_.Enqueue(behaviour);
        }
    }
    public void Attack(Character character)
    {
        if(attack_queue_.empty())//badum tss
            ReloadAttack();

        MonsterBehavior_SO.Behavior now_attack = attack_queue.Dequeue();
        
        character.current_HP_ -= now_attack.damage;
        character.state_["cold"] += now_attack.cold;
        character.state_["burn"] += now_attack.burn;
        character.state_["frozen"] += now_attack.frozen;
        character.state_["heal"] += now_attack.heal;
    }

    public void UpdateState(){
        // TODO
    }

    // #endregion

}
