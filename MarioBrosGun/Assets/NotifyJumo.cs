using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyJumo : MonoBehaviour
{
   private PlayerMovement playerMovement; // Referencia al script PlayerMovement

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Busca el objeto con el script PlayerMovement y obtiene la referencia
    }

    public void Jump()
    {
        if (playerMovement != null)
        {
            playerMovement.GroundedMovement(true); // Llama a la función GroundedMovement del script PlayerMovement
            playerMovement.ApplyGravity(true);
        }
        else
        {
            Debug.LogError("PlayerMovement script not found!");
        }
    }
}