using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int level;
    private int lives;
    private int score;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip theme;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        score = 0;

        LoadLevel(1,false);

    }

    private void LoadLevel(int index, bool failed)
    {

        level = index;

        Camera camera = Camera.main;

        if(camera != null)
        {
            camera.cullingMask = 0;
        }

        if (failed)
        {
            lives--;
        }

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
        
    }


    public void LevelComplete()
    {
        score += 100;
        level++;

        if(level < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(level,false);
        }
        else
        {
            LoadLevel(1,false);
        }
    }

    public void LevelFailed()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        Invoke("Theme", 0.6f);

        if (lives - 1 == 0)
        {
            NewGame();
        }
        else
        {
            LoadLevel(level,true);
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetScore()
    {
        return score;
    }

    private void Theme()
    {
        audioSource.clip = theme;
        audioSource.Play();
    }
}
