using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBalaMovement : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    public static float speed = 6f;

    private Vector2 _direction = Vector2.left;
    private Vector2 velocity;
    private ExplosionAnimation _explosionAnimation;
    private AudioSource[] _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _explosionAnimation =  GetComponent<ExplosionAnimation>();
        _audioSource = GetComponents<AudioSource>();


    }

    public void setDirection(Vector2 direction)
    {
        _direction = direction;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Hit();
            }
            else
            {
                player.Hit();
            }
        }
        else
        {
            _audioSource[0].Play(); 
            _explosionAnimation.enabled = true;
            this.enabled = false;
        }

    }
    private void Hit()
    {
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }


    void FixedUpdate()
    {

        velocity.x = _direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
