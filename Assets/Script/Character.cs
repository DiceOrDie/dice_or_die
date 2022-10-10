using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    // C# 預設類別都是call by reference 所以其實不用加 ref
    // 雖然有點差別，但這邊不用(https://ithelp.ithome.com.tw/articles/10255682?sc=rss.qu)
    public void Attack(List<Dice> rolled_dice_list, List<Monster> monsters)
    {
        int attack_damage = base_attack_;
        foreach(Dice dice in rolled_dice_list)
        {
            attack_damage += dice.point_;
        }

        monsters[0].current_HP_ -= attack_damage;

        return;
    }
}
