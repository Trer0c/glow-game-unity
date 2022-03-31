using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _getReady;
        [SerializeField] private GameObject _gameOver;
        [SerializeField] private GameObject[] _textsInfo;
        [SerializeField] private Text _textScore;
        [SerializeField] private Text _textRecord;
        [SerializeField] private Text _textGameOverScore;
        [SerializeField] private Text _textGameOverRecord;

        private void Start()
        {
            instance = this;
            _startPanel.SetActive(true);
            _getReady.SetActive(false);
            _gameOver.SetActive(false);
            foreach (GameObject item in _textsInfo)
                item.SetActive(false);
            Time.timeScale = 0f;
        }

        public void UpdateTexts()
        {
            _textScore.text = "Score: " + DataManager.instance.scoreCount.ToString();
            _textRecord.text = "Record: " + DataManager.instance.recordCount.ToString();
        }

        public void PlayGame()
        {
            _startPanel.SetActive(false);
            _getReady.SetActive(true);
            _gameOver.SetActive(false);
            DataManager.instance.scoreCount = 0;
            Player.instance.RestartPlayer();
        }

        public void GetReady()
        {
            _getReady.SetActive(false);
            Time.timeScale = 1f;
            UpdateTexts();
            Player.instance.gameOverCheck = false;
            Player.instance.SwitchColorWalls();
            foreach (GameObject item in _textsInfo)
                item.SetActive(true);
        }

        public void GameOver()
        {
            Time.timeScale = 0f;
            _gameOver.SetActive(true);
            _textGameOverScore.text = DataManager.instance.scoreCount.ToString();
            _textGameOverScore.text = DataManager.instance.recordCount.ToString();
            foreach (GameObject item in _textsInfo)
                item.SetActive(false);
        }
    }
}

