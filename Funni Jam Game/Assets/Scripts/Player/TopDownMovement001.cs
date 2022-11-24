using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement001 : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    float moveLimiter = 0.7f;

    [SerializeField] float runSpeed = 20.0f;

    [SerializeField] Animator playerAnimator;

    bool isDashing;

    [SerializeField] float dashingCooldown;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false)
        {         
            runSpeed *= 3;
            playerAnimator.SetBool("isWalking", false);
            Invoke("StopDashing", .3f);
            isDashing = true;
        }

        if(isDashing == false)
        {
            Animation();
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void Animation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    void StopDashing()
    {
        runSpeed /= 3;
        Invoke("ChangeIsDashingVariable", dashingCooldown);
    }

    void ChangeIsDashingVariable()
    {
        isDashing = false;
    }
}