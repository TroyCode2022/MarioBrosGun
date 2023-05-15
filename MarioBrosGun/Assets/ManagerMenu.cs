using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EscenaJuego()
    {
        SceneManager.LoadScene("Scene1");
    }

    //* NOTA: Esto solo funcionará en una compilación del juego, no en el Editor de Unity
    public void TerminarJuego()
    {
        Application.Quit();
    }
}
