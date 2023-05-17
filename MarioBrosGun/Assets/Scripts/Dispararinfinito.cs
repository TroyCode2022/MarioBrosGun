using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dispararinfinito : MonoBehaviour
{

    private float nextShot = 0.0f;
    public float reloadTime = 3.5f;

    public GameObject bala;
    private Transform spawner;

    private Vector2 _direction;

    public enum Direction
    {
        left,
        right,
        up
    }
    public Direction dir;



    private void Awake()
    {
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
    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.transform.GetChild(0);
        switch (dir)
        {
            case Direction.left: _direction = Vector2.left; break;

            case Direction.right: _direction = Vector2.right; break;

            case Direction.up: _direction = Vector2.up; break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + reloadTime;
            CheckFiring();
        }
    }

    private void CheckFiring()
    {
        GameObject bullet = Instantiate(bala);
        bullet.GetComponent<BigBalaMovement>().setDirection(_direction);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;
    }

    

}
