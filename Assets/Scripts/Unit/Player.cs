using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit 
{

    public void Move(Vector2Int vector2)
    {
        pos = vector2;
        GameManager.Instance.gameStateFSM.SwitchToState(-1);
        transform.DOMove(new Vector3(vector2.x+0.5f, vector2.y+0.75f, 0),0.3f).OnComplete(() => 
        { 
            GameManager.Instance.gameStateFSM.SwitchToState(1);
            GameManager.Instance.NextTurn();
        });
    }

}
