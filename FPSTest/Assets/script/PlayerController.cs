using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //控制方向
    public float rotaSpeed = 180;
    [Range(1,2)]
    public float rotaRatio;//灵敏度
    public Transform playerTransform;
    public Transform viewTransform;
    private float X_rotaAng;//x选择偏移
    public float xLimit = 60;
    //移动
    public CharacterController playerCC;
    public float moveSpeed = 10;
    public float _G = -9.8f;
    public float _GforJump = 20;
    public float _V = 0;
    public bool Isground = false;
    public Transform groundCheckPoint;
    public float checkRad = 0.7f;
    public LayerMask groundLayer;
    public float max_Hight = 5;
    public bool ContrLock = false;
    



    // Start is called before the first frame update
    void Start()
    {
        playerCC = this.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Screen.SetResolution(1600, 900, true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (!ContrLock)
        {
            PlayerRotaCtr();
            PlayerMovent();
        }
        
    }
    //控制方向
    private void PlayerRotaCtr()
    {
        if (playerTransform == null || viewTransform == null) return;
        float offset_x = Input.GetAxis("Mouse X");//控制水平方向（player,世界坐标的Y为旋转轴
        float offset_y = Input.GetAxis("Mouse Y");//控制垂直方向（view，X为旋转轴

        playerTransform.Rotate(Vector3.up * offset_x * rotaSpeed * rotaRatio * Time.fixedDeltaTime);
        X_rotaAng += offset_y * rotaSpeed * rotaRatio * Time.fixedDeltaTime;
        X_rotaAng = Mathf.Clamp(X_rotaAng, -xLimit, xLimit);

        Quaternion currentlocalRat = Quaternion.Euler(new Vector3(X_rotaAng, viewTransform.localEulerAngles.y, viewTransform.localEulerAngles.z));
        viewTransform.localRotation = currentlocalRat;
    }


    //控制移动
    private void PlayerMovent()
    {
        if (playerCC == null) return;
        Vector3 motionValue = Vector3.zero;
        //获取键盘输入
        float H_InputValue = Input.GetAxis("Horizontal");//左右
        float V_InputValue = Input.GetAxis("Vertical");//前后
        motionValue += this.transform.forward * moveSpeed * V_InputValue * Time.fixedDeltaTime;//前后
        motionValue += this.transform.right * moveSpeed * H_InputValue * Time.fixedDeltaTime;


        //垂直方向位移
        // h = 0.5 * _G * t * t;
        //Vector3.up
        _V += _G * Time.fixedDeltaTime;
       
        //地面监测
        Isground = Physics.CheckSphere(groundCheckPoint.position, checkRad, groundLayer);
        if (Isground) { 
            if ( _V <= 0) {

            
                _V = 0;
            }


        
            /*if (Input.GetButtonDown("Jump"))
            {
                _V = Mathf.Sqrt((max_Hight * 2) / _GforJump) * _GforJump;
            }*/
        }
        motionValue += Vector3.up * _V * Time.fixedDeltaTime;
        playerCC.Move(motionValue);
    }


   

}
