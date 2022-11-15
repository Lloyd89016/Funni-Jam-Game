using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement001 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Vector3 Move;

    RaycastHit2D topHit;
    RaycastHit2D bottomHit;
    RaycastHit2D leftHit;
    RaycastHit2D rightHit;

    //there has to be a better way to do this...
    void Update()
    {
        

        Move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        topHit = Physics2D.Raycast(transform.position, transform.up, .55f, groundLayer);
        bottomHit = Physics2D.Raycast(transform.position, -transform.up, .55f, groundLayer);
        leftHit = Physics2D.Raycast(transform.position, -transform.right, .55f, groundLayer);
        rightHit = Physics2D.Raycast(transform.position, transform.right, .55f, groundLayer);

        if (topHit.collider == true){
            if(Move.y > .01f){
                Move.y = 0;
            }
        }        
        if(bottomHit.collider == true){
            if(Move.y < -.01f)
            {
                Move.y = 0;
            }
        }
        if(leftHit.collider == true){
            if(Move.x < -.01f)
            {
                Move.x = 0;
            }
        }        
        if(rightHit.collider == true){
            if(Move.x > .01f)
            {
                Move.x = 0;
            }
        }

        transform.position += Move * Time.deltaTime * moveSpeed;
    }
}