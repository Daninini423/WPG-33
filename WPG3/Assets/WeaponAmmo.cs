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
        int ammoNeeded = clipSize - currentAmmo;

        // isi penuh tanpa mengurangi extraAmmo
        currentAmmo += ammoNeeded;

        // kalau mau tetap tampil 120 terus, jangan ubah extraAmmo
    }

}
