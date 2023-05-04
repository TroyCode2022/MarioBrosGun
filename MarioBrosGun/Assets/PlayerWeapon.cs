using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform spawner;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFiring();
    }

        private void CheckFiring() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawner.position;
            bullet.transform.rotation = transform.rotation;
            //Destroy(bullet, 2f);
        }
    }
}
