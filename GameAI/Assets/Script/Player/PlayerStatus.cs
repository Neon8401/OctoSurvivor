using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    
    protected override void OnDie()
    {
        base.OnDie();
        MainSceneController.Instance.GameOver();
    }


    private IEnumerator GotoGameOverCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScene");
    }
}