using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public static Player instance;
        public bool gameOverCheck;
        private AudioSource audioSorce;
        [SerializeField] private Image[] _imageWalls;
        [SerializeField] private Sprite[] _spritePlayer;
        [SerializeField] private Sprite[] _spriteWalls;
        [SerializeField] private Image _imagePlayer;
        [SerializeField] private AudioClip switchSound;
        [SerializeField] private AudioClip deadSound;
        [SerializeField] private AudioClip trigerSound;
        private int[] _indexColorWalls = new int[] { 0, 1 };
        private int _indexColorPlayer = 0;
        private float _speed = 4;
        private Transform _player;
        private Vector2 _direction;


        private void Start()
        {
            instance = this;
            audioSorce = this.GetComponent<AudioSource>();
            _player = transform;
            _direction = new Vector2(10, 0);
        }

        public void RestartPlayer()
        {
            _player.localPosition = new Vector3(0, 0, 0);
            _imagePlayer.sprite = _spritePlayer[0];
            _indexColorPlayer = 0;
            _indexColorWalls = new int[] { 0, 1 };
            _imageWalls[0].sprite = _spriteWalls[0];
            _imageWalls[1].sprite = _spriteWalls[8];
            _direction = new Vector2(10, 0);
        }

        private void FixedUpdate()
        {
            if (!gameOverCheck)
            {
                _player.Translate(_direction * _speed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.name == "WallLeft")
            {
                if (_indexColorWalls[0] != _indexColorPlayer)
                {
                    gameOverCheck = true;
                    audioSorce.PlayOneShot(deadSound);
                    Taptic.Heavy();
                    UIManager.instance.GameOver();
                    return;
                }
                int random = UnityEngine.Random.Range(0, 2);
                _indexColorPlayer = random;
                _imagePlayer.sprite = _indexColorPlayer == 0 ? _spritePlayer[0] : _spritePlayer[1];
                _direction = new Vector2(10f, 0);
            }
            else if (other.transform.name == "WallRight")
            {
                if (_indexColorWalls[1] != _indexColorPlayer)
                {
                    gameOverCheck = true;
                    audioSorce.PlayOneShot(deadSound);
                    Taptic.Heavy();
                    UIManager.instance.GameOver();
                    return;
                }
                int random = UnityEngine.Random.Range(0, 2);
                _indexColorPlayer = random;
                _imagePlayer.sprite = _indexColorPlayer == 0 ? _spritePlayer[0] : _spritePlayer[1];
                _direction = new Vector2(-10f, 0);
            }
            DataManager.instance.scoreCount += 1;
            if (DataManager.instance.scoreCount >= DataManager.instance.recordCount)
            {
                DataManager.instance.recordCount = DataManager.instance.scoreCount;
                PlayerPrefs.SetInt("RecordCount", DataManager.instance.recordCount);
            }
            UIManager.instance.UpdateTexts();
            UpdateSpeed();
            audioSorce.PlayOneShot(trigerSound);
            Taptic.Light();
        }

        private void UpdateSpeed()
        {
            if (DataManager.instance.scoreCount >= 0 && DataManager.instance.scoreCount <= 5)
            {
                _speed += 0.1f;
            }
            else if (DataManager.instance.scoreCount > 10 && DataManager.instance.scoreCount <= 15)
            {
                _speed += 0.1f;
            }
            else if (DataManager.instance.scoreCount > 20 && DataManager.instance.scoreCount <= 25)
            {
                _speed += 0.1f;
            }
            else if (DataManager.instance.scoreCount > 30 && DataManager.instance.scoreCount <= 35)
            {
                _speed += 0.1f;
            }
            _speed = (Convert.ToSingle(Math.Round(_speed, 1)));
        }

        public void SwitchColorWalls()
        {
            if (!gameOverCheck)
            {
                _indexColorWalls[0] = _indexColorWalls[0] == 0 ? 1 : 0;
                _indexColorWalls[1] = _indexColorWalls[1] == 0 ? 1 : 0;
                StartCoroutine(Animation());
                audioSorce.PlayOneShot(switchSound);
            }
        }

        private IEnumerator Animation()
        {
            int count = 9;
            while (count > 0)
            {
                count--;
                for (int i = 0; i < 2; i++)
                {
                    if (_indexColorWalls[i] == 1)
                    {
                        if (_imageWalls[i].sprite == _spriteWalls[0])
                        {
                            _imageWalls[i].sprite = _spriteWalls[1];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[1])
                        {
                            _imageWalls[i].sprite = _spriteWalls[2];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[2])
                        {
                            _imageWalls[i].sprite = _spriteWalls[3];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[3])
                        {
                            _imageWalls[i].sprite = _spriteWalls[4];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[4])
                        {
                            _imageWalls[i].sprite = _spriteWalls[5];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[5])
                        {
                            _imageWalls[i].sprite = _spriteWalls[6];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[6])
                        {
                            _imageWalls[i].sprite = _spriteWalls[7];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[7])
                        {
                            _imageWalls[i].sprite = _spriteWalls[8];
                        }

                    }
                    else if (_indexColorWalls[i] == 0)
                    {
                        if (_imageWalls[i].sprite == _spriteWalls[8])
                        {
                            _imageWalls[i].sprite = _spriteWalls[7];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[7])
                        {
                            _imageWalls[i].sprite = _spriteWalls[6];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[6])
                        {
                            _imageWalls[i].sprite = _spriteWalls[5];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[5])
                        {
                            _imageWalls[i].sprite = _spriteWalls[4];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[4])
                        {
                            _imageWalls[i].sprite = _spriteWalls[3];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[4])
                        {
                            _imageWalls[i].sprite = _spriteWalls[3];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[3])
                        {
                            _imageWalls[i].sprite = _spriteWalls[2];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[2])
                        {
                            _imageWalls[i].sprite = _spriteWalls[1];
                        }
                        else if (_imageWalls[i].sprite == _spriteWalls[1])
                        {
                            _imageWalls[i].sprite = _spriteWalls[0];
                        }
                    }
                }
                yield return new WaitForSeconds(0.02f);
            }
            StopCoroutine(Animation());
        }
    }
}
