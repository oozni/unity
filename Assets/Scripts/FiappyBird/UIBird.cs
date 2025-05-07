using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBird : MonoBehaviour
{
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.Instance;
        if (uiManager.scoreText == null)
        {
            Debug.Log("score is null");
        }
        if (uiManager.restartText == null)
        {
            Debug.Log("restart is null");
        }
        uiManager.restartText.gameObject.SetActive(false);
    }
    public void SetRestart()
    {
        uiManager.restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        uiManager.scoreText.text = score.ToString();
    }
}
