using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScoreTableOptionsHandler : MonoBehaviour
{
    [SerializeField] EndingScoreTableController scoreController;
    public void RestartLevel()
    {
        GlobalScore.Restart();
        scoreController.DisableTables();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}
