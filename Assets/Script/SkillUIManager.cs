using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public static bool upgrading = false;
    public static bool finished = false;
    public GameObject confirm;
    List<Skill_base> skills = new List<Skill_base>();
    List<Text> skills_name_text = new List<Text>();
    List<Text> skills_level_text = new List<Text>();
    List<Button> skills_button = new List<Button>();
    // Start is called before the first frame update
    public void Init()
    {
        // gameObject.SetActive(true);
        
        skills = GameManager.instance.player.skill_list;
        confirm.SetActive(false);
        // skills[0] 是芝麻拳
        for (int i = 1; i < skills.Count; i++) {
            skills_name_text.Add(transform.Find("Common/Skill" + i.ToString() + "/Content").gameObject.GetComponent<Text>());
            skills_level_text.Add(transform.Find("Common/Skill" + i.ToString() + "/level/1").gameObject.GetComponent<Text>());
            skills_button.Add(transform.Find("Common/Skill" + i.ToString() + "/Add Button").gameObject.GetComponent<Button>());
            skills_name_text[i-1].text = skills[i].name;
            skills_level_text[i-1].text = skills[i].level.ToString();
        }
        // gameObject.SetActive(false);
    }
    public void Finished() {
        finished = true;
    }

    public void Upgrade(int index) {
        foreach (Button button in skills_button) {
            button.gameObject.SetActive(false);
        }
        skills[index].Upgrade();
        skills_level_text[index-1].text = skills[index].level.ToString();
        upgrading = false;
    }
    public void Open() {
        gameObject.SetActive(true);
        for (int i = 1; i < skills.Count; i++) {
            skills_button[i-1].gameObject.SetActive(upgrading & skills[i].isUpgradable());
            skills_name_text[i-1].text = skills[i].name;
            skills_level_text[i-1].text = skills[i].level.ToString();
        }
    }
}
