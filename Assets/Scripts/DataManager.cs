using UnityEngine;

namespace Game
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        [HideInInspector] public int scoreCount;
        [HideInInspector] public int recordCount;
        [HideInInspector] public int activeSound;
        [HideInInspector] public bool checkPause;

        private void Awake()
        {
            instance = this;
            activeSound = PlayerPrefs.GetInt("ActiveSound");
            recordCount = PlayerPrefs.GetInt("RecordCount");
            checkPause = false;
        }
    }
}

