using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Image[] _imageWalls;
    [SerializeField] private Sprite[] _spritePlayer;
    [SerializeField] private Sprite[] _spriteWalls;
    [SerializeField] private Image _imagePlayer;
    [SerializeField] private Text _textScore;
    [SerializeField] private Text _textRecord;
    [SerializeField] private GameObject _btnRestart;
    private int[] _indexColorWalls = new int[] { 1, 0 };
    private int _indexColorPlayer = 0;
    private int _score = 0;
    private int _record = 0;
    private float _speed = 4;
    private bool start;
    private bool up;
    private bool bottom;

    Vector2 direction;

    private void Start()
    {
        direction = new Vector2(10, 0);
        start = true;
        _btnRestart.SetActive(false);
        _record = PlayerPrefs.GetInt("Record");
        _textScore.text = "Score: " + _score.ToString();
        _textRecord.text = "Record: " + _record.ToString();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name == "WallLeft")
        {
            if (_indexColorWalls[0] != _indexColorPlayer)
            {
                Time.timeScale = 0f;
                _btnRestart.SetActive(true);
                return;
            }
            int random = Random.Range(0, 2);
            _indexColorPlayer = random;
            _imagePlayer.sprite = _indexColorPlayer == 0 ? _spritePlayer[0] : _spritePlayer[1];
            direction = new Vector2(10f, 0);
            _score += 1;
            _textScore.text = "Score: " + _score.ToString();
            if (_score >= _record)
            {
                _record = _score;
                PlayerPrefs.SetInt("Record", _record);
                _textRecord.text = "Record: " + _record.ToString();
            }
            if (start)
            {
                _speed += 0.04f;
                if (_speed >= 5)
                {
                    start = false;
                    up = true;
                }
            }
            else if (up)
            {
                _speed += 0.04f;
                if (_speed >= 6)
                {
                    up = false;
                    bottom = true;
                }
            }
            else if (bottom)
            {
                _speed -= 0.04f;
                if (_speed <= 5)
                {
                    up = true;
                    bottom = false;
                }
            }
        }
        else if (other.transform.name == "WallRight")
        {
            if (_indexColorWalls[1] != _indexColorPlayer)
            {
                Time.timeScale = 0f;
                _btnRestart.SetActive(true);
                return;
            }
            int random = Random.Range(0, 2);
            _indexColorPlayer = random;
            _imagePlayer.sprite = _indexColorPlayer == 0 ? _spritePlayer[0] : _spritePlayer[1];
            direction = new Vector2(-10f, 0);
            _score += 1;
            _textScore.text = "Score: " + _score.ToString();
            if (_score >= _record)
            {
                _record = _score;
                PlayerPrefs.SetInt("Record", _record);
                _textRecord.text = "Record: " + _record.ToString();
            }
            if (start)
            {
                _speed += 0.04f;
                if (_speed >= 5)
                {
                    start = false;
                    up = true;
                }
            }
            else if (up)
            {
                _speed += 0.04f;
                if (_speed >= 6)
                {
                    up = false;
                    bottom = true;
                }
            }
            else if (bottom)
            {
                _speed -= 0.04f;
                if (_speed <= 5)
                {
                    up = true;
                    bottom = false;
                }
            }
        }
        print(_speed);
    }

    public void SwitchColorWalls()
    {
        _indexColorWalls[0] = _indexColorWalls[0] == 0 ? 1 : 0;
        _indexColorWalls[1] = _indexColorWalls[1] == 0 ? 1 : 0;
        for (int i = 0; i < 2; i++)
        {
            if (_indexColorWalls[i] == 0)
                _imageWalls[i].sprite = _spriteWalls[0];
            else if (_indexColorWalls[i] == 1)
                _imageWalls[i].sprite = _spriteWalls[1];
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
