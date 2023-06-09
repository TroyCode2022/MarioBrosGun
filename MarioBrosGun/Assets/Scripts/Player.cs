using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    AudioSource[] audioSources;

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    private Transform pistolTransform; 

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        audioSources[0].Play();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
        pistolTransform = transform.GetChild(2);// Obtenemos la pistola
    }

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

    public void Death()
    {
        audioSources[0].Stop();
        audioSources[1].Play();
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow()
    {
        if (!big)
        {
            audioSources[4].Play();
            smallRenderer.enabled = false;
            bigRenderer.enabled = true;
            activeRenderer = bigRenderer;

            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);

            StartCoroutine(ScaleAnimation());

            // Modificación de la posición de Pistol
            Transform pistolTransform = transform.GetChild(2);
            if (pistolTransform != null)
            {
                pistolTransform.position = transform.GetChild(4).position;
            }
        }
    }

    public void Shrink()
    {
        audioSources[3].Play();
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());

        // Modificación de la posición de Pistol
        Transform pistolTransform = transform.GetChild(2);
        if (pistolTransform != null) {
            pistolTransform.position = transform.GetChild(3).position;
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
        //audioSources[2].Play();
        
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;
        audioSources[0].Stop();
        audioSources[5].Play();

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
        audioSources[5].Stop();
        audioSources[0].Play(); 
    }

}
