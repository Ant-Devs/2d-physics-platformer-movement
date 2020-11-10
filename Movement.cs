using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /*
     This work is licensed under the Creative 
     Commons Attribution 4.0 International 
     License. To view a copy of this license, 
     visit 
     http://creativecommons.org/licenses/by/4.0/ 
     or send a letter to Creative Commons, PO Box 
     1866, Mountain View, CA 94042, USA.

     This code was made by Natalius of Ant Devs
     Illustrate with Ants#6552
     https://ant-devs.itch.io/
    */

    //The player must have a collider2d and a rigidbody
    //The platform must have it's own tag
    Rigidbody2D rb;
    public float speed = 600; //The player's speed
    public float jumpPower = 12500; //The player's force applied when jumping
    public float speedLimit = 5; //The player's speed limit
    public string platformTag; //The platform game object's tag, used to determine if jumping is enabled
    Vector2 movement;
    bool grounded;
    int space = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("jump");
        }
        if (rb.velocity.x > speedLimit)
        {
            rb.velocity = new Vector2(speedLimit, rb.velocity.y);
        }
        if (rb.velocity.x < -speedLimit)
        {
            rb.velocity = new Vector2(-speedLimit, rb.velocity.y);
        }
    }

    IEnumerator jump()
    {
        space = 1;
        yield return new WaitForSeconds(0.01f);
        space = 0;
    }

    void FixedUpdate()
    {
        if(space == 1 && grounded == true)
        {
            if(rb.velocity.x != 0)
            {
                rb.AddForceAtPosition(new Vector2(0, (jumpPower * 2) * Time.fixedDeltaTime), Vector2.up);
            }
            else
            {
                rb.AddForceAtPosition(new Vector2(0, jumpPower * Time.fixedDeltaTime), Vector2.up);
            }
        }
        if (movement.x > 0.1f)
        {
            rb.AddForceAtPosition(new Vector2(rb.position.x + movement.x * speed * Time.fixedDeltaTime, 0), Vector2.right);
        }
        if (movement.x < -0.1f)
        {
            rb.AddForceAtPosition(new Vector2(rb.position.x + movement.x * speed * Time.fixedDeltaTime, 0), Vector2.left);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(platformTag))
        {
            grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(platformTag))
        {
            grounded = false;
        }
    }
}
