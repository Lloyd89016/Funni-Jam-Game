using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement001 : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    [SerializeField] float runSpeed = 20.0f;
    float startingSpeed;

    [SerializeField] Animator playerAnimator;

    bool isDashing;
    bool canDash = true;

    [SerializeField] float dashCooldown;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashLength;

    [SerializeField] TrailRenderer trailRenderer;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startingSpeed = runSpeed;
    }

    void Update()
    {
        if(isDashing == false)
        {
            // Gives a value between -1 and 1
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            StartDash();
        }

        if(isDashing == false)
        {
            Animation();
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0 && isDashing == false) // Check for diagonal movement
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
    
    void StartDash()
    {
        canDash = false;
        isDashing = true;
        //Stops the walk animation
        playerAnimator.SetBool("isWalking", false);

        trailRenderer.enabled = true;

        //Dash Movement
        StopAllCoroutines();
        runSpeed = startingSpeed;
        StartCoroutine(Dash(dashLength / 2));
    }

    IEnumerator Dash(float duration)
    {
        //Speeds up
        float endValue = runSpeed *= dashSpeed;

        float time = 0;
        float startValue = runSpeed /= dashSpeed;
        while (time < duration)
        {
            runSpeed = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        runSpeed = endValue;

        //Slows down
        endValue = runSpeed /= dashSpeed;

        time = 0;
        startValue = runSpeed *= dashSpeed;
        while (time < duration)
        {
            runSpeed = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        runSpeed = endValue;

        StopDashing();
    }

    void StopDashing()
    {
        trailRenderer.enabled = false;
        isDashing = false;

        //Waits to let the player dash again
        Invoke("LetPlayerDash", dashCooldown);
    }

    void LetPlayerDash()
    {
        canDash = true;
    }
}