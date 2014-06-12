using UnityEngine;
using System.Collections;
using GameSceneModel;
using System.Collections.Generic;

namespace GameLayerInfoModel {

    public class GameLayerInfo : object {

        // ダイスの数
        public static int _DICE_CNT_Y = 10;
        public static int _DICE_CNT_X = 6;
        // ダイスサイズ
        private static int _DICE_SIZE = 80;
        // X,Yのオフセット
        private static int _OFSET_X   = 70;
        private static int _OFSET_Y   = 60;
        private static int _G_OFSET   = 20;

        static private GameLayerInfo _instance = null;

        // グリッド情報
        // [y, x]
        private GridPosition[,] _gridList;
        // ダイス情報
        // 配列がx リストがy;
        private List<DiceInfo>[] _diceInfo;
        // ダイスオブジェクト
        // 配列がx リストがy
        private List<GameObject>[] _diceList;

        // インスタンス生成
        public static void createInstance(){
            if(_instance == null) {
                _instance = new GameLayerInfo();
            }
        }

        // インスタンス取得
        public static GameLayerInfo Instance {
            get{ return _instance; } }

        // 座標からダイス情報を取得
        public DiceInfo getDiceInfoAt(int p_x, int p_y) {

            int l_x = (p_x - _G_OFSET) / _DICE_SIZE;
            int l_y = (p_y - _G_OFSET) / _DICE_SIZE;

            return this._diceInfo[l_x][l_y];
        }

        // 座標からダイスを取得
        public GameObject getDiceAt(int p_x, int p_y) {

            int l_x = (p_x - _G_OFSET) / _DICE_SIZE;
            int l_y = (p_y - _G_OFSET) / _DICE_SIZE;

            if(l_x > _DICE_CNT_X || l_y > _DICE_CNT_Y) {
                return null;
            }

            return this._diceList[l_x][l_y];
        }

        // グリッドポジションからダイスを削除
        public void removeDice(GridPosition p_grid) {

            int l_x = (p_grid.X - _G_OFSET) / _DICE_SIZE;
            int l_y = (p_grid.Y - _G_OFSET) / _DICE_SIZE;

            this._diceList[l_x].RemoveAt(l_y);
        }

        // ダイスを追加
        public void addDice(
                int p_x, GameObject p_dice) {

            this._diceList[p_x].Add(p_dice);
        }

        public List<GameObject>[] Dices { get { return this._diceList; } }

        // コンストラクタ
        private GameLayerInfo() {

            this._gridList = new GridPosition[_DICE_CNT_Y, _DICE_CNT_X];
            this._diceInfo = new List<DiceInfo>[_DICE_CNT_X];
            this._diceList = new List<GameObject>[_DICE_CNT_X];

            for(int i=0;i<_DICE_CNT_X;i++) {

                // リストを初期化
                this._diceInfo[i] = new List<DiceInfo>();
                this._diceList[i] = new List<GameObject>();

                // グリッドの初期化
                for(int j=0;j<_DICE_CNT_Y;j++) {
                    this._gridList[j,i] = new GridPosition(
                            _OFSET_X + (_DICE_SIZE * i),
                            _OFSET_Y + (_DICE_SIZE * j));
                }
            }
        }

        // グリッドのリストを取得
        public GridPosition[,] Grids { 
            get { return this._gridList; }
        }

        // グリッドのY配列を取得
        public GridPosition[] getGridArrayY() {
            GridPosition[] l_grids = new GridPosition[_DICE_CNT_Y];

            for(int i=0;i<_DICE_CNT_Y;i++) {
                l_grids[i] = this._gridList[i, 0];
            }

            return l_grids;
        }

        // ダイスのX配列を取得
        public GameObject[] getDiceArrayX(int p_y) {
            GameObject[] l_dices = new GameObject[_DICE_CNT_X];

            for(int i=0;i<_DICE_CNT_X;i++) {
                l_dices[i] = this._diceList[i][p_y];
            }

            return l_dices;
        }

    }

}
