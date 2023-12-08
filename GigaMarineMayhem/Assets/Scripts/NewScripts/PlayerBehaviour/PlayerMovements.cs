using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashDistance = 5f; 
    public float dashDuration = 0.5f; 
    public float dashCooldown = 2f;

    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    private bool isDashing = false;
    private bool canDash = true;

    void Update()
    {
        if (!isDashing)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        
        Vector2 startPosition = rb.position;
        Vector2 dashDestination = startPosition + movement.normalized * dashDistance;

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            
            rb.MovePosition(Vector2.Lerp(startPosition, dashDestination, (Time.time - startTime) / dashDuration));
            yield return null;
        }

       
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
