using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{
    [SerializeField] MonoBehaviour scriptToDisable;
    [SerializeField] float timeBeforeRenable;

    //When the object gets hit by an explotion, its movement script gets disabled so that it can be moved by explotions
    public void DisableScript()
    {
        scriptToDisable.enabled = false;
        Invoke("EnableScript", timeBeforeRenable);
    }

    void EnableScript()
    {
        scriptToDisable.enabled = true;
    }
}
