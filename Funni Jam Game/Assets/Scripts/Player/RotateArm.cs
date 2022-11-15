using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    private Vector3 diff;
    public float rotZ;

    void Update()
    {
        //It makes the arm point towards the mouse
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //normalize difference  
        diff.Normalize();

        //calculate rotation
        rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);

        //Flips the arm
        if(rotZ < 89 && rotZ > -89)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
