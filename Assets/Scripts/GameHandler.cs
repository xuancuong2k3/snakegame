using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    private static int score;

    private void Awake()
    {
        instance = this;
        InitializeStatic();
    }

    private void Start()
    {
        //CMDebug.ButtonUI(Vector2.zero, "Reload Scene", () =>
        //{
        //    Loader.Load(Loader.Scene.SampleScene);
        //});
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPauseGame())
            {
                GameHandler.ResumeGame();
            } else
            {
                GameHandler.PauseGame();
            }
        }
    }
    private static void InitializeStatic()
    {
        score = 0;
    }
    public static int GetScore()
    {
        return score;
    }
    public static void AddScore()
    {
        score += 100;
    }
    public static void SnakeDied()
    {
        GameOverWindow.ShowStatic();
    }
    public static void ResumeGame()
    {
        PauseWindow.HideStatic();

        Time.timeScale = 1f;
    }
    public static void PauseGame()
    {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }
    public static bool IsPauseGame()
    {
        return Time.timeScale == 0f;
    }
}
