using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float paddingLeft;
    [SerializeField]
    private float paddingRight;
    [SerializeField]
    private float paddingTop;
    [SerializeField]
    private float paddingBottom;

    private Vector2 rawInput;
    private Vector2 minBoundary;
    private Vector2 maxBoundary;

    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();        
    }

    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBoundary = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBoundary = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()
    {
        Vector3 delta = rawInput * Time.deltaTime * moveSpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBoundary.x + paddingLeft, maxBoundary.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBoundary.y + paddingBottom, maxBoundary.y - paddingTop);
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.IsFiring = value.isPressed;
        }
    }
}
