using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        SampleScene,
        Loading,
        MainMenu
    }
    private static Action loaderCallbackAction;
    public static void Load(Scene scene)
    {
        loaderCallbackAction = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallBack()
    {
        if(loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction=null;
        }
    }
}
