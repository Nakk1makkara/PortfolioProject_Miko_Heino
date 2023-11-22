using System.Collections;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float bulletForce = 20f;
    public float shootingInterval = 0.5f;
    public float reloadTime = 4f;

    private bool isShooting = false;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText; // Reference to the TextMeshProUGUI component on your HUD

    public AudioSource shootingAudioSource; // Reference to the AudioSource component for shooting sound
    public AudioSource reloadAudioSource;   // Reference to the AudioSource component for reload sound

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting && !isReloading)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    IEnumerator ShootRoutine()
    {
        isShooting = true;

        // Play shooting sound continuously while shooting
        shootingAudioSource.Play();

        while (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval);
        }

        // Stop shooting sound
        shootingAudioSource.Stop();

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
        reloadAudioSource.Play();

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoText();
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoText();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }
}
