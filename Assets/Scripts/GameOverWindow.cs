using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow instance;
    private void Awake()
    {
        instance = this;
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => {
            Loader.Load(Loader.Scene.SampleScene);
        } ;
        transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();

        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public static void ShowStatic()
    {
        instance.Show();
    }
}
