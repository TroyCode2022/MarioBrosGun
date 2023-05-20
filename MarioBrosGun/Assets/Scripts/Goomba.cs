using System.Collections;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private GameObject bulletPrefab;
    public int health = 2;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Debug.Log("Goomba tocado saltando");
            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                player.Hit();
            }
        }
    
        if (collision.gameObject.CompareTag("Shell"))
        {
            Hit();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (bulletPrefab != other.gameObject)
            {
                health--;
                if (health == 0)
                    Hit();
                else
                    StartCoroutine(CambiarColor());

                bulletPrefab = other.gameObject;
            }
        }
        
    }

    IEnumerator CambiarColor() 
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.03f);
        spriteRenderer.color = Color.white;
    }
    

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        spriteRenderer.sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }


    // Funciones para detectar disparo
    private void TocadoPorBulletPrefab()
{
    Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);

    foreach (Collider2D collider in colliders)
    {
        if (collider.gameObject == bulletPrefab)
        {
            // Haz algo cuando el objeto es tocado por bulletPrefab
            Destroy(gameObject); // Por ejemplo, destruye el objeto actual
            break; // Sal del bucle foreach ya que ya se encontró la colisión con bulletPrefab
        }
    }
}

    // void Update()
    // {
    //     TocadoPorBulletPrefab();
    // }
}
