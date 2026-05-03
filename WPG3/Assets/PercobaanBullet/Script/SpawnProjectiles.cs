using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {

    public GameObject WaterPoint;
    public List<GameObject> vfx = new List<GameObject> ();
    public RotateToMouse rotateToMouse;

    private GameObject effectToSpawn;
    private float timeToWater = 0;

    void Start () {
        effectToSpawn = vfx[0];
    }
    void Update() {
        if(Input.GetMouseButton (0) && Time.time >= timeToWater) {

            Debug.Log("Klik mouse terdeteksi!"); // Tambahkan ini

            timeToWater = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().WaterRate;
            SpawnVFX();
        }
    }

    void SpawnVFX () {
        GameObject vfx;

        if (WaterPoint != null) {
            vfx = Instantiate (effectToSpawn, WaterPoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null) {
                vfx.transform.localRotation = rotateToMouse.GetRotation();
                }
        } else {
            Debug.Log("No Water Point");
        }   
    }
}
