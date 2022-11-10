using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCreat : MonoBehaviour
{
   
    public GameObject NextLayer;
    public GameObject ec;
    Transform creatPoint;
    
    public List<Transform> creatponint = new List<Transform>();
    GameObject newEnemy;
    public int createPointNum;//1,2,3,4,生成点//上层点y = 1.35
    public int actionPattern;//行动模式
    

    void Start()
    {

        createPointNum= Random.Range(1, 5);
        creatPoint = creatponint[createPointNum-1];
        


    }
        
    // Update is called once per frame
    void Update()
    {

        if(newEnemy != null) 
        {
           
                if (newEnemy.GetComponentInChildren<enemyContr>().IsDead)
                {
                    if (NextLayer != null)
                    {
                        NextLayer.SetActive(true);
                        Destroy(this.gameObject);
                        
                    }

                }
            
            
        }
        



    }

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("player"))
        {
            newEnemy = Instantiate(ec, creatPoint.position, creatPoint.rotation);
            newEnemy.GetComponentInChildren<enemyContr>().createPoint = createPointNum;
            this.GetComponent<BoxCollider>().enabled =false;
        }
    }


}
