using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _getReady;
        [SerializeField] private GameObject _gameOver;
        [SerializeField] private GameObject[] _textsInfo;
        [SerializeField] private GameObject _btnPlayPause;
        [SerializeField] private GameObject _btnStopPause;
        [SerializeField] private GameObject _time;
        [SerializeField] private Image _imageSound;
        [SerializeField] private Sprite[] _spriteSound;
        [SerializeField] private Text _textScore;
        [SerializeField] private Text _textRecord;
        [SerializeField] private Text _textGameOverScore;
        [SerializeField] private Text _textGameOverRecord;
        [SerializeField] private Text _textTime;

        private void Start()
        {
            instance = this;
            _startPanel.SetActive(true);
            _getReady.SetActive(false);
            _gameOver.SetActive(false);
            _btnPlayPause.SetActive(false);
            _btnStopPause.SetActive(false);
            _time.SetActive(false);
            foreach (GameObject item in _textsInfo)
                item.SetActive(false);
            if (DataManager.instance.activeSound == 0)
            {
                _imageSound.sprite = _spriteSound[0];
            }
            else if (DataManager.instance.activeSound == 1)
            {
                _imageSound.sprite = _spriteSound[1];
            }
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
            _btnPlayPause.SetActive(true);
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
            _textGameOverRecord.text = DataManager.instance.recordCount.ToString();
            foreach (GameObject item in _textsInfo)
                item.SetActive(false);
        }

        public void Back()
        {
            _startPanel.SetActive(true);
            _gameOver.SetActive(false);
            Player.instance.RestartPlayer();
        }

        public void PlayPause()
        {
            Time.timeScale = 0f;
            _btnPlayPause.SetActive(false);
            _btnStopPause.SetActive(true);
            DataManager.instance.checkPause = true;

        }

        public void StopPause()
        {
            Time.timeScale = 1f;
            _btnStopPause.SetActive(false);
            _time.SetActive(true);
            StartCoroutine(TimeFlow());
        }

        private IEnumerator TimeFlow()
        {
            _textTime.text = "3";
            yield return new WaitForSeconds(1);
            _textTime.text = "2";
            yield return new WaitForSeconds(1);
            _textTime.text = "1";
            yield return new WaitForSeconds(1);
            _btnPlayPause.SetActive(true);
            _time.SetActive(false);
            DataManager.instance.checkPause = false;
            StopCoroutine(TimeFlow());
        }

        public void ActiveSound()
        {
            DataManager.instance.activeSound = DataManager.instance.activeSound == 0 ? 1 : 0;
            PlayerPrefs.SetInt("ActiveSound", DataManager.instance.activeSound);
            if (DataManager.instance.activeSound == 0)
            {
                _imageSound.sprite = _spriteSound[0];
            }
            else if (DataManager.instance.activeSound == 1)
            {
                _imageSound.sprite = _spriteSound[1];
            }
        }
    }
}

