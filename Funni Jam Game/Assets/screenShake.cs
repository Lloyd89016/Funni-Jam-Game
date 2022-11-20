using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class screenShake : MonoBehaviour
{
    public UnityEvent explode;
    public UnityEvent shoot;

    public void ShakeEvent()
    {
        //explode shake
        explode.Invoke();
        //cupcake shake
        shoot.Invoke();

    }
}
