using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 12;
    [SerializeField] AmmoType ammoType;
    public AudioClip pickupAudioClip;
    [SerializeField] private AudioSource pickupAudio;

    private void start()
    {
        pickupAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            pickupAudio.PlayOneShot(pickupAudioClip);
            Destroy(gameObject, 0.3f);
        }
    }
}
