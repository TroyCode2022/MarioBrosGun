using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//este script solo se activara para la versio android 

public class PlayerWeaponAndroid : MonoBehaviour
{
    public Transform spawner;
    public GameObject bulletPrefab;

    private float nextShot = 0.0f;
    private static float reloadTime = 0.3f;
    private bool shooting = false;
    private AudioSource audioShot;


    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        // Comprueba si hay al menos dos componentes de audio
        if (audioSources.Length >= 1)
        {
            // Asigna el segundo componente de audio al campo "audioJump"
            audioShot = audioSources[0];
        }
        else
        {
            // Maneja el caso en el que no haya suficientes componentes de audio
            Debug.LogError("No se encontró el segundo componente de audio.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shotAndroid()
    {
        CheckFiring();
                        shooting = true;
            // Reproduce shot sound
            audioShot.Play();

        

            shooting = false;
        

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
