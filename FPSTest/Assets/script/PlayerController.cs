using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //���Ʒ���
    public float rotaSpeed = 180;
    [Range(1,2)]
    public float rotaRatio;//������
    public Transform playerTransform;
    public Transform viewTransform;
    private float X_rotaAng;//xѡ��ƫ��
    public float xLimit = 60;
    //�ƶ�
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
    //���Ʒ���
    private void PlayerRotaCtr()
    {
        if (playerTransform == null || viewTransform == null) return;
        float offset_x = Input.GetAxis("Mouse X");//����ˮƽ����player,���������YΪ��ת��
        float offset_y = Input.GetAxis("Mouse Y");//���ƴ�ֱ����view��XΪ��ת��

        playerTransform.Rotate(Vector3.up * offset_x * rotaSpeed * rotaRatio * Time.fixedDeltaTime);
        X_rotaAng += offset_y * rotaSpeed * rotaRatio * Time.fixedDeltaTime;
        X_rotaAng = Mathf.Clamp(X_rotaAng, -xLimit, xLimit);

        Quaternion currentlocalRat = Quaternion.Euler(new Vector3(X_rotaAng, viewTransform.localEulerAngles.y, viewTransform.localEulerAngles.z));
        viewTransform.localRotation = currentlocalRat;
    }


    //�����ƶ�
    private void PlayerMovent()
    {
        if (playerCC == null) return;
        Vector3 motionValue = Vector3.zero;
        //��ȡ��������
        float H_InputValue = Input.GetAxis("Horizontal");//����
        float V_InputValue = Input.GetAxis("Vertical");//ǰ��
        motionValue += this.transform.forward * moveSpeed * V_InputValue * Time.fixedDeltaTime;//ǰ��
        motionValue += this.transform.right * moveSpeed * H_InputValue * Time.fixedDeltaTime;


        //��ֱ����λ��
        // h = 0.5 * _G * t * t;
        //Vector3.up
        _V += _G * Time.fixedDeltaTime;
       
        //������
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
