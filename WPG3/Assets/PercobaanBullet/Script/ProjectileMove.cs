using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileMove : MonoBehaviour {

    public float speed;
    public float WaterRate;

    void Start() {

    }

    void Update() {
        if (speed != 0) {
            transform.position += transform.forward * (speed * Time.deltaTime);
        } else {
            Debug.Log("No Speed");
        }
    }

    private void OnCollisionEnter(Collision co) {
        // Baris ini akan mencetak nama objek yang ditabrak ke layar Console
        Debug.Log("Peluru menabrak: " + co.gameObject.name);

        speed = 0;
        Destroy(gameObject);
    }
}