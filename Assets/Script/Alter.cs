using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alter : MonoBehaviour
{

    private Character player_;
    public Image fish_image_;
    private Text fish_name_;
    public Text fish_num_;
    public Text change_info;
    private bool is_finished_;

    public GameObject warning_;

    private string change_info_base_ = "以 {0} 個小魚乾供俸兔兔神換取 {1} {2}";
    private int hp_price_ = 10;
    private int get_hp_value_ = 13;
    private string hp_unit = "點血量";

    private string current_fish_info_base_ = "擁有小魚乾數：{0}";



    public void Init() {
        player_ = GameManager.instance.player;
        change_info.text = string.Format(change_info_base_, hp_price_, get_hp_value_, hp_unit);
        fish_num_.text = string.Format(current_fish_info_base_, player_.GetFishNum());
    }

    public bool isFinished {
        get { return is_finished_; }
        set { is_finished_ = value; }
    }

    public void TradeButton() {
        ChangeFishToHP();
    }

    public void ChangeFishToYarn() {
        Debug.Log("Useless");
    }

    public void ChangeFishToHP() {
        Debug.Log("ChangeFishToHP");
        if (player_.GetFishNum() >= hp_price_) {
            player_.EarnFish(-hp_price_);
            player_.getDamage(get_hp_value_);
            StartCoroutine(player_.ShowDamageText());
            fish_num_.text = string.Format(current_fish_info_base_, player_.GetFishNum());
        }
        else {
            warning_.SetActive(true);
        }
    }

    public void ExitAlter() {
        is_finished_ = true;
    }

    public void ChangeFishToSkill() {
        Debug.Log("Useless");
    }
}
