using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class inputLog : MonoBehaviour
{
    // Start is called before the first frame update
    
    public enemyContr enemyContr;
    
    public float xpoint;
    public float ypoint;
    public Vector2 pos;
    public string pathTime;
    public LogList LogList;
    void Start()
    {
        LogList = FindObjectOfType<LogList>();
        LogList.Index++;
        LogList.AxisList.Add( new List<(float, float)>()  );
        
        //print(LogList.Index);
        
    }

    // Update is called once per frame
    void Update()
    {
        xpoint = Input.GetAxis("Mouse X");
        ypoint = Input.GetAxis("Mouse Y");
        
        LogList.AxisList[LogList.Index].Add((xpoint, ypoint));
        

       
    }

    

    
}
