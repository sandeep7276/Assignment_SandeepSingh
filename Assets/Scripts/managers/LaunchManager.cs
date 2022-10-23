
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SnakeGame;


public class LaunchManager : MonoBehaviour
{
    [SerializeField] Text highScoreTxt;

    private void Start()
    {
        SetScoreToMenu();
    }

    private void SetScoreToMenu()
    {
        highScoreTxt.text = $" High Score :  {GetScore()}";
    }

    private string GetScore()
    {
        IOOperation io = new IOOperation(DataConst.userFilePath);
        return io.LoadData();
    }


    public void LaunchGame()
    {
        SceneManager.LoadScene(DataConst.GameSceneName, LoadSceneMode.Single);
    }
}
