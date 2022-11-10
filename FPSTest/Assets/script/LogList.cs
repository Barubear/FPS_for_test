using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogList : MonoBehaviour
{
    public List<List<(float, float)>> AxisList = new List<List<(float, float)>>();
    public List<List<(float, float)>> PosList = new List<List<(float, float)>>();
    public int Index = -1;
    public PlayerController PlayerController;
    public UImanage Uimanager;
    public string AxisPath;
    public string PosPath;
    public bool Islog;
    public GameObject panel;
    public GameObject exit;
    public GameObject mouse;
    public GameObject again;
    public GameObject back;
    void Awake()
    {
        Uimanager = FindObjectOfType<UImanage>();
        AxisPath = Uimanager.AxisPath;
        
        Islog = Uimanager.Islog;
        print(Islog);
    }

    
    void Update()
    {
       
    }
    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("player"))
        {

            PlayerController.ContrLock = true;
            this.GetComponent<BoxCollider>().enabled = false;
            //print(Islog);
            
            if (!Directory.Exists(AxisPath))
            {
                Directory.CreateDirectory(AxisPath);
            }

            if (Islog)
            {
               

                for (int i = 0; i < 11; i++)
                {
                    print(i/11*100  + "%");

                    string fileName = GetFileName();
                    StreamWriter Axissw;                                      
                    FileInfo Axisfi = new FileInfo(AxisPath + "//" + fileName + "_" + i + ".txt");//E:/testdata/cheat_axis
                                                                                                  //
                   
                    
                    
                    if (!Axisfi.Exists)
                        Axissw = Axisfi.CreateText();
                    else
                        Axissw = Axisfi.AppendText();

                   

                    foreach ((float x, float y) in AxisList[i])
                    {
                        Axissw.WriteLine(x + "," + y);
                        //print(x+","+ y);
                    }
                    Axissw.Close();
                    Axissw.Dispose();

                    
                }

               


                   print("saveOver");
            }

            panel.SetActive(true);
            exit.SetActive(true);
            again.SetActive(true);
            back.SetActive(true);
            mouse.SetActive(false);
            Cursor.visible = true;
           

        }
    }


    string GetFileName()
    {
        string pathTime = System.DateTime.Now.ToString();
        string flieName = pathTime.Replace("/", "");
        flieName = flieName.Replace(":", "");
        flieName = flieName.Replace(" ", "");
        return flieName;
    }
}
