using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private bool isShootingRoutineActive = false;

    public TextMeshProUGUI ammoText;

    public Image shotgunImage;
    public Image lmgImage;

    private bool isInfiniteAmmoActive = false;

    void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }

        if (Input.GetButtonDown("Fire1") && !isShooting && !isReloading)
        {
            if (currentWeaponIndex == 0)
            {
                isShooting = true;
                StartCoroutine(ContinuousLMGFire());
            }
            else if (currentWeaponIndex == 1 && !isShootingRoutineActive)
            {
                isShooting = true;
                StartCoroutine(ShootRoutine());
            }
        }
        else if (Input.GetButtonUp("Fire1") && currentWeaponIndex == 0)
        {
            isShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void SwitchWeapon(int index)
    {
        currentWeaponIndex = Mathf.Clamp(index, 0, weapons.Length - 1);
        currentAmmo = weapons[currentWeaponIndex].maxAmmo;
        UpdateAmmoText();

        shotgunImage.gameObject.SetActive(currentWeaponIndex == 0);
        lmgImage.gameObject.SetActive(currentWeaponIndex == 1);
    }

    IEnumerator ShootRoutine()
    {
        isShootingRoutineActive = true;

        Debug.Log("Playing Shooting Sound");

        weapons[currentWeaponIndex].shootingAudioSource.Play();

        Shoot();

        if (currentAmmo <= 0)
        {
            yield return StartCoroutine(Reload());
        }

        isShootingRoutineActive = false;
        isShooting = false;
    }

    IEnumerator ContinuousLMGFire()
    {
        weapons[currentWeaponIndex].shootingAudioSource.Play();

        while (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
            yield return new WaitForSeconds(weapons[currentWeaponIndex].shootingInterval);
        }

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

        weapons[currentWeaponIndex].reloadAudioSource.Play();

        yield return new WaitForSeconds(weapons[currentWeaponIndex].reloadTime);
        currentAmmo = weapons[currentWeaponIndex].maxAmmo;
        isReloading = false;
        UpdateAmmoText();
    }

    void Shoot()
    {
        if (!isInfiniteAmmoActive)
        {
            currentAmmo--;
        }

        UpdateAmmoText();

        if (currentWeaponIndex == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                float angle = Random.Range(-15f, 15f);
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 bulletDirection = rotation * weapons[currentWeaponIndex].firePoint.up;

                GameObject bullet = Instantiate(weapons[currentWeaponIndex].bulletPrefab, weapons[currentWeaponIndex].firePoint.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletDirection * weapons[currentWeaponIndex].bulletForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            GameObject bullet = Instantiate(weapons[currentWeaponIndex].bulletPrefab, weapons[currentWeaponIndex].firePoint.position, weapons[currentWeaponIndex].firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(weapons[currentWeaponIndex].firePoint.up * weapons[currentWeaponIndex].bulletForce, ForceMode2D.Impulse);
        }
    }

    void UpdateAmmoText()
    {
        if (isInfiniteAmmoActive)
        {
            ammoText.text = "Ammo: Infinite";
        }
        else
        {
            ammoText.text = "Ammo: " + currentAmmo.ToString();
        }
    }

    public void EnableInfiniteAmmo()
    {
        isInfiniteAmmoActive = true;
        UpdateAmmoText();
    }

    public void DisableInfiniteAmmo()
    {
        isInfiniteAmmoActive = false;
        UpdateAmmoText();
    }
}
