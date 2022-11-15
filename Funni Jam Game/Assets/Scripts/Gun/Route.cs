using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints;

    public Transform gunPos;

    private Vector2 gizmosPosition;

    public void Setup(Vector2 endPoint)
    {
        //Moves all of the control points to their correct locations
        controlPoints[0].position = gunPos.position;

        controlPoints[3].position = endPoint;

        float offset = .2f + (Vector2.Distance(controlPoints[0].position, controlPoints[3].position) / 10);
        Debug.Log(offset);

        Vector2 midPoint = MidPoint(controlPoints[0].gameObject, controlPoints[3].gameObject);
        controlPoints[1].position = new Vector2(midPoint.x, midPoint.y + offset);
        controlPoints[2].position = new Vector2(midPoint.x, midPoint.y + offset);
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

    }

    Vector2 MidPoint(GameObject objectOne, GameObject objectTwo)
    {
        float midPointX = objectOne.transform.position.x + (objectTwo.transform.position.x - objectOne.transform.position.x) / 2;
        float midPointY = objectOne.transform.position.y + (objectTwo.transform.position.y - objectOne.transform.position.y) / 2;
        return new Vector2(midPointX, midPointY);
    }
}