using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;
    private GameObject _canvas;
    private GameObject _UICanvas;
    public float VolumeLevel;
    public AudioMixer MainMixer;
    private bool _onPause = false;

    void Awake()
    {
        _canvas = gameObject;
        _UICanvas = GameObject.FindWithTag("UICanvas");
    }
    public void SetVolume(float val)
    {
        VolumeLevel = val;
        MainMixer.SetFloat("MainVolume", VolumeLevel);
    }
    public void BackToGame()
    {
        _onPause = false;
        _canvas.GetComponent<Canvas>().enabled = false;
        _UICanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1f;
    }
    public void ShowMainMenu(bool val)
    {
        settingsMenu.SetActive(!val);
        mainMenu.SetActive(val);
    }
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
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
