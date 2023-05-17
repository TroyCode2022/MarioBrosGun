using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    public int health = 5;

    private bool shelled;
    private GameObject bulletPrefab;
    private bool pushed;
    

    //private void Update()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    if (rb.velocity.magnitude > 0f && shelled && !pushed)
    //    {
    //        Vector2 direction = GetComponent<EntityMovement>().direction;
    //        PushShell(direction);
            
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell(collision);
            }  else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            if (bulletPrefab != other.gameObject)
            {
                health--;
                if (health == 0)
                    Hit();

                bulletPrefab = other.gameObject;
            }
        }
    }

    private void EnterShell(Collision2D other)
    {
        shelled = true;

        GetComponent<SpriteRenderer>().sprite = shellSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        gameObject.tag = "Shell";
       
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed) {
            Destroy(gameObject);
        }
    }

}
