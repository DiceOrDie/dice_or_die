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
    kLosing, 
    kWinning
};


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject backpack_go_;
    public GameObject hands_go_;
    public GameObject result_bar_;
    public LevelManagement level_manger_;
    public List<GameObject> monsters_gameobject_;
    public GameObject player_gameobject_;
    public Text state_text_;

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
        level_manger_.NextLevel();
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
            case GameState.kRoomEnd:
                yield return RoomEnd();
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
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
        Debug.Log("GameStart() finished.");
        state.game_state = GameState.kRoomStart;
        yield return null;
    }
    IEnumerator RoomStart() {
        Debug.Log("RoomStart() started.");
        PlayerRoomInitialize();
        MonsterRoomInitialize();
        room_tmp_fish_ = 0;
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
        Debug.Log("RoomStart() finished.");
        state.game_state = GameState.kRoundStart;
        yield return null;
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
        backpack_.StartDraw();
        yield return new WaitUntil(() => backpack_.is_draw_on_going_ == false);
        foreach (Skill_base skill in player_.skill_list)
        {
            Debug.Log("技能檢測");
            yield return skill.Effect(state);
        }
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
        DisplayAttackSummation(attack_damage);
        // yield return new WaitForSeconds(2);

        if(!attack_damage.Equals("0"))
            yield return monsters_[0].ShowDamageText();
        
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
            Debug.Log("RoundEnd() monster");
            if (!monster.IsAlive()) {
                Debug.Log("RoundEnd() monster die");
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
        Debug.Log("當前掉落小魚乾數量：" + room_tmp_fish_.ToString());
        Debug.Log("RoundEnd() finished.");
        yield return null;
    }
    IEnumerator RoomEnd() {
        Debug.Log("Room End");

        if(player_.IsAlive())
        {
            int now_Scene = SceneManager.GetActiveScene().buildIndex;
            print(now_Scene);
            if(now_Scene != 10)
            {
                level_manger_.NextLevel();
                SceneManager.LoadScene(now_Scene + 1);
                state.game_state = GameState.kRoomStart;
            }
            player_.GetFish(room_tmp_fish_);
        }
        else
            gameover_flag_ = true;
        room_tmp_fish_ = 0;
        Debug.Log("當前身上的小魚乾數量：" + player_.GetFishNum().ToString());
        yield return null;
    }
    
    void PlayerGameInitialize() {
        player_.CharacterInit();
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
            if(!player_.IsAlive())
                state_text_.text = "You Died.";
            state_text_.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            state_text_.gameObject.SetActive(false);
            break;
        }

    }

    public void Pause() {
        Time.timeScale = 0;
    }
    public void Resume() {
        Time.timeScale = 1;
    }

    void DisplayAttackSummation(string attack_damage) {
        // foreach (Dice dice in rolled_dice_list_) {
        //     dice.gameObject.SetActive(false);
        // }

        GameObject playerIcon = GameObject.Find("Player Icon");
        playerIcon.GetComponent<Image>().enabled = true;
        playerIcon.GetComponent<Image>().sprite = GameObject.Find("Player/Sprite").GetComponent<SpriteRenderer>().sprite;

        GameObject attackSumText = GameObject.Find("Attack Sum Text");
        attackSumText.GetComponent<Text>().enabled = true;
        attackSumText.GetComponent<Text>().text = "= " + attack_damage;
        Debug.Log(attackSumText.GetComponent<Text>().text);
        attackSumText.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        attackSumText.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
        attackSumText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        attackSumText.GetComponent<Text>().fontSize = 60;
        attackSumText.transform.SetSiblingIndex(rolled_dice_list_.Count + 1);

        return;
    }

    void CleanRollResult() {
        GameObject.Find("Player Icon").GetComponent<Image>().enabled = false;
        GameObject.Find("Attack Sum Text").GetComponent<Text>().enabled = false;
    }
}
