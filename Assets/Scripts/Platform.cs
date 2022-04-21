using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveVertically();
        DisablePlatform();
    }

    public void MoveVertically()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 5.1f), Speed * Time.deltaTime);
        rb.velocity = Vector2.up * Speed * Time.deltaTime;
    }

    public void DisablePlatform()
    {
        if (transform.position.y >= 5.4f)
        {
            gameObject.SetActive(false);
        }
    }
}
