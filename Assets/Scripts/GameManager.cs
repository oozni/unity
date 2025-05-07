using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    UIBird uiBird;
    public UIBird uIBird { get { return uiBird; } }

    private int currentScore = 0;

    private void Awake()
    {
        gameManager = this;
        uiBird = FindAnyObjectByType<UIBird>();
    }
    private void Start()
    {
        uiBird.UpdateScore(0);
    }
    public void GameOver()
    {
        Debug.Log("gameover");
        uiBird.SetRestart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Addscore(int score)
    {
        currentScore += score;
        Debug.Log(currentScore);
        uiBird.UpdateScore(currentScore);
    }
}
