using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI textMeshProComponentLives;
    public TextMeshProUGUI textMeshProComponentScore;


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        textMeshProComponentScore.text = gameManager.GetScore().ToString();
        var x = gameManager.GetLives().ToString();
        textMeshProComponentLives.text = x;
    }
}
