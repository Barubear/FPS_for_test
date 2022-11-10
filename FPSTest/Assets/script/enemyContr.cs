using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemyContr : MonoBehaviour
{
    // Start is called before the first frame update
   
    
    public Collision Collision;
    
    
    
    public bool IsDead = false;

    //血条
    public Slider PHslider;
    public float HP = 100;
    public float MaxHP = 100;

    //行动模式
    public int createPoint;//1,2,3,4,生成点//上层点y = 1.35
    public int actionPattern;//行动模式
    bool IsRight;//行进方向
    public float MoveSpeed= 2;
    public GameObject JumpBox;
    public GameObject backBoxR;
    public GameObject backBoxL;
    public GameObject stopBox;
    

    public bool IsJump;
    bool crearted = true;
    
    void Start()
    {
        
        
        IsRight = createPoint <= 2 ? true : false;//12为右侧点，34为左侧点

        
        actionPattern = Random.Range(1, 7);
        if (createPoint == 2 || createPoint == 4) //上层点
        {
            if (actionPattern == 2) actionPattern++;
        }

        
        
        switch (actionPattern)
        {
            case 1: //左右横跳              
                
                if (IsRight) 
                {
                    movetValue = 1;
                    
                    backBoxR.transform.position = this.gameObject.transform.position + new Vector3(3.5f,0,0);
                }
                else
                {
                    movetValue = -1;
                    backBoxL.transform.position = this.gameObject.transform.position - new Vector3(3.5f, 0, 0);
                    
                }

                break;
                
            case 2:
                //下方点用，直线抛出，随机点随机跳跃
                if (IsRight)
                {
                    movetValue = 1;
                    JumpBox.transform.position = this.gameObject.transform.position + new Vector3(Random.Range(2,8), 0, 0);
                    
                }
                else
                {
                    movetValue = -1;
                    JumpBox.transform.position = this.gameObject.transform.position - new Vector3(Random.Range(2, 8), 0, 0);
                }

                break;

            case 3:
                //上方点用，指定点跳下
                if (IsRight)
                {
                    movetValue = 1;
                    JumpBox.transform.position = this.gameObject.transform.position + new Vector3(3, 0, 0);

                }
                else
                {
                    movetValue = -1;
                    JumpBox.transform.position = this.gameObject.transform.position - new Vector3(3, 0, 0);
                }

                break;
            case 4://随机点停止，左右横跳
                if (IsRight)
                {
                    movetValue = 1;
                    backBoxR.transform.position = this.gameObject.transform.position + new Vector3(Random.Range(7,12), 0, 0);
                    
                }
                else
                {
                    movetValue = -1;
                    backBoxL.transform.position = this.gameObject.transform.position - new Vector3(Random.Range(7, 12), 0, 0);
                    
                }

                break;

            case 5:
                //随机点停止
                if (IsRight)
                {
                    movetValue = 1;
                    stopBox.transform.position = this.gameObject.transform.position + new Vector3(Random.Range(7, 12), 0, 0);

                }
                else
                {
                    movetValue = -1;
                    stopBox.transform.position = this.gameObject.transform.position - new Vector3(Random.Range(7, 12), 0, 0);

                }

                break;
            case 6:
                //随机点停止后返回
                crearted = false;
                if (IsRight)
                {
                    movetValue = 1;
                    backBoxL.transform.position = this.gameObject.transform.position + new Vector3(Random.Range(9, 12), 0, 0);
                    
                }
                else
                {
                    movetValue = -1;
                    backBoxR.transform.position = this.gameObject.transform.position - new Vector3(Random.Range(9, 12), 0, 0);
                    
                }

                break;


        }

        PHslider.value = HP/MaxHP * PHslider.maxValue;//血条

        
        

    }

    // Update is called once per frame
    void Update()
    {
       
        
        DoDestory(3f);
    }

    private void FixedUpdate()
    {
        Movent();
    }

    public void beDamaged(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            PHslider.value = HP / MaxHP * PHslider.maxValue;
        }
            
        if (HP<= 0)
        {
            //死亡处理
            
            Destroy(this.gameObject);
            IsDead = true;
        }


    }
    public void DoDestory(float aliveTime)
    {
        Destroy(this.gameObject, aliveTime);
        IsDead = true;
       
    }
    
  
    //跳跃点&折返点

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("jumpBox"))
        {
            IsJump = true;
            
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.CompareTag("backBox"))
        {
            movetValue = -movetValue;
            if (IsRight&&crearted)
            {                
                backBoxL.transform.position = backBoxR.transform.position -  new Vector3(3, 0, 0);
                crearted = false;
            }
            else if (!IsRight && crearted)
            {          
                backBoxR.transform.position = backBoxL.transform.position + new Vector3(3, 0, 0);
                crearted = false;
            }

        }

        if (collider.gameObject.CompareTag("stopBox"))
        {
            MoveSpeed = 0;
        }

    }




    public CharacterController enemyCC;
    public float _G = -20f;
    
    public float _V = 0;
    public bool Isground = false;
    public Transform groundCheckPoint;
    float checkRad = 0.1f;
    public LayerMask groundLayer;
    public float maxhight;
    public float movetValue;

    private void Movent()
    {
        if (enemyCC == null) return;
        Vector3 motionValue = Vector3.zero;
        
        
       
        motionValue += this.transform.right * MoveSpeed * movetValue * Time.fixedDeltaTime;


        //垂直方向位移
        // h = 0.5 * _G * t * t;
        //Vector3.up
        _V += _G * Time.fixedDeltaTime;

        //地面监测
        Isground = Physics.CheckSphere(groundCheckPoint.position, checkRad, groundLayer);
        if (Isground)
        {
            if (_V <= 0)
            {


                _V = 0;
            }



            if (IsJump)
            {
                _V = maxhight;
            }
        }
        motionValue += Vector3.up * _V * Time.fixedDeltaTime;
        enemyCC.Move(motionValue);
        IsJump = false;
    }

    

    
  

}