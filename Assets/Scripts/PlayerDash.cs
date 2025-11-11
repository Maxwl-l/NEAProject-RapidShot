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
            dashCdTimer -= Time.deltaTime;
    }

    private void TryDash()
    {
        if (dashCdTimer > 0) return;

        dashCdTimer = dashCd;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        pm.dashing = true;
        rb.useGravity = false;

        // Get movement input
        float HInput = Input.GetAxisRaw("Horizontal");
        float VInput = Input.GetAxisRaw("Vertical");

        dashDirection = orientation.forward * VInput + orientation.right * HInput;
        if (dashDirection == Vector3.zero)
            dashDirection = orientation.forward;

        dashDirection.Normalize();

        // Reset velocity so the dash is consistent
        rb.linearVelocity = Vector3.zero;

        // Apply dash instantly
        rb.AddForce(dashDirection * dashForce + orientation.up * dashUpwardForce, ForceMode.VelocityChange);

        // Wait for dash duration
        yield return new WaitForSeconds(dashDuration);

        // Stop dash smoothly
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.5f, rb.linearVelocity.y, rb.linearVelocity.z * 0.5f);

        rb.useGravity = true;
        pm.dashing = false;
    }
}
