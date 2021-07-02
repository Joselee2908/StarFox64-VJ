using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //para el display de las vidas

public class Ruins : MonoBehaviour
{
    public float moveSpeed = 5;

    private Collider SelfCollider;
    private bool alive;

    void Start() {
        SelfCollider = GetComponent<Collider>();
        alive = true;
    }

    void Update() {
        if(!alive) {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
            moveSpeed += 0.1f;
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Shot") Destroy(col.gameObject);
        else if(col.gameObject.tag == "Ship") {
            alive = false;
            Invoke("DestroyEnemy",4f);
        }
    }

    void DestroyEnemy() {
        Destroy(gameObject);
    }
}
