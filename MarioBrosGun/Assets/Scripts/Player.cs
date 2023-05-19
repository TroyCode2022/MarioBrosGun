using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    private Transform pistolTransform;
    private Vector3 initialPositionPistol; 

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
        pistolTransform = transform.GetChild(2);// Obtenemos la pistola
        initialPositionPistol = pistolTransform.position;
    }

    // Manejo cuando Mario es alcanzado por un enemigo
    public void Hit()
    {
        if (!dead && !starpower)
        {
            if (big) {
                Shrink();
            } else {
                Death();
            }
        }
    }

    // Muerte de Mario
    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
    // Hacer crecer a mario y modificar la posición de la pistola
    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
        
        // Modificación de la posición de Pistol
        Transform pistolTransform = transform.GetChild(2);
        if (pistolTransform != null) {
            pistolTransform.position += new Vector3(0.15f, 0.35f, 0);
        }
    }
    
    // Disminuir tamaño 
    public void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());

        // Modificación de la posición de Pistol
        Transform pistolTransform = transform.GetChild(2);
        if (pistolTransform != null) {
            pistolTransform.position -= new Vector3(0.15f, 0.35f, 0);
        }
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;
       
        gameObject.transform.GetChild(2).gameObject.SetActive(false); // Desactivar pistola
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }
        gameObject.transform.GetChild(2).gameObject.SetActive(true);//Activar pistola
        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }

}
