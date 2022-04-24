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
        //DetectOtherPlatforms();
    }

    public void MoveVertically()
    {
        rb.velocity = Vector2.up * Speed * Time.deltaTime;
    }

    public void DisablePlatform()
    {
        if (transform.position.y >= 5.4f)
        {
            if (gameObject.tag == "GoodPlatform")
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject);
    //    if (collision.gameObject.CompareTag("GoodPlatform"))
    //    {
    //        Debug.Log("Detection");
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodPlatform"))
        {
            collision.gameObject.SetActive(false);
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(leftSide.position, new Vector2(leftSide.position.x - 20f, leftSide.position.y));
        //Gizmos.DrawLine(rightSide.position, new Vector2(rightSide.position.x + 20f, rightSide.position.y));
    }
}
