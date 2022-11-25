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
    bool canDash = true;

    [SerializeField] float dashCooldown;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashLength;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
            Dash();
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
    
    void Dash()
    {
        canDash = false;
        isDashing = true;
        runSpeed *= dashSpeed;
        playerAnimator.SetBool("isWalking", false);
        Invoke("StopDashing", dashLength);
    }

    void StopDashing()
    {
        StartCoroutine(LerpNumber(runSpeed, runSpeed /= dashSpeed, 1000f));
        isDashing = false;

        //Waits to let the player dash again
        Invoke("LetPlayerDash", dashCooldown);
    }

    void LetPlayerDash()
    {
        canDash = true;
    }

    IEnumerator LerpNumber(float valueToChange, float endValue, float duration)
    {
        float time = 0;
        float startValue = valueToChange;
        while (time < duration)
        {
            valueToChange = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        valueToChange = endValue;
    }
}