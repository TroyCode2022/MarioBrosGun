using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementToTarget : MonoBehaviour
{
    Transform target;    // Objeto que queremos perseguir

    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
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

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }


    private void FixedUpdate()
    {
        float raycastDistance = 5.0f;
        //float yPosition = Mathf.Sin(Time.time * 5f) * 3f;

        direction = (transform.position - target.position).normalized;
        velocity.x = direction.x * speed;
        velocity.y = direction.y * speed;

        if (target.position.x < transform.position.x)
            velocity.x *= -1;
        if (target.position.y < transform.position.y)
            velocity.y *= -1;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance);

        if (hit.collider != null)
        {
            if(!hit.collider.gameObject.CompareTag("Player"))
                Debug.Log("HAY COSAS");
        }


        rigidbody.MovePosition(rigidbody.position + direction * velocity * Time.fixedDeltaTime);

        
        if (direction.x < 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x > 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }

    }


}
