using UnityEngine;
using System.Collections;

#region 共通モデルオブジェクト
namespace GameSceneModel {

    // フリック向き
    enum DirectionKind {
        None = 0, UP, LEFT, DOWN, RIGHT
    };
    // ダイスの状態
    enum DiceState {
        None = 0, ONE, TWO, THREE, FOUR, FIVE, SIX
    }

    /**
     * フリック情報
     */
    public class FlickInfo : object{
        // メンバ
        private int _startX;
        private int _startY;
        private int _direction;

        // フリック開始X
        public int StartX {
            set { this._startX = value; }
            get { return this._startX;  }
        }
        // フリック開始Y
        public int StartY {
            set { this._startY = value; }
            get { return this._startY;  }
        }
        // フリック方向
        public int Direction {
            set {
                if(value < (int)DirectionKind.None || 
                   value > (int)DirectionKind.RIGHT) {
                    this._direction = (int)DirectionKind.None;
                } else {
                    this._direction = value;
                }
            }
            get { return this._direction;  }
        }
        // コンストラクタ
        public FlickInfo() {
            this._startX    = 0;
            this._startY    = 0;
            this._direction = (int)DirectionKind.None;
        }
    }


    /**
     * ダイス情報
     */
    public class DiceInfo : object{

        // ダイスの向き
        private int _state = (int)DiceState.None;
        public int State {

            set { 
                if(value >= (int)DiceState.None) return;
                if(value <  (int)DiceState.ONE)  return;

                this._state = value;
            } get {
                return this._state;
            }
        }

        // ダイスオブジェクト本体
        private GameObject _obj;
        public GameObject Object {
            get {
                return this._obj;
            }
        }

        // ダイス位置
        private GridPosition _pos;
        public GridPosition GridPos {
            get {
                return this._pos;
            } set {
                if(value != null) {
                    this._pos = value;
                }
            }
        }

        // ダイスの状態を設定
        public int changeDiceState(
                int p_dir, Vector3 p_vecter) {
            return this._state;
        }

        // コンストラクタ
        public DiceInfo (GameObject p_obj, GridPosition p_pos) {
            this._obj   = p_obj;
            this._pos   = p_pos;
        }
        public DiceInfo (GameObject p_obj) {
            this._obj   = p_obj;
        }
        public DiceInfo () { }
    }

    /**
     * グリッド位置
     */
    public class GridPosition {

        private int _x = 0;
        public int X {
            set{ this._x = value; }
            get{ return this._x; }
        }

        private int _y = 0;
        public int Y {
            set { this._y = value; }
            get { return this._y; }
        }

        public void Update(int p_x, int p_y) {
            this._x = p_x;
            this._y = p_y;
        }

        // コンストラクタ
        public GridPosition(int p_x, int p_y) {
            this._x = p_x;
            this._y = p_y;
        }
    }
}

#endregion
