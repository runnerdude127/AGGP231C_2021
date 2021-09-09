using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    bool canJump;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("grounded");
            canJump = true;
        }
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        rb.velocity = new Vector3((rb.velocity.x / 2), rb.velocity.y, rb.velocity.z);
    }

    public void playerJump()
    {        
        if(canJump == true)
        {
            canJump = false;
            Vector3 direction = Vector3.up * jumpHeight;
            Debug.Log("button button button" + direction);
            rb.AddForce(direction * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            Debug.Log("cannot jump");
        }
    }

}
