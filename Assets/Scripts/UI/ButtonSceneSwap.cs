using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneSwap : MonoBehaviour
{
    [SerializeField] private string m_sceneName = "MapName";
    public void OnButtonPressed()
    {
        SceneSwaper.LoadScene(m_sceneName);
    }
}
