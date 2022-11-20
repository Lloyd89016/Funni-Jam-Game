using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevButtonChangeWeapon : MonoBehaviour
{
    public Shoot shoot;
    public GameObject CupCake;
    public GameObject Doughnut;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shoot.projectile = Doughnut;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            shoot.projectile = CupCake;
        }
    }
}
