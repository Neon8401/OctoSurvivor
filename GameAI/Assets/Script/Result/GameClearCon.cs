using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearCon : MonoBehaviour
{
    // Start is called before the first frame update

    public static float Score;

    [SerializeField] private Text previousHighScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject isHighScoreUpdatedMessage;

    private void Start()
    {
        var previousHighScore = SaveData.Instance.HighScore;
        previousHighScoreText.text = "HighScore" + "" + previousHighScore;
        scoreText.text = "" + Score;
        AudioManager.instance.PlaySE("GameClaer");
        if (previousHighScore < Score)
        {
            // �X�R�A���O�̃n�C�X�R�A����������΁A�n�C�X�R�A�Ƃ��ċL�^
            isHighScoreUpdatedMessage.SetActive(true);
            SaveData.Instance.HighScore = (int)Score;
            SaveData.Instance.Save();
        }
        else
        {
            isHighScoreUpdatedMessage.SetActive(false);
        }
    }
}
