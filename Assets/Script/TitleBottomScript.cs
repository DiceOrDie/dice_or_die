using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleBottomScript : MonoBehaviour
{
    void Start(){
        foreach (GameObject o in DontDestroy.dont_destroy) {
            print(o.name);
            // DontDestroy.dont_destroy.Remove(o);
            Destroy(o);
        }
        DontDestroy.dont_destroy.Clear();
    }
    public void ContinueBottom()
    {
    }
    public void StartBottom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
