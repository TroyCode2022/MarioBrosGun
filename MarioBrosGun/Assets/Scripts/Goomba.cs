using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private GameObject bulletPrefab;
    public int health = 2;

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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (bulletPrefab != other.gameObject)
            {
                Debug.Log("balaso");
                health--;
                Debug.Log("MENOS health: " + health);
                if (health == 0)
                    Hit();

                bulletPrefab = other.gameObject;
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
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
            Debug.Log("Tocado por bala");
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
