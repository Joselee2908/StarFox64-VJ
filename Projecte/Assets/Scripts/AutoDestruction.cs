using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
	public float shotSpeed = 50f;

    void Start() {
        Destroy(gameObject, 3f);
    }

    void Update() {
        transform.Translate(Vector3.forward * shotSpeed * Time.deltaTime);
    }
}
