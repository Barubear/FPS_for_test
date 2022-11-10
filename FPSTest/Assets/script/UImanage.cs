using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanage : MonoBehaviour

    
{
    public string AxisPath;
    
    public bool Islog = true;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        AxisPath = Application.dataPath + "/dataset/axis";
        
    }
    public GameObject setCanvas;
    public GameObject mainCanvas;
    public Toggle openlog;
    public Toggle UseCheat;
    public Toggle defaltPath;
    
    
    public void OnStart(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    public void OnExit()
    {

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Onset()
    {
        setCanvas.SetActive(true);
        mainCanvas.SetActive(false);

    }

    public void Onback()
    {
        setCanvas.SetActive(false);
        mainCanvas.SetActive(true);

    }

    public void Logopen()
    {
        Islog = openlog.isOn;
       
        //print(Islog);
    }

    public void usecheat()
    {
        if(UseCheat.isOn==true){
            AxisPath = Application.dataPath + "/dataset/axis_cheat";
            
            print(AxisPath);
            
        }
        else
        {
            AxisPath = Application.dataPath + "/dataset/axis";
            
            print(AxisPath);
            
        }

        
    }

    
}
