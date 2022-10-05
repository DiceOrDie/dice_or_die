using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    playerDraw,
    playerRoll,
    opponent
}

public class BattleManager : MonoBehaviour
{
    public BattleState state;

    // private bool battleEnd;
    // private Backpack backpack = Backpack.getInstance();

    void Start()
    {
        // battleEnd = false;

        // Dice dice1, dice2;

        //while (!battleEnd)
        {
            playerDraw();
            // playerRoll();
            // opponentAtk();
        }//while
    }//start

    void playerDraw()
    {
        Debug.Log("Battle state: player draw");
    }//playerDraw
}
