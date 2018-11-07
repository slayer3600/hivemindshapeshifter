using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

    public Animator btnStart;
    public Animator btnSettings;
    public Animator dialogSettings;

    public void StartGame () {

        SceneManager.LoadScene("HiveMindMain");

	}

    public void OpenSettings()
    {
        btnStart.SetBool("IsHidden", true);
        btnSettings.SetBool("IsHidden", true);
        dialogSettings.SetBool("IsHidden", false);
    }

    public void CloseSettings()
    {
        btnStart.SetBool("IsHidden", false);
        btnSettings.SetBool("IsHidden", false);
        dialogSettings.SetBool("IsHidden", true);
    }

    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying needs to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
