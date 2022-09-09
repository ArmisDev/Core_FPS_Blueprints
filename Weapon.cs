using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //Weapon Aspects
    [Header("Weapon Aspects")]
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;
    public bool weaponCanFire = true;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    //Shoot FX
    [Header("Shoot FX")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource ReloadMagDrop;
    [SerializeField] AudioSource ReloadInsertMag;
    public AudioClip dropMag;
    public AudioClip insertMag;
    public GameObject muzzlelight;

    [Header("Weapon Animations")]
    [SerializeField] private Animator weaponAnimations;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ReloadMagDrop = GetComponent<AudioSource>();
        ReloadInsertMag = GetComponent<AudioSource>();
        GameObject muzzlelight = gameObject.GetComponent<GameObject>();
        weaponCanFire = true;
    }

    void Update()
    {
        WeaponReload();

        if (weaponCanFire && Input.GetButton("Fire1"))
        {
            StartCoroutine(ShootMain());
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            GetComponent<Animator>().SetBool("Pistol Fire", false);
            GetComponent<Animator>().SetBool("Pistol Idle", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("Pistol Idle", true);
        }
    }

    private void OnEnable()
    {
        weaponCanFire = true;
    }

#region - Handle FX -

    void ShootFX()
    {
        audioSource.PlayOneShot(gunShot);
        muzzleFlash.Play();

        //FYI the muzzle flash light source is handled in the WeaponMain function
    }

#endregion

#region - Handle Weapon Behavior -

    void HandleWeaponFire()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            CreateHitEffect(hit);
            if(target == null) return;
            target.TakeDamage(damage);
        }

        else
        {
            return;
        }
    }

    void WeaponReload()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Animator>().SetBool("Pistol Reloading", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("Pistol Reloading", false);
            return;
        }
    }

#region - Reload Animation Events -

    void ReloadSlide()
    {
        audioSource.PlayOneShot(dropMag);
    }

    void ReloadInsert()
    {
        audioSource.PlayOneShot(insertMag);
    }

#endregion


    void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .5f);
    }

    IEnumerator ShootMain()
    {

        weaponCanFire = false;

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ShootFX();  
            HandleWeaponFire();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            //muzzlelight.SetActive(true);

            //Triggers fire animation. Idle animation trigger is in HandleWeaponFire method.
            GetComponent<Animator>().SetBool("Pistol Fire", true);
            GetComponent<Animator>().SetBool("Pistol Idle", false);
            
        }

        yield return new WaitForSeconds(timeBetweenShots);

        // GetComponent<Animator>().SetBool("Pistol Fire", false);
        // GetComponent<Animator>().SetBool("Pistol Idle", true);

        weaponCanFire = true;

        // else
        // {
        //     //muzzlelight.SetActive(false);
        //     return;
        // }
    }

    public void StartReloadCheck()
    {
        weaponCanFire = false;
    }

    public void EndReloadCheck()
    {
        if(!weaponCanFire)
        {
            weaponCanFire = true;
            return;
        }
    }

#endregion
}
