using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToBar : MonoBehaviour
{
    public void Click()
    {
        SceneLoader.Load(SceneLoader.Scene.Bar);
    }
}
