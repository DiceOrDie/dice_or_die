// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject backpack_go_;
    
    

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
        backpack_ = backpack_go_.GetComponentInChildren<Backpack>();
    }
    IEnumerator GameRoutine() {
        while (!flag_is_room_end_) {
            switch (game_state_) {
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
            default:
                Debug.Log("Error in GameManager/GameRoutine()/switch-case");
                break;
            }
        }
        game_state_ = GameState.kRoomEnd;
    }
    
    IEnumerator RoomStart() {
        PlayerRoomInitialize();
        MonsterRoomInitialize();
        game_state_ = GameState.kRoundStart;
        yield return null;
    }
    
    IEnumerator RoundStart() {
        // TODO
        game_state_ = GameState.kPlayerDrawDice;
        yield return null;
    }

    IEnumerator PlayerDrawDice() {
        // TODO: turn on backpack UI
        game_state_ = GameState.kPlayerSelectDice;
        yield return null;
    }

    IEnumerator PlayerSelectDice() {
        // TODO
        game_state_ = GameState.kPlayerRollDice;
        yield return null;
    }

    IEnumerator PlayerRollDice() {
        // TODO
        game_state_ = GameState.kPlayerAttack;
        yield return null;
    }

    IEnumerator PlayerAttack() {
        character_.Attack(rolled_dice_list_, monsters_);
        foreach (Monster monster in monsters_) {
            if (!monster.IsAlive()) {
                monsters_.Remove(monster);
            }
        }
        JudgeGameEnd();
        game_state_ = GameState.kMonsterAttack;
        yield return null;
    }

    IEnumerator MonsterAttack() {
        foreach (Monster monster in monsters_) {
            monster.Attack(character_);
        }
        JudgeGameEnd();
        game_state_ = GameState.kRoundEnd;
        yield return null;
    }
    
    IEnumerator RoundEnd() {
        character_.UpdateState();
        foreach (Monster monster in monsters_) {
            monster.UpdateState();
        }
        JudgeGameEnd();
        // TODO: RoundStart
        if (!flag_is_room_end_) {
            game_state_ = GameState.kRoundStart;
        }
        yield return null;
    }
    
    void PlayerRoomInitialize() {}
    
    void MonsterRoomInitialize() {}
    
    void JudgeGameEnd() {
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
