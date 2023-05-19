using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyShot : MonoBehaviour
{
   private PlayerWeaponAndroid pwa; // Referencia al script PlayerMovement

    void Start()
    {
        pwa= FindObjectOfType<PlayerWeaponAndroid>(); // Busca el objeto con el script PlayerMovement y obtiene la referencia
    }

    public void Shot()
    {
        if (pwa != null)
        {
            pwa.shotAndroid();
        }
        else
        {
            Debug.LogError("PlayerMovement script not found!");
        }
    }
}
