using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PowerUpManager.instance.ActivateShield(transform));
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = (Vector2.right * Speed * horizontalInput);
    }
}
