using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //para el display de las vidas
using UnityEngine.SceneManagement;

public class Boost : MonoBehaviour
{
	public AudioSource audio;
	public AudioClip boostClip;
	public float rotationSpeed = 50f;

	public Shooting shooting;

    void Update() {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) {
    	if(col.gameObject.tag == "Ship") {
    		audio.PlayOneShot(boostClip, 1f);
    		if(gameObject.tag == "RPS") {
				shooting.RPS(1);
			}
			else if(gameObject.tag == "Shield") {
				col.gameObject.GetComponent<Live>().Shield(1);
			}
			else if(gameObject.tag == "Heal") {
				col.gameObject.GetComponent<Live>().Heal(3);
			}
			Destroy(gameObject);
		}
    }
}