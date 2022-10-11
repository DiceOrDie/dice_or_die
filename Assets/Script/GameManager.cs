// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject backpack_go_;
    public GameObject hands_go_;
    public GameObject player_gameobject_;
    public List<GameObject> monsters_gameobject_;
    

    static public GameManager instance;

    public enum GameState {
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
    private GameState game_state_;
    private bool flag_is_room_end_;
    private Character character_;
    private Backpack backpack_;
    private Hands hands_;

    public Character character{
        get { return character_; }
    }

    private List<Monster> monsters_;
    private List<Dice> rolled_dice_list_;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        game_state_ = GameState.kRoomStart;
        StartCoroutine(GameRoutine());
    }
    void Start() {
        backpack_ = backpack_go_.GetComponent<Backpack>();
        hands_ = hands_go_.GetComponent<Hands>();
        character_ = player_gameobject_.GetComponent<Character>();
        foreach(GameObject monster_gameobject in monsters_gameobject_) {
            monsters_.Add(monster_gameobject.GetComponent<Monster>());
        }
        RoomStart();
    }
    IEnumerator GameRoutine() {
        while (!flag_is_room_end_) {
            switch (game_state_) {
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
            default:
                Debug.Log("Error in GameManager/GameRoutine()/switch-case");
                break;
            }
        }
        game_state_ = GameState.kRoomEnd;
    }
    
    IEnumerator RoomStart() {
        Debug.Log("RoomStart() started.");
        PlayerRoomInitialize();
        MonsterRoomInitialize();
        Debug.Log("RoomStart() finished.");
        game_state_ = GameState.kRoundStart;
        yield return null;
    }
    
    IEnumerator RoundStart() {
        // TODO
        Debug.Log("RoundStart() started.");
        Debug.Log("RoundStart() finished.");
        game_state_ = GameState.kPlayerDrawDice;
        yield return null;
    }

    IEnumerator PlayerDrawDice() {
        // TODO: turn on backpack UI
        Debug.Log("PlayerDrawDice() started.");
        backpack_.StartDraw();
        yield return new WaitUntil(() => backpack_.is_draw_on_going_ == false);
        Debug.Log("PlayerDrawDice() finished.");
        game_state_ = GameState.kPlayerSelectDice;
        //yield return null;
    }

    IEnumerator PlayerSelectDice() {
        // TODO
        Debug.Log("PlayerSelectDice() started.");
        hands_.StartSelect();
        yield return new WaitUntil(() => hands_.is_select_on_going == false);
        rolled_dice_list_ = hands_.GetSelectedDice();
        Debug.Log("PlayerSelectDice() finished.");
        game_state_ = GameState.kPlayerRollDice;
        // yield return null;
    }

    IEnumerator PlayerRollDice() {
        // TODO: roll all dice in rolled_dice_list_
        Debug.Log("PlayerRollDice() started.");
        foreach (Dice dice in rolled_dice_list_) {
            Debug.Log(dice.RollDice());
        }
        Debug.Log("PlayerRollDice() finished.");
        game_state_ = GameState.kPlayerAttack;
        yield return null;
    }

    IEnumerator PlayerAttack() {
        Debug.Log("PlayerAttack() started.");
        Debug.Log("Attack: " + character_.Attack(rolled_dice_list_, monsters_));
        foreach (Monster monster in monsters_) {
            if (!monster.IsAlive()) {
                monsters_.Remove(monster);
                Destroy(monster.gameObject);
            }
        }
        isGameEnd();
        Debug.Log("PlayerAttack() finished.");
        game_state_ = GameState.kMonsterAttack;
        yield return null;
    }

    IEnumerator MonsterAttack() {
        Debug.Log("MonsterAttack() started.");
        foreach (Monster monster in monsters_) {
            Debug.Log("Monster Attack: " + monster.Attack(character_));
        }
        isGameEnd();
        Debug.Log("MonsterAttack() finished.");
        game_state_ = GameState.kRoundEnd;
        yield return null;
    }
    
    IEnumerator RoundEnd() {
        Debug.Log("RoundEnd() started.");
        character_.UpdateState();
        foreach (Monster monster in monsters_) {
            monster.UpdateState();
        }
        isGameEnd();
        Debug.Log("RoundEnd() finished.");
        // TODO: RoundStart
        if (!flag_is_room_end_) {
            game_state_ = GameState.kRoundStart;
        }
        yield return null;
    }
    
    void PlayerRoomInitialize() {}
    
    void MonsterRoomInitialize() {}
    
    void isGameEnd() {
        if (!character_.IsAlive()) {
            flag_is_room_end_ = true;
        }
        bool all_monster_die = true;
        foreach (Monster monster in monsters_) {
            if (monster.IsAlive()) {
                all_monster_die = false;
                break;
            }
        }
        if (all_monster_die) {
            flag_is_room_end_ = true;
        }
    }
}
