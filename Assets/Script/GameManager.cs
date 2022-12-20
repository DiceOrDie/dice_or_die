// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class State {
    public int round_count = 0;
    public DiceChance dice_chance;
    public RollChance roll_chance;
    public List<Dice> roll_result;
    // List<Skill> bonus_skill;
    public int bonus_attack_count;
    public GameState game_state;
    public Character player;
    public List<Monster> monsters;
    public Backpack backpack;
    public Hands hands;
    public State()
    {
        dice_chance = new DiceChance();
        roll_chance = new RollChance();
        List<Dice> roll_result = new List<Dice>();
        bonus_attack_count = 0;
        game_state = GameState.kGameStart;
        // player = GameManager.instance.player;
        // monsters = GameManager.instance.monsters;
        // backpack =

    }
}

public class RoundLog {
    public State state;
    public List<Dice> dices;
    public EntityData_SO plyer_info;
    // public List<Skill> active_skill;
    public List<EntityData_SO> monsters_info;
    public List<MonsterBehavior_SO> monsters_behaviors;

}
public enum GameState {
    kGameStart,
    kRoomStart, 
    kRoomEnd, 
    kRoundStart, 
    kPlayerDrawDice, 
    kPlayerSelectDice, 
    kPlayerRollDice, 
    kPlayerAttack, 
    kMonsterAttack, 
    kRoundEnd,
    kUpgradeSkill,
    kLosing, 
    kWinning,
    kAlterStart,
};


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas_go_;
    public GameObject skill_table_go_;
    public GameObject backpack_go_;
    public GameObject hands_go_;
    public GameObject result_bar_;
    public LevelManagement level_manger_;
    public List<GameObject> monsters_gameobject_;
    public GameObject player_gameobject_;
    public Text state_text_;
    public Font font_;
    public Alter alter;

    static public GameManager instance;
    static public State state;


    private GameState game_state_;
    private bool gameover_flag_;
    private Character player_;
    private Backpack backpack_;
    private Hands hands_;
    private int room_tmp_fish_;
    public Character player{
        get { return player_; }
    }
    public List<Monster> monsters{
        get { return monsters_;}
    }
    public Backpack backpack{
        get { return backpack_; }
    }
    public Hands hands
    {
        get { return hands_;}
    }

    private List<Monster> monsters_;
    private List<Dice> rolled_dice_list_;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(this);
        
    }
    void Start() {
        
        backpack_ = backpack_go_.GetComponent<Backpack>();
        hands_ = hands_go_.GetComponent<Hands>();
        player_ = player_gameobject_.GetComponent<Character>();
        // level_manger_ = result_bar_.GetComponentInChildren<LevelManagement>();
        // level_manger_.NextLevel();
        room_tmp_fish_ = 0;
        // foreach(GameObject monster_gameobject in monsters_gameobject_) {
        //     monsters_.Add(monster_gameobject.GetComponent<Monster>());
        // }
        state = new State();
        StartCoroutine(GameRoutine());
    }
    IEnumerator GameRoutine() {
        gameover_flag_ = false;
        while (!gameover_flag_) {
            yield return UpdateStateText();
            switch (state.game_state) {
            case GameState.kGameStart:
                yield return GameStart();
                break;
            case GameState.kRoomStart:
                yield return RoomStart();
                break;
            case GameState.kRoundStart:
                yield return RoundStart();
                break;
            case GameState.kPlayerDrawDice:
                yield return PlayerDrawDice();
                break;
            case GameState.kPlayerSelectDice:
                yield return PlayerSelectDice();
                break;
            case GameState.kPlayerRollDice:
                yield return PlayerRollDice();
                break;
            case GameState.kPlayerAttack:
                yield return PlayerAttack();
                break;
            case GameState.kMonsterAttack:
                yield return MonsterAttack();
                break;
            case GameState.kRoundEnd:
                yield return RoundEnd();  
                break;
            case GameState.kUpgradeSkill:
                yield return UpgradeSkill();
                break;
            case GameState.kRoomEnd:
                yield return RoomEnd();
                break;
            case GameState.kAlterStart:
                yield return AlterStart();
                break;
            default:
                Debug.Log("Error in GameManager/GameRoutine()/switch-case");
                break;
            }
        }
        // Game Over
        SceneManager.LoadScene(0);
        // yield return UpdateStateText();
        
    }
    IEnumerator GameStart() {
        Debug.Log("GameStart() started.");
        PlayerGameInitialize();
        yield return new WaitUntil(() => SkillUIManager.upgrading == false);
        Debug.Log("GameStart() finished.");
        state.game_state = GameState.kRoomStart;
        yield return null;
    }
    IEnumerator RoomStart() {
        Debug.Log("RoomStart() started.");
        PlayerRoomInitialize();
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
        if(GameObject.Find("/RoomType").tag == "Battle")
        {
            MonsterRoomInitialize();
            
            Debug.Log("RoomStart() finished.");
            state.game_state = GameState.kRoundStart;
        }
        else if (GameObject.Find("/RoomType").tag == "Plot")
        {
            alter.gameObject.SetActive(true);
            state.game_state = GameState.kAlterStart;
        }
        yield return null;
    }
    
    IEnumerator AlterStart() {
        Debug.Log("AlterStart() finished.");
        alter.Init();
        alter.isFinished = false; 

        yield return new WaitUntil(() => alter.isFinished == true);
        alter.gameObject.SetActive(false);
        state.game_state = GameState.kRoomEnd;
    }



    IEnumerator RoundStart() {
        // TODO
        Debug.Log("RoundStart() started.");
        Debug.Log("RoundStart() finished.");
        state.game_state = GameState.kPlayerDrawDice;
        state.round_count += 1;
        yield return null;
    }

    IEnumerator PlayerDrawDice() {
        // TODO: turn on backpack UI
        Debug.Log("PlayerDrawDice() started.");
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
        backpack_.StartDraw();
        yield return new WaitUntil(() => backpack_.is_draw_on_going_ == false);
        Debug.Log("PlayerDrawDice() finished.");
        state.game_state = GameState.kPlayerSelectDice;
        //yield return null;
    }

    IEnumerator PlayerSelectDice() {
        // TODO
        if(!(player_.CanAttack())){
            Debug.Log("player can't attack");
            state.game_state = GameState.kPlayerRollDice;
            yield return null;
        }
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
        Debug.Log(player_.CanAttack());
        Debug.Log("PlayerSelectDice() started.");
        hands_.StartSelect();
        yield return new WaitUntil(() => hands_.is_select_on_going == false);
        rolled_dice_list_ = hands_.GetSelectedDice();
        Debug.Log("PlayerSelectDice() finished.");
        state.game_state = GameState.kPlayerRollDice;
        yield return null;
    }

    IEnumerator PlayerRollDice() {
        // TODO: roll all dice in rolled_dice_list_
        if(!player_.CanAttack()){
            Debug.Log("player can't attack");
            state.game_state = GameState.kPlayerAttack;
            yield return null;
        }
        Debug.Log("PlayerRollDice() started.");
        foreach (Dice dice in rolled_dice_list_) {
            int point = dice.RollDice();
            Debug.Log(point);
        }
        state.roll_result = rolled_dice_list_;

        Debug.Log("PlayerRollDice() finished.");
        state.game_state = GameState.kPlayerAttack;
        yield return null;
    }

    IEnumerator PlayerAttack() {
        
        Debug.Log("PlayerAttack() started.");

        string attack_damage = "0";
        attack_damage = player_.Attack(rolled_dice_list_, monsters_);
        Debug.Log("Attack: " + attack_damage);

        // yield return new WaitForSeconds(2);
        yield return DisplayAttackSummation(attack_damage, rolled_dice_list_);
        // yield return new WaitForSeconds(2);

        // if(!attack_damage.Equals("0"))
        //     yield return monsters_[0].ShowDamageText();
        
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }

        for(int i = 0; i < monsters_.Count; i++) {
            Monster monster = monsters_[i];
            if (!monster.IsAlive()) {
                room_tmp_fish_ += monster.GetFishNum();
                monsters_.Remove(monster);
                yield return monster.Die();
                // Destroy(monster.gameObject);
                i--;
            }
        }
        Debug.Log("PlayerAttack() finished.");
        if(isRoomEnd())
            state.game_state = GameState.kRoundEnd;
        state.game_state = GameState.kMonsterAttack;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MonsterAttack() {
        Debug.Log("MonsterAttack() started.");
        foreach (Monster monster in monsters_) {
            string attack_damage = "0";
            attack_damage = monster.Attack(player_);
            Debug.Log("Monster Attack: " + attack_damage);
            if(attack_damage != "0")
                yield return player_.ShowDamageText();
            // yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("MonsterAttack() finished.");
        if(isRoomEnd())
            state.game_state = GameState.kRoomEnd;
        state.game_state = GameState.kRoundEnd;
        yield return new WaitForSeconds(1f);
    }
    
    IEnumerator RoundEnd() {
        Debug.Log("RoundEnd() started.");
        player_.UpdateState();
        for(int i = 0; i < monsters_.Count; i++) {
            Monster monster = monsters_[i];
            monster.UpdateState();
            if (!monster.IsAlive()) {
                room_tmp_fish_ += monster.GetFishNum();
                monsters_.Remove(monster);
                monster.Die();
                // Destroy(monster.gameObject);
                i--;
            }
        }
        foreach ( Dice dice in rolled_dice_list_) {
            Destroy(dice.gameObject);
        }
        CleanRollResult();
        if(isRoomEnd())
            state.game_state = GameState.kRoomEnd;
        else 
            state.game_state = GameState.kRoundStart;
        Debug.Log("RoundEnd() finished.");
        yield return null;
    }
    IEnumerator RoomEnd() {
        Debug.Log("Room End");

        if(player_.IsAlive())
        {
            int now_Scene = SceneManager.GetActiveScene().buildIndex;
            print(now_Scene);
            // 跳關
            // while(now_Scene <= 6) {
            //     level_manger_.NextLevel();
            //     SceneManager.LoadScene(now_Scene + 1);
            //     now_Scene += 1;
            // } 
            if(now_Scene != 10)
            {
                level_manger_.NextLevel();
                SceneManager.LoadScene(now_Scene + 1);
                state.game_state = GameState.kUpgradeSkill;
            }
            else if(now_Scene == 10)
            {
                GameObject.Find("Background Music").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene(now_Scene + 1);
            }
            player_.EarnFish(room_tmp_fish_);
            Debug.Log("關卡獲得小魚乾數量：" + room_tmp_fish_.ToString());
            Debug.Log("當前身上的小魚乾數量：" + player_.GetFishNum().ToString());
        }
        else
            gameover_flag_ = true;
        room_tmp_fish_ = 0;
        yield return null;
    }
    IEnumerator UpgradeSkill() {
        SkillUIManager.upgrading = true;
        SkillUIManager.finished = false;
        skill_table_go_.GetComponent<SkillUIManager>().Open();
        yield return new WaitUntil(() => SkillUIManager.finished == true);
        state.game_state = GameState.kRoomStart;
    }
    
    void PlayerGameInitialize() {
        player_.CharacterInit();
        skill_table_go_.GetComponent<SkillUIManager>().Init();
        SkillUIManager.upgrading = true;
        skill_table_go_.GetComponent<SkillUIManager>().Open();
    }
    void PlayerRoomInitialize() {
        // hands.Clear();
    }
    
    void MonsterRoomInitialize() {
        monsters_ = GameObject.Find("/Monsters").GetComponentsInChildren<Monster>().ToList();
    }
    
    bool isRoomEnd() {
        if (!player_.IsAlive()) {
            gameover_flag_ = true;
            return true;
        }
        bool all_monster_die = true;
        foreach (Monster monster in monsters_) {
            if (monster.IsAlive()) {
                all_monster_die = false;
                break;
            }
        }
        if (all_monster_die) {
            return true;
        }
        return false;
    }

    IEnumerator UpdateStateText() {
        switch (state.game_state) {
        case GameState.kRoomStart:
            // state_text_.text = "Room Start";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kRoundStart:
            // state_text_.text = "Round Start";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kPlayerDrawDice:
            // state_text_.text = "Player Draw Dice";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kPlayerSelectDice:
            // state_text_.text = "Player Select Dice";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kPlayerRollDice:
            // state_text_.text = "Player Roll Dice";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kPlayerAttack:
            // state_text_.text = "Player Attack";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kMonsterAttack:
            // state_text_.text = "Monster Attack";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kRoundEnd:
            // state_text_.text = "Round End";
            // state_text_.gameObject.SetActive(true);
            // yield return new WaitForSeconds(1f);
            // state_text_.gameObject.SetActive(false);
            break;
        case GameState.kRoomEnd:
            if(!player_.IsAlive()){
                state_text_.text = "You Died.";
                state_text_.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                state_text_.gameObject.SetActive(false);
            }
            break;
        }

    }

    public void Pause() {
        Time.timeScale = 0;
    }
    public void Resume() {
        Time.timeScale = 1;
    }

    IEnumerator DisplayAttackSummation(string attack_damage, List<Dice> rolled_dice_list_) {
        // foreach (Dice dice in rolled_dice_list_) {
        //     dice.gameObject.SetActive(false);
        // }

        GameObject playerIcon = GameObject.Find("Player Icon");
        playerIcon.GetComponent<Image>().enabled = true;
        playerIcon.GetComponent<Image>().sprite = GameObject.Find("Player/Sprite").GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(0.4f);
        playerIcon.gameObject.GetComponent<Image>().enabled = false;
        GameObject playerIcon_result_text_o = Instantiate(new GameObject(playerIcon.gameObject.name + "_result"), playerIcon.transform);
        Text playerIcon_result_text = playerIcon_result_text_o.AddComponent<Text>();
        
        playerIcon_result_text_o.transform.parent = playerIcon.gameObject.transform;
        playerIcon_result_text_o.transform.position = playerIcon.gameObject.transform.position;
        playerIcon_result_text.text = player_.base_attack_.ToString();
        playerIcon_result_text.font = font_;
        playerIcon_result_text.alignment = TextAnchor.MiddleCenter;
        playerIcon_result_text.fontSize = 60;
        playerIcon_result_text.color = Color.gray;

        // GameObject attackSumText = GameObject.Find("Attack Sum Text");
        // attackSumText.GetComponent<Text>().enabled = true;
        // attackSumText.GetComponent<Text>().text = "= " + attack_damage;
        // Debug.Log(attackSumText.GetComponent<Text>().text);
        // attackSumText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        // attackSumText.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        // attackSumText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        // attackSumText.GetComponent<Text>().fontSize = 60;
        // attackSumText.transform.SetSiblingIndex(rolled_dice_list_.Count + 1);
        player_.animator_.SetTrigger("attack");
        yield return playerIcon.GetComponent<AttackEffect>().showEffect();
        monsters[0].getDamage(-player_.base_attack_);
        StartCoroutine(monsters_[0].ShowDamageText());

        // rolled_dice_list_.Reverse();
        foreach(Dice dice in rolled_dice_list_) {
            print("Rolling dice:" + dice.type_);
            yield return new WaitForSeconds(0.4f);
            dice.gameObject.GetComponent<Image>().enabled = false;
            GameObject roll_result_text_o = Instantiate(new GameObject(dice.gameObject.name + "_result"), dice.transform);
            Text roll_result_text = roll_result_text_o.AddComponent<Text>();
            
            roll_result_text_o.transform.parent = dice.gameObject.transform;
            roll_result_text_o.transform.position = dice.gameObject.transform.position;
            roll_result_text.text = dice.point_.ToString();
            roll_result_text.font = font_;
            roll_result_text.alignment = TextAnchor.MiddleCenter;
            roll_result_text.fontSize = 60;
            switch(dice.type_){
                case DiceType.odd:
                    roll_result_text.color = Color.blue;
                    break;
                case DiceType.even:
                    roll_result_text.color = Color.green;
                    break;
                case DiceType.cheat:
                    roll_result_text.color = Color.black;
                    break;
                default:
                    roll_result_text.color = Color.white;
                    break;

            }

            player_.animator_.SetTrigger("attack");
            yield return dice.gameObject.GetComponent<AttackEffect>().showEffect();
            monsters[0].getDamage(-dice.point_);
            StartCoroutine(monsters_[0].ShowDamageText());

        }
        yield return new WaitForSeconds(0.4f);
    }

    void CleanRollResult() {
        GameObject.Find("Player Icon").GetComponent<Image>().enabled = false;
        Destroy(GameObject.Find("Player Icon/Player Icon_result(Clone)"));
        // GameObject.Find("Attack Sum Text").GetComponent<Text>().enabled = false;
    }
}
