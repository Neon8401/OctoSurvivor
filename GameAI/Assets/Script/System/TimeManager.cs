//日本語対応
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Charadata;

public class TimeManager : MonoBehaviour
{
    public bool TimeUp => _timeUp;
    public bool Gimic => _gimic;

    public bool Warming => _warming;
    [Header("制限時間")]
    [SerializeField] private float _timeLimit;
    [SerializeField] private Text _timeText;
    [SerializeField] private float _nearTimeLimit = 10.0f;
    [SerializeField] Charadata _charadata;
    MainSceneController _mainSceneController;

    private float _currentTime;
    private float _currentUpTime;
    public float Score;
    private bool _timeUp = false;
    private bool _isStart = false;
    private bool _gimic = false;
    private bool _warming =false;

    private bool _isNearTimeLimitFlag = false;

    void Start()
    {
        _timeText.text = _timeLimit.ToString("0");
        _currentTime = _timeLimit;
    }

    public void SetUp() 
    {
        _isStart = true;
        _gimic = false;
        _warming = false;

    }

    void Update()
    {
        _gimic = false;
        if (_isStart && !_timeUp)
        {
            _currentTime -= Time.deltaTime;
            _timeText.text = _currentTime.ToString("0");
            _currentUpTime += Time.deltaTime;
            Score += _currentUpTime;

            if (_currentTime < 0)
            {
                _timeUp = true;
                MainSceneController.Instance.GameOver();
            }
            else if(_currentUpTime > 10f)
            {
                AudioManager.instance.PlaySE("Warning");
                Debug.Log("OK9");
                _charadata.ATK += 1;
                _charadata.MAXHP += 1;
                _gimic = !_gimic;
                _warming = !_warming;
                _currentUpTime = 0.0f;
               /// EditorUtility.SetDirty(_charadata);

                Wait().Forget();

            }

            else if(_currentTime < _nearTimeLimit)
            {

                //  制限時間が近くなったら
                if (!_isNearTimeLimitFlag)
                {
                    
                    _timeText.color = Color.red;
                    //_isNearTimeLimitFlag = true;
                    if (_currentUpTime > 5f)
                    {
                        AudioManager.instance.PlaySE("Warning");
                        Debug.Log("OK9");
                        _charadata.ATK += 1;
                        _charadata.MAXHP += 1;
                        _gimic = !_gimic;
                        _warming = !_warming;
                        _currentUpTime = 0.0f;
                        /// EditorUtility.SetDirty(_charadata);

                        Wait().Forget();

                    }

                }
            }
            
        }
    }
    public async UniTaskVoid Wait()
    {

        await UniTask.WaitForSeconds(0.6f);
        _warming = !_warming;


    }
}
