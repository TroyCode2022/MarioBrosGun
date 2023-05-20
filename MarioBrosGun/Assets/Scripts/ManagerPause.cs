using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPause : MonoBehaviour
{
    public GameObject pauseMenu; // Referencia al canvas del menú de pausa
    AudioSource[] audios;

    void Awake()
    {
        audios = FindObjectsOfType<AudioSource>();
    }

    void Start()
    {
        pauseMenu.SetActive(false); // Desactivamos el canvas al inicio del juego
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Detener sonidos


            if (pauseMenu.activeSelf) // Si el canvas está activo, lo desactivamos y activamos el sonido
            {
                pauseMenu.SetActive(false);
                //foreach(AudioSource a in audios)
                //    a.Play();
                Time.timeScale = 1f; // Restauramos la velocidad del juego
            }
            else // Si el canvas está desactivado, lo activamos y paramos el sonido
            {
                pauseMenu.SetActive(true);
                //foreach(AudioSource a in audios)
                //    a.Pause();
                Time.timeScale = 0f; // Pausamos el juego

            }
        }
    }

    public void ButtonPlay()
    {
        //foreach(AudioSource a in audios) // restore sound
        //    a.Play();
        this.Start();
    }

    public void ButtonExitGame()
    {
        Application.Quit(); // Salir de la aplicación (solo funciona en compilaciones)
        Debug.Log("Saliendo del juego"); // Mensaje de depuración para el editor de Unity
    }
}
