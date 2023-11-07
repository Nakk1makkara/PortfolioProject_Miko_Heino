using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject pfBullet;
    [SerializeField] private Transform Muzzle;
    [SerializeField] private PlayerAimWeapon playerAimWeapon; 

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerShootProjectiles_OnShoot();
        }
    }

    private void PlayerShootProjectiles_OnShoot()
    {
        Vector3 aimDirection = playerAimWeapon.aimDirection.normalized;
        GameObject bullet = Instantiate(pfBullet, Muzzle.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetInitialDirection(aimDirection);
        }
    }
}
