using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Referencia al canvas del menú de pausa
    
    void Start()
    {
        pauseMenu.SetActive(false); // Desactivamos el canvas al inicio del juego
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf) // Si el canvas está activo, lo desactivamos
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f; // Restauramos la velocidad del juego
            }
            else // Si el canvas está desactivado, lo activamos
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f; // Pausamos el juego
            }
        }
    }

    public void ButtonPlay()
    {
        this.Start();
    }

    public void ButtonExitGame()
    {
        Application.Quit(); // Salir de la aplicación (solo funciona en compilaciones)
        Debug.Log("Saliendo del juego"); // Mensaje de depuración para el editor de Unity
    }
}
