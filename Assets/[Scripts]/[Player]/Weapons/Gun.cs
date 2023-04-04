using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    [Header("Ammo Parameters")]
    public int clipSize = 10;
    private int currentAmmo;
    private int stockAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    [SerializeField] private bool isPistol;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isRifle;

    [Header("Hipfire Recoil Parameters")]
    [SerializeField] public float recoilX;
    [SerializeField] public float recoilY;
    [SerializeField] public float recoilZ;

    [Header("ADS Recoil Parameters")]
    [SerializeField] public float aimRecoilX;
    [SerializeField] public float aimRecoilY;
    [SerializeField] public float aimRecoilZ;

    [Header("Settings")]
    [SerializeField] public float snappiness;
    [SerializeField] public float returnSpeed;

    [Header("Additional Variables")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    public TextMeshProUGUI text;
    public GameObject hitmarker;

    public ReloadAudio reloadAudio;

    // Internal Privates
    private float nextTimeToFire = 0f;

    private InputManager input;
    private Recoil recoil_script;
    private Animation anim;

    void Start()
    {
        input = InputManager.Instance;
        recoil_script = transform.Find("/-- Player --/FirstPersonController/CameraRotation/CameraRecoil").GetComponent<Recoil>();
        anim = gameObject.GetComponent<Animation>();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        GetAmmo();
        UpdateAmmoText();

        if (isReloading)
            return;

        if (stockAmmo > 0 && currentAmmo < clipSize && (currentAmmo <= 0 || input.GetReload()))
        {
            StartCoroutine(Reload());
            return;
        }

        if (input.GetShooting() && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        UpdateAmmo();
    }

    void GetAmmo()
    {
        if (isPistol)
        {
            currentAmmo = Variables.pistolMagAmmo;
            stockAmmo = Variables.pistolStockAmmo;
        }
        else if (isShotgun)
        {
            currentAmmo = Variables.shotgunMagAmmo;
            stockAmmo = Variables.shotgunStockAmmo;
        }
        else if (isRifle)
        {
            currentAmmo = Variables.rifleMagAmmo;
            stockAmmo = Variables.rifleStockAmmo;
        }
    }

    void UpdateAmmo()
    {
        if (isPistol)
        {
            Variables.pistolMagAmmo = currentAmmo;
            Variables.pistolStockAmmo = stockAmmo;
        }
        else if (isShotgun)
        {
            Variables.shotgunMagAmmo = currentAmmo;
            Variables.shotgunStockAmmo = stockAmmo;
        }
        else if (isRifle)
        {
            Variables.rifleMagAmmo = currentAmmo;
            Variables.rifleStockAmmo = stockAmmo;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        anim.Play();
        GetComponent<FMODUnity.StudioEventEmitter>().Play();

        recoil_script.RecoilFire();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target != null)
            {
                target.TakeDamage(damage);
                hitmarker.SetActive(true);
                Invoke("RemoveHitmarker", 0.1f);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    private void RemoveHitmarker()
    {
        hitmarker.SetActive(false);
    }

    public void AddAmmo(int ammoAmount)
    {
        stockAmmo += ammoAmount;
    }

    private void UpdateAmmoText()
    {
        text.text = $"{currentAmmo} / {stockAmmo}";
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (gameObject.tag == "Reload/MAG")
        {
            reloadAudio.ReloadType("Reload/MAG");
        }
        if (gameObject.tag == "Reload/SHOTGUN")
        {
            reloadAudio.ReloadType("Reload/SHOTGUN");
        }

        animator.SetBool("Reloading", true);
        reloadAudio.PlayReload();

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);

        int reloadAmount = clipSize - currentAmmo; // How many bullets to refill clip

        reloadAmount = (stockAmmo - reloadAmount) >= 0 ? reloadAmount : stockAmmo; // Check if enough bullets in stock
        currentAmmo += reloadAmount;
        stockAmmo -= reloadAmount;

        UpdateAmmo();
        isReloading = false;
    }
}