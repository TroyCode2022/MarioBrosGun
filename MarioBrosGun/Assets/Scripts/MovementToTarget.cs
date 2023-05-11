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
        direction = (target.position - transform.position).normalized;
        velocity.x = direction.x * speed;
        velocity.y = direction.y * speed;
        Debug.Log("aaa");
        //direction = Vector2.left;
        float yPosition = Mathf.Sin(Time.time * 1.5f) * 0.005f;
        //Vector2 position = transform.position + direction * velocity * Time.deltaTime + Vector2.up * yPosition
        rigidbody.MovePosition(rigidbody.position + direction * velocity * Time.fixedDeltaTime);

    }


}
