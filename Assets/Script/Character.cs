using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    // C# 預設類別都是call by reference 所以其實不用加 ref
    // 雖然有點差別，但這邊不用(https://ithelp.ithome.com.tw/articles/10255682?sc=rss.qu)
    // public GameObject bullet;
    public bool CanAttack()
    {
        if(this.debuffs_["frozen"] > 0 || this.debuffs_["paralysis"] > 0)
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

    
}
