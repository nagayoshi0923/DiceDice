using UnityEngine;
using System.Collections;

namespace ResourceManager {

    // GameObjectのリソース管理クラス
    public class UObjectManager : object {

        static private UObjectManager _instance = null;
        private bool _isLoaded = false;

        static public void createInstance() {
            if(_instance == null) {
                _instance = new UObjectManager();
            }
        }

        static public UObjectManager Instance { get { 
            if(_instance == null) {
                createInstance();
            }
            return _instance; 
        } }

        private GameObject _dice = null;
        public GameObject Dice { get { return _dice; }}

        // リソース読み込み
        public void loadResources() {
            if(!this._isLoaded)
            _dice = Resources.Load("Prefab/Dice") as GameObject;
            this._isLoaded = true;
        }

        private UObjectManager() {

        }
    }
}
