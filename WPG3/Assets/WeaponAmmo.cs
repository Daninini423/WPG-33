using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector] public int currentAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    void Start()
    {
        currentAmmo = clipSize;
    }


    public void Reload()
    {
        // Hitung berapa peluru yang dibutuhkan untuk penuh
        int ammoNeeded = clipSize - currentAmmo;

        // Ambil peluru dari extraAmmo, tapi jangan sampai lebih dari extraAmmo
        int ammoToReload = Mathf.Min(ammoNeeded, extraAmmo);

        // Update nilai peluru
        currentAmmo += ammoToReload;
        extraAmmo -= ammoToReload;

        // Pastikan extraAmmo tidak pernah negatif
        extraAmmo = Mathf.Max(0, extraAmmo);
    }

}
