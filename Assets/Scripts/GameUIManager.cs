using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    public Animator pnlExit;
    public Animator pnlWin;

    public void ExitScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OpenExitPanel()
    {
        pnlExit.SetBool("IsHidden", false);
    }

    public void CloseExitPanel()
    {
        pnlExit.SetBool("IsHidden", true);        
    }

    public void OpenWinPanel()
    {
        pnlWin.SetBool("IsHidden", false);
    }

    public void CloseWinPanel()
    {
        pnlWin.SetBool("IsHidden", true);
    }
}
