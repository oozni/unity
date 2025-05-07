using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UiState
{
    Home,
    Game,
    Score,
}
public class UIManager : MonoBehaviour
{
    static UIManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;

    TheStack theStack = null;
    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        theStack = FindObjectOfType<TheStack>();
        
        
    }
}
