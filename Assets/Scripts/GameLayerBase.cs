using UnityEngine;
using System.Collections;
using htMessenger;
using GameSceneModel;
using Utility;
using ResourceManager;
using GameLayerInfoModel;
using System.Collections.Generic;


namespace GameSystem {

    abstract public class GameLayerBase : MonoBehaviour {

        static private int _ROLL_SPEED    = 10;
        static private int _OFSET         = 10;
        static private int _HALF          = 90;
        static private int _DICE_SIZE     = 80;
        static private int _FALL_SPEED    = -10;
        static private int _DICE_CNT_Y    = GameLayerInfo._DICE_CNT_Y;
        static private int _DICE_CNT_X    = GameLayerInfo._DICE_CNT_X;

        private bool       _isRolling     = false;
        private GameObject _target        = null;
        private FlickInfo  _targetInfo_f  = null;
        private short      _animCount     = 0;
        private Vector3    _rollDirection;

        // 初期化
        protected void Start () {

            UObjectManager.createInstance();
            UObjectManager.Instance.loadResources();
     
            // ゲーム情報オブジェクトを初期化
            GameLayerInfo.createInstance();

            for(int j=0;j<_DICE_CNT_Y;j++) {
                for(int i=0;i<_DICE_CNT_X;i++) {
                    this._createDices( j+1, i+1);
                }
            }
        }

        // メインループ
        protected void Update () {

            if(this._isRolling) {
                this._onFlickDice(
                    this._target, this._targetInfo_f);
            } else {
                this._receiveMessage();
            }

            // ダイスを常に画面下につめる
            this._moveDice();

            // 特定キー
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._destroyDice(4, 3);
                this._createDices(4, 3);
            }
        }

        // ダイスの状態変更イベント
        protected void onDiceStateChanged(GameObject p_dice) {}
        // ダイス消えるイベント
        protected void onDestroyDice(GridPosition p_pos) {}
        // ダイス揃ったイベント
        protected void onSetDices() {}
        

        // ダイスを常に画面下につめる
        private void _moveDice() {

            int l_count = 0;
            // グリッドのY配列を取得
            GridPosition[] l_grids = 
                GameLayerInfo.Instance.getGridArrayY();

            for(int i=0;i<_DICE_CNT_Y;i++) {

                for(int j=0;j<_DICE_CNT_X;j++) {

                    // ダイスのX配列を取得
                    GameObject[] l_dices = 
                        GameLayerInfo.Instance.getDiceArrayX(i);

                    int l_diceY = (int)l_dices[j].transform.localPosition.z;

                    // ダイスの座標を確認
                    if(l_diceY > l_grids[i].Y) {
                        // グリッドの位置と一致しない場合は移動
                        l_dices[j].transform.position +=
                            new Vector3( 0, 0, _FALL_SPEED);
                    }
                }
            }
        }

        // ダイス削除
        private void _destroyDice(int p_posX, int p_posY) {

            // 削除するダイスを取得
            GameObject l_dice = 
                GameLayerInfo.Instance.Dices[p_posY - 1][p_posX - 1];
            GridPosition l_grids = 
                GameLayerInfo.Instance.Grids[p_posX - 1, p_posY - 1];

            // ダイスを削除
            GameLayerInfo.Instance.removeDice(l_grids);
            Destroy(l_dice);
        }

        // ダイス生成
        private void _createDices(int p_posX, int p_posY) {

            // 新しいダイスを生成
            GameObject l_dice =
                Instantiate(UObjectManager.Instance.Dice) as GameObject;

            // グリッド情報を取得
            GridPosition l_pos = 
                GameLayerInfo.Instance.Grids[p_posX - 1, p_posY - 1];

            // 一番上の行の一行上に生成
            l_dice.transform.position = new Vector3(
                    l_pos.X, 0, 60 + (_DICE_SIZE * _DICE_CNT_Y));
            l_dice.transform.localScale = new Vector3( 1, 1, 1);

            int l_rX = (int)(Random.value * 10) % 4;
            int l_rY = (int)(Random.value * 10) % 4;
            int l_rZ = (int)(Random.value * 10) % 4;

            // ダイスの向きを設定
            l_dice.transform.Rotate(
                    l_rX * _HALF, l_rY * _HALF, l_rZ * _HALF);

            // 追加したダイスをリストに追加
            GameLayerInfo.Instance.addDice(p_posY - 1, l_dice);
        }

        // メッセージ受信イベント
        private void _receiveMessage() {

            if(GameMessenger.Instance.Count > 0) {

                // メッセージ受信
                object l_msg  =
                    GameMessenger.Instance.RecvMessage(); 
                FlickInfo l_fInfo = l_msg as FlickInfo;

                if(l_fInfo != null) {
                    // フリック開始位置からダイスを取得
                    GameObject l_dice = 
                        GameLayerInfo.Instance.getDiceAt(
                                l_fInfo.StartX, l_fInfo.StartY);

                    if(l_dice != null) {
                        // 回転フラグ立てる
                        this._isRolling    = true;
                        this._target       = l_dice;
                        this._targetInfo_f = l_fInfo;
                    }
                }
            }
        }

        // さいころ回転
        private void _onFlickDice(GameObject p_dice, FlickInfo p_info) {

            if(this._animCount < (_HALF / _ROLL_SPEED)) {

                if(this._animCount == 0 ) {

                    int l_y = this._roundAngle(
                            p_dice.transform.localEulerAngles.y);
                    int l_x = this._roundAngle(
                            p_dice.transform.localEulerAngles.x);
                    int l_z = this._roundAngle(
                            p_dice.transform.localEulerAngles.z);

                    int Direction = DiceUtility.getDiceDirection(l_y);
                    int State = DiceUtility.getDiceState(l_x, l_z);

                    // 回転する方向を取得
                    this._rollDirection = DiceUtility.getRotateVector(
                        State, Direction, p_info.Direction);
                }
                
                p_dice.transform.Rotate(
                        this._rollDirection.x,
                        this._rollDirection.y,
                        this._rollDirection.z);

                this._animCount++;
            } else {

                this._isRolling = false;
                this._animCount = 0;
                this._rollDirection.x = 0;
                this._rollDirection.y = 0;
                this._rollDirection.z = 0;

                this.onDiceStateChanged(p_dice);
            }
        }

        // 値を丸め込む
        private int _roundAngle(float p_angle) {
            return (int)System.Math.Round(
                    p_angle, System.MidpointRounding.AwayFromZero);
        }
    }

}
