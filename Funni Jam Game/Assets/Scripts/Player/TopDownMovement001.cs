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

    float width;
    float height;

    //there has to be a better way to do this...

    private void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        Move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        topHit = Physics2D.Raycast(transform.position, transform.up, height/2, groundLayer);
        bottomHit = Physics2D.Raycast(transform.position, -transform.up, height/2, groundLayer);
        leftHit = Physics2D.Raycast(transform.position, -transform.right, width/2, groundLayer);
        rightHit = Physics2D.Raycast(transform.position, transform.right, width/2, groundLayer);

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