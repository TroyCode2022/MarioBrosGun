using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bowser : MonoBehaviour
{

    //Variables para manejar la vida
    private GameObject bulletPrefab;
    public int health = 2;

    //Variables para hacer las distintas animaciones
    private HitAnimation hitAnimation;
    private SpecialAnimation specialAnimation;
    private int maxHealth;
    private float siguietenePorcentaje = 0.9f;

    //Variables para invocar enemigos
    public Transform spawns;
    private int childCount;
    public GameObject goomba;


    private void Awake()
    {
        enabled = false;
        hitAnimation = GetComponent<HitAnimation>();
        specialAnimation = GetComponent<SpecialAnimation>();
        maxHealth = health;
        childCount = spawns.childCount;
    }

    private void OnBecameVisible()
    {
        #if UNITY_EDITOR
            enabled = !EditorApplication.isPaused;
        #else
            enabled = true;
        #endif
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.starpower)
            {
                Hit();
            }
            else
            {
                player.Hit();
            }
        }

    }

    //Comprobamos que le haya dado una bala
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (bulletPrefab != other.gameObject)
            {
                if(hitAnimation.enabled)
                    StartCoroutine(hitAnimation.doAnimation());
                
                health--;
                checkHealth();

                bulletPrefab = other.gameObject;
            }

        }

    }

    //Por cada 10% de vida menos empezará una animación de salto
    private void checkHealth()
    {
        if (health == 0)
            Hit();
        else
        {
            float porcentajeRestante = ((float)health / maxHealth);

            if (porcentajeRestante <= siguietenePorcentaje)
            {
                siguietenePorcentaje -= 0.1f;
                hitAnimation.StopAllCoroutines();
                hitAnimation.enabled = false;
                specialAnimation.enabled = true;
                Attack();
            }
        }
    }

    private void Attack()
    {
        Transform spawn;
        for (int i = 0; i < childCount; i++)
        {
            spawn = spawns.GetChild(i).transform;
            GameObject bullet = Instantiate(goomba);
            bullet.transform.position = spawn.position;
        }
    }

    private void Hit()
    {
        hitAnimation.StopAllCoroutines();
        hitAnimation.enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }


}
