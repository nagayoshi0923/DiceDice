using UnityEngine;
using System.Collections;
using GameSceneModel;
using htMessenger;
using ResourceManager;
using GameLayerInfoModel;

/**
 * タッチ制御クラス
 */
public class MainCamera : MonoBehaviour {

    private const int _FLICK_RANGE = 50;

    // タッチ開始座標
    private int _startX     = 0;
    private int _startY     = 0;
    //private int _nowX       = 0;
    //private int _nowY       = 0;
    // フリック方向
    private int _directionX = (int)DirectionKind.None;
    private int _directionY = (int)DirectionKind.None;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update ()  {

        if(Input.touchCount > 0) {
            Touch _touch = Input.GetTouch(0);

            // ダウン
            if(_touch.phase == TouchPhase.Began) {
                this.onTouchDownEvent(_touch.position);
            // ムーブ
            } else if(_touch.phase == TouchPhase.Moved) {
                this.onTouchMoveEvent(_touch.position);
            // アップ
            } else if(_touch.phase == TouchPhase.Ended) {
                this.onTouchUpEvent();
            }
        }

#region デバッグ
        if(Input.GetMouseButtonDown(0)) {
            Vector3 l_wVec = Input.mousePosition;
            l_wVec = camera.ScreenToWorldPoint(l_wVec);
            this.onTouchDownEvent(l_wVec);
        } else if(Input.GetMouseButtonUp(0)) {
            Vector3 l_wVec = Input.mousePosition;
            l_wVec = camera.ScreenToWorldPoint(l_wVec);
            this._onTouchMoveStub(l_wVec);
        }
    }

    private void _onTouchMoveStub(Vector3 p_position) {
        this._setFlickDirection(p_position);
        this.onTouchUpEvent();
    }
#endregion

    // フリック方向の設定
    private void _setFlickDirection(Vector3 p_position) {
        int l_rangeX = (int)p_position.x - this._startX;
        int l_rangeY = (int)p_position.z - this._startY;

        if(l_rangeX > _FLICK_RANGE) {
            this._directionX = (int)DirectionKind.RIGHT;
        } else if(l_rangeX < -(_FLICK_RANGE)) {
            this._directionX = (int)DirectionKind.LEFT;
        } else {
            this._directionX = 0;
        }

        if(l_rangeY > _FLICK_RANGE) {
            this._directionY = (int)DirectionKind.UP;
        } else if(l_rangeY < -(_FLICK_RANGE)) {
            this._directionY = (int)DirectionKind.DOWN;
        } else {
            this._directionY = 0;
        }
    }

    // ダウンイベント
    protected void onTouchDownEvent(Vector3 p_position) {
        this._startX    = (int)p_position.x;
        this._startY    = (int)p_position.z;
    }

    // ムーブイベント
    // TODO
    // _nowX, _nowY と _startX, _startY から
    // _directionX, _directionY を算出する
    // _directionの制限は行わず、イベントをはじくのは
    // onTouchUpEventで行う
    protected void onTouchMoveEvent(Vector3 p_position) {
    }

    // アップイベント
    protected void onTouchUpEvent() {
        if(this._directionX != (int)DirectionKind.None &&
           this._directionY != (int)DirectionKind.None) {
            return;
        } else if(this._directionX == (int)DirectionKind.None &&
                  this._directionY == (int)DirectionKind.None) {
            return;
        }

        FlickInfo l_fInfo = new FlickInfo();
        l_fInfo.StartX    = this._startX;
        l_fInfo.StartY    = this._startY;
        l_fInfo.Direction = ((this._directionX != (int)DirectionKind.None) ?
                            this._directionX : this._directionY);

        // メッセージ送信
        GameMessenger.Instance.SendMessage(l_fInfo);

        this._startX = 0;
        this._startY = 0;
        this._directionX = (int)DirectionKind.None;
        this._directionY = (int)DirectionKind.None;
    }
}
