using UnityEngine;

namespace Game
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        public int scoreCount;
        public int recordCount;
        private void Awake()
        {
            instance = this;
            recordCount = PlayerPrefs.GetInt("RecordCount");
        }
    }
}

