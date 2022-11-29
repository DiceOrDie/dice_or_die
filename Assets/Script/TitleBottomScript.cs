using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleBottomScript : MonoBehaviour
{
    public void ContinueBottom()
    {
    }
    public void StartBottom()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingBottom()
    {

    }
    public void ExitBottom()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
