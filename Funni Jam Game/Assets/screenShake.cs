using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class screenShake : MonoBehaviour
{
    public UnityEvent explode;
    public UnityEvent shoot;

    public void ShakeEventShoot()
    {
        //cupcake shake
        shoot.Invoke();

    }

    public void ShakeEventExplode()
    {
        //explode shake
        explode.Invoke();
      

    }
}
