using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float forceAmount = 2f;
    private float angularChangeInDegrees = 15f;
    private Rigidbody2D rb;

    private float maxAngularVelocity = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        float hMovement = Input.GetAxisRaw("Horizontal");
        float vMovement = Input.GetAxisRaw("Vertical");

        if (hMovement != 0 || vMovement != 0)
        {
            Vector2 force = new Vector2(hMovement, vMovement);
            force = forceAmount * Time.deltaTime * force.normalized;

            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void Rotation()
    {
        float turn = Input.GetAxis("Rotation");
        if (turn != 0)
        {
            var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * rb.inertia * turn;

            rb.AddTorque(impulse, ForceMode2D.Impulse);
            ClampAngularVelocity();
        }
    }

    void ClampAngularVelocity()
    {
        if (rb.angularVelocity < -maxAngularVelocity) { rb.angularVelocity = -maxAngularVelocity; }
        if (rb.angularVelocity > maxAngularVelocity) { rb.angularVelocity = maxAngularVelocity; }
    }

}
