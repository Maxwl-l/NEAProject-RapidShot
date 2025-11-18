using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dash Settings")]
    public float dashForce = 20f;
    public float dashUpwardForce = 0f;
    public float dashDuration = 0.2f;

    [Header("Cooldown")]
    public float dashCd = 1f;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    private Vector3 dashDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            TryDash();
        }

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime; //decrements "dashCdTimer" at the same rate regardless of how fast unity is running, can go past 0 but doesn't really matter
    }

    private void TryDash()
    {
        if (dashCdTimer > 0) return; //if the timer is still active, then no dash, otherwise dash is allowed as long as 'dashCdTimer' is below 0

        dashCdTimer = dashCd;
        StartCoroutine(DashRoutine()); // calls to DashRoutine() function
    }

    private IEnumerator DashRoutine() //The IEnumerator is used for the "yield return". Essentially the term is used to create a coroutine.

        // A coroutine in this case is used to pause the function until a condition is fulfilled.
        // In 'DashRoutine()', the 'yield return' is the point at which the function pauses - until "dashDuration" is done, then it resumes.
        // This is what lets the player obj basically "glide" along the y axis for a second before dropping again.
    {
        pm.dashing = true;
        rb.useGravity = false;

        // Get movement input
        float HInput = Input.GetAxisRaw("Horizontal");
        float VInput = Input.GetAxisRaw("Vertical");

        dashDirection = orientation.forward * VInput + orientation.right * HInput;
        if (dashDirection == Vector3.zero) // If the player does not input a direction of movement, the player obj will automatically dash forward.
            dashDirection = orientation.forward;

        dashDirection.Normalize();

        // Reset velocity so the dash is consistent
        rb.linearVelocity = Vector3.zero;

        // Apply dash instantly
        rb.AddForce(dashDirection * dashForce + orientation.up * dashUpwardForce, ForceMode.VelocityChange);

        // Wait for dash duration - this is during when player is 'gliding' in the air.
        yield return new WaitForSeconds(dashDuration);

        // Stop dash smoothly - removes added momentum by just assigning a new, lower value
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.5f, rb.linearVelocity.y, rb.linearVelocity.z * 0.5f);

        rb.useGravity = true;
        pm.dashing = false;
    }
}
