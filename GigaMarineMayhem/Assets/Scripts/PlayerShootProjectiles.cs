using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;


   private void Awake () {
    GetComponent<CharacterAim_Base>().OnShoot += PlayerShootProjectiles_OnShoot;
   }

   private void PlayerShootProjectiles_OnShoot(object sender, CharacterAim_Base.OnShootEventArgs e) {
    
    Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);

   }
}
