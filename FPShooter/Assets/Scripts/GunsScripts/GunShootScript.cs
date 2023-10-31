using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GunShootScript : MonoBehaviour
{
    [Header("Settings of shoot")]

    [SerializeField] float FireRate;
    [SerializeField] int Damage;
    [SerializeField] float RangeOfHit;

    [Header("Other")]

    [SerializeField] Camera Camera;
    [SerializeField] ParticleSystem Particle;

    [Header("Ammo and Reload")]

    [SerializeField] int Mag;
    [SerializeField] int Ammo;
    [SerializeField] int AmmoMag;

    [Header("UI")]

    [SerializeField] TextMeshProUGUI MagText;
    [SerializeField] TextMeshProUGUI AmmoText;

    [Header("Animation")]

    [SerializeField] Animation Animation;
    [SerializeField] AnimationClip ReloadClip;

    [Header("Recoile Settings")]
    [Range(0f, 1f)]
    [SerializeField] float RecoverPercent = 0.7f;

    [SerializeField] float RecoileUp = 1f;
    [SerializeField] float RecoileBack = 0f;

    Vector3 OriginalPosition;
    Vector3 RecoileVelocity = Vector3.zero;

    float RecoilLenght;
    float RecoverLenght;

    bool Recoiling;
    bool Recovering;

    float NextFire;
    bool Hitted;
    private void Start()
    {
        MagText.text = Mag.ToString();
        AmmoText.text = Ammo + "/" + AmmoMag;

        OriginalPosition = transform.localPosition;

        RecoilLenght = 0F;
        RecoverLenght = 1/FireRate * RecoverPercent;
    }
    private void Update()
    {
        if(NextFire > 0)
        {
            NextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && NextFire <= 0 && Ammo > 0 && Animation.isPlaying == false)
        {
            NextFire = 1 / FireRate;

            Ammo--;

            MagText.text = Mag.ToString();
            AmmoText.text = Ammo + "/" + AmmoMag;

            Fire();
        }
        if(Input.GetKeyDown(KeyCode.R) && Ammo != AmmoMag && Mag > 0)
        {
             ReloadAmmo();
        }
        if(Recoiling)
        {
            Recoile();
        }
        if(Recovering)
        {
            Recover();
        }
    }
    private void ReloadAmmo()
    {
        Animation.Play(ReloadClip.name);
        if(Mag > 0)
        {
            Mag--;
            Ammo = AmmoMag;
        }
        MagText.text = Mag.ToString();
        AmmoText.text = Ammo + "/" + AmmoMag;
    }
    private void Fire()
    {
        Recoiling = true;
        Recovering = false;

        Particle.Play();

        Ray ray = new Ray(Camera.transform.position,Camera.transform.forward);

        RaycastHit hit;

        Hitted = Physics.Raycast(ray.origin, ray.direction, out hit, RangeOfHit);
        if(Hitted)
        {
            if(hit.transform.gameObject.GetComponent<PlayerHealth>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, Damage);
            }
        }
    }
    void Recoile()
    {
        Vector3 FinalPosition =
            new Vector3(OriginalPosition.x,OriginalPosition.y + RecoileUp,OriginalPosition.z - RecoileBack);

        transform.localPosition = 
            Vector3.SmoothDamp(transform.localPosition, FinalPosition, ref RecoileVelocity, RecoilLenght);

        if(transform.localPosition == FinalPosition)
        {
            Recoiling = false;
            Recovering = true;
        }
    }
    void Recover()
    {
        Vector3 FinalPosition = OriginalPosition;

        transform.localPosition = 
            Vector3.SmoothDamp(transform.localPosition, FinalPosition, ref RecoileVelocity, RecoverLenght);

        if(transform.localPosition == FinalPosition)
        {
            Recoiling = false;
            Recovering = false;
        }
    }
}
