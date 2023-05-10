using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform spawner;
    public GameObject bulletPrefab;

    private float nextShot = 0.0f;
    private static float reloadTime = 0.3f;
    private bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            //CheckFiring();
        }

        if(Input.GetMouseButtonUp(0))
        {
            shooting = false;
        }

        if(shooting && Time.time > nextShot)
        {
            nextShot = Time.time + reloadTime;
            CheckFiring();
        }
    }

    private void CheckFiring() { 
        
        
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;
        //Destroy(bullet, 2f)
       
    }

    //lo utilizaremos para el power up FastShot que incrementa la cadencia
    static public void ReduceReloadtime()
    {
        reloadTime = 0.15f;
    }

    //Para cuando se acabe FastShot reseteamos a la cadencia normal
    static public void ResetReloadtime()
    {
        reloadTime = 0.3f;
    }


}
