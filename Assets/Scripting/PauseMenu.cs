using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject _canvas;
    private GameObject _UICanvas;
    private bool _onPause = false;

    void Awake()
    {
        _canvas = gameObject;
        _UICanvas = GameObject.FindWithTag("UICanvas");
    }

    public void BackToGame()
    {
        _onPause = false;
        _canvas.GetComponent<Canvas>().enabled = false;
        _UICanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void GamePause()
    {
        _onPause = true;
        _canvas.GetComponent<Canvas>().enabled = true;
        _UICanvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!_onPause && Input.GetKeyDown(KeyCode.Escape))
            GamePause();
    }
}
