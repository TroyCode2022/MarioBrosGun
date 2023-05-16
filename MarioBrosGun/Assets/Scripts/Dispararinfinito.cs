using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispararinfinito : MonoBehaviour
{

    private float nextShot = 0.0f;
    public static float reloadTime = 0.3f;

    public GameObject bala;
    private Transform spawner;


    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        nextShot = Time.time + reloadTime;
        CheckFiring();
    }

    private void CheckFiring()
    {
        GameObject bullet = Instantiate(bala);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;
    }

    

}
