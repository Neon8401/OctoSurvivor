using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour
{
    public static float Score;

    [SerializeField] private Text previousHighScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject isHighScoreUpdatedMessage;

    private void Start()
    {
        var previousHighScore = SaveData.Instance.HighScore;
        previousHighScoreText.text = "HighScore"+ ""+ previousHighScore;
        scoreText.text = "" + Score;
        AudioManager.instance.PlaySE("GameOver");
        if (previousHighScore < Score)
        {
            // スコアが前のハイスコアよりも高ければ、ハイスコアとして記録
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