using UnityEngine;
using System.Collections;
using GameSystem;
using GameSceneModel;

public class GameLayer : GameLayerBase {

    void Start() {
        base.Start();
    }

    void Update() {
        base.Update();
    }

    // ダイスの状態変更イベント
    protected void onDiceStateChanged(GameObject p_dice) { }
    // ダイス消えるイベント
    protected void onDestroyDice(GridPosition p_pos) { }
    // ダイス揃ったイベント
    protected void onSetDices() { } 
}
