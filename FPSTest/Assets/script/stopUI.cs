using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stopUI : MonoBehaviour

{

    public GameObject panel;
    public GameObject exit;
    public GameObject mouse;
    public PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if(panel.activeSelf == true)
            {
                panel.SetActive(false);
                exit.SetActive(false);
                mouse.SetActive(true);
                Cursor.visible = false;
                PlayerController.ContrLock = false;
            }
            else
            {
                panel.SetActive(true);
                exit.SetActive(true);
                mouse.SetActive(false);
                Cursor.visible = true;
                PlayerController.ContrLock = true;
            }
        }
    }

    public void exitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void reStart(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
    public void reback(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
}
