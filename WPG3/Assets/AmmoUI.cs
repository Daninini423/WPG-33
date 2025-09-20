using UnityEngine;
using TMPro;    

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private ActionStateManager actions; // referensi ke ActionStateManager
    [SerializeField] private TMP_Text ammoText;              // referensi ke UI Text

    void Update()
    {
        if (actions != null && actions.ammo != null)
        {
            // Tampilkan "currentAmmo / extraAmmo"
            ammoText.text = actions.ammo.currentAmmo + " / " + actions.ammo.extraAmmo;
        }
    }
}
