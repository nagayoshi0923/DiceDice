using UnityEngine;
using System.Collections;

namespace htMessenger {

    /**
     * メッセージ受け渡し基底クラス
     */
    abstract public class Messenger : object {

        private Queue _que;

        // メッセージ送信
        public void SendMessage(object p_obj) {
            this._que.Enqueue(p_obj);
        }

        // メッセージ受信
        public object RecvMessage() {
            return this._que.Dequeue();
        }

        // メッセージ数取得
        public int Count { get{ return this._que.Count; } }

        public Messenger() {
            this._que = new Queue();
        }

    }

    /**
     * ゲーム情報受け渡しクラス
     */
    public class GameMessenger : Messenger {

        private static GameMessenger _instance = new GameMessenger();

        public static GameMessenger Instance { get{ return _instance; } }
    }
}
