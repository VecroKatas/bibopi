using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public static class SceneLoader
{
    public enum Scene
    {
        Bar, 
        BarCounter,
        Loading,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
