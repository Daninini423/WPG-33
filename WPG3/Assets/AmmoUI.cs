using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private ActionStateManager actions; // referensi ke ActionStateManager
    [SerializeField] private TMP_Text ammoText;          // referensi ke UI Text
    public bool unlimitedExtraAmmo = true;               // toggle bisa diatur di Inspector

    void Update()
    {
        if (actions != null && actions.ammo != null)
        {
            if (unlimitedExtraAmmo)
            {
                // currentAmmo / ∞
                ammoText.text = actions.ammo.currentAmmo + " / ∞";
            }
            else
            {
                // currentAmmo / extraAmmo biasa
                ammoText.text = actions.ammo.currentAmmo + " / " + actions.ammo.extraAmmo;
            }
        }
    }
}
