using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => Loader.Load(Loader.Scene.SampleScene);
        transform.Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();

        Time.timeScale = 1f;
        transform.Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => Application.Quit();
        transform.Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();

    }
}
