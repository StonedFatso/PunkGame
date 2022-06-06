using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;
    public float VolumeLevel;
    public AudioMixer MainMixer;

    private void Start()
    {
        ShowMainMenu(true);
    }
    public void SetVolume(float val)
    {
        VolumeLevel = val;
        MainMixer.SetFloat("MainVolume", VolumeLevel);
    }
    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
}
