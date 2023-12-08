using System.Collections;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [System.Serializable]
    public class Weapon
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        public int maxAmmo = 10;
        public float bulletForce = 20f;
        public float shootingInterval = 0.5f;
        public float reloadTime = 4f;
        public AudioSource shootingAudioSource;
        public AudioSource reloadAudioSource;
    }

    public Weapon[] weapons;
    private int currentWeaponIndex = 0;

    private int currentAmmo;
    private bool isShooting = false;
    private bool isReloading = false;

    private bool isShootingRoutineActive = false; // Add this flag

    public TextMeshProUGUI ammoText;

    void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // LMG
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Shotgun
        {
            SwitchWeapon(1);
        }
        // Add more conditions for other weapon keys if needed

        if (Input.GetButtonDown("Fire1") && !isShooting && !isReloading)
        {
            if (currentWeaponIndex == 0) // LMG
            {
                isShooting = true;
                StartCoroutine(ContinuousLMGFire());
            }
            else if (currentWeaponIndex == 1 && !isShootingRoutineActive) // Shotgun
            {
                isShooting = true;
                StartCoroutine(ShootRoutine());
            }
        }
        else if (Input.GetButtonUp("Fire1") && currentWeaponIndex == 0) // Stop LMG fire on button release
        {
            isShooting = false;
        }
    }

    void SwitchWeapon(int index)
    {
        currentWeaponIndex = Mathf.Clamp(index, 0, weapons.Length - 1);
        currentAmmo = weapons[currentWeaponIndex].maxAmmo;
        UpdateAmmoText();
    }

    IEnumerator ShootRoutine()
    {
        // Set the flag to indicate the routine is active
        isShootingRoutineActive = true;

        // Debug statement
        Debug.Log("Playing Shooting Sound");

        // Play shooting sound once
        weapons[currentWeaponIndex].shootingAudioSource.Play();

        // Fire a single bullet for the shotgun
        Shoot();


        if (currentAmmo <= 0)
        {
            yield return StartCoroutine(Reload());
        }

        // Reset the flag when the routine is finished
        isShootingRoutineActive = false;
        isShooting = false; // Reset the isShooting flag
    }






    IEnumerator ContinuousLMGFire()
    {
        // Play shooting sound continuously while shooting
        weapons[currentWeaponIndex].shootingAudioSource.Play();

        while (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
            yield return new WaitForSeconds(weapons[currentWeaponIndex].shootingInterval);
        }

        // Stop shooting sound
        weapons[currentWeaponIndex].shootingAudioSource.Stop();

        if (currentAmmo <= 0)
        {
            yield return StartCoroutine(Reload());
        }

        isShooting = false;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Reloading...";

        // Play reload sound
        weapons[currentWeaponIndex].reloadAudioSource.Play();

        yield return new WaitForSeconds(weapons[currentWeaponIndex].reloadTime);
        currentAmmo = weapons[currentWeaponIndex].maxAmmo;
        isReloading = false;
        UpdateAmmoText();
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoText();

        if (currentWeaponIndex == 1) // Shotgun
        {
            for (int i = 0; i < 10; i++) // Adjust the number of bullets in the cone as needed
            {
                float angle = Random.Range(-15f, 15f); // Adjust the spread angle as needed
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 bulletDirection = rotation * weapons[currentWeaponIndex].firePoint.up;

                GameObject bullet = Instantiate(weapons[currentWeaponIndex].bulletPrefab, weapons[currentWeaponIndex].firePoint.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletDirection * weapons[currentWeaponIndex].bulletForce, ForceMode2D.Impulse);
            }
        }
        else // Other weapons (e.g., LMG)
        {
            // Only instantiate one bullet for other weapons
            GameObject bullet = Instantiate(weapons[currentWeaponIndex].bulletPrefab, weapons[currentWeaponIndex].firePoint.position, weapons[currentWeaponIndex].firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(weapons[currentWeaponIndex].firePoint.up * weapons[currentWeaponIndex].bulletForce, ForceMode2D.Impulse);
        }
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }
    
}