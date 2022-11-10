using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtr : MonoBehaviour
{
    public Transform bullerStartPoint;
    public GameObject bullet;
    public float bulletStartSpeed = 100;
    public float fireSpeed = 0.5f;
    public bool openFire = false;
    public float lerpRatio = 0.2f;
    //后座力
    public Transform defult;
    public Transform back;

    public PlayerController PlayerController;
    //枪声
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.ContrLock)
        {
            OpenFire();
        }
        
    }

    private void OpenFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            openFire = true;
            StartCoroutine("Fire");
            

        }

        if (Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonUp(0))
        {
            openFire = false;
            StopCoroutine("Fire");
        }

    }

    IEnumerator Fire()
    {
        while (openFire)
        {
            if (bullerStartPoint != null || bullet != null)
            {
                GameObject newBullet = Instantiate(bullet, bullerStartPoint.position, bullerStartPoint.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletStartSpeed;
                PlayVoice();
                //后坐力
                StopCoroutine("WeapenBackAnim");
                StartCoroutine("WeapenBackAnim");

                Destroy(newBullet, 0.25f);

            }
            yield return new WaitForSeconds(fireSpeed);//中断函数，等待

        }

    }

    private void OneFire()
    {
        if (bullerStartPoint == null || bullet == null) return;
        GameObject newBullet = Instantiate(bullet, bullerStartPoint.position, bullerStartPoint.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletStartSpeed;
        Destroy(newBullet, 0.25f);
    }

    IEnumerator WeapenBackAnim()
    {
        //往后
        if(defult != null && back != null)
        {
            while(this.transform.localPosition != back.localPosition) {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, back.localPosition, lerpRatio*3);
                yield return null;
            }
            while (this.transform.localPosition != defult.localPosition)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, defult.localPosition, lerpRatio);
                yield return null;
            }

        }
        




    }

    private void PlayVoice()
    {
        if (audio)
        {
            audio.Play();
        }
    }

}
