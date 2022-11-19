using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class screenShake : MonoBehaviour
{
    public UnityEvent shake;

    public void ShakeEvent()
    {
        shake.Invoke();
    }
}
