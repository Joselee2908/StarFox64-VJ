using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //para el display de las vidas

public class Asteroid : MonoBehaviour
{
    public GameObject model;
	public Text scoreText;
	public AudioSource audio;
	public int lives = 1;
    public ParticleSystem boom;

    private Collider SelfCollider;
    private bool alive = true;

    private float xAngle = 0f;
    private float yAngle = 0f;
    private float zAngle = 0f;

    void Start() {
        alive = true;
        SelfCollider = GetComponent<Collider>();
        xAngle = Random.Range(-0.1f, 0.1f);
        yAngle = Random.Range(-0.1f, 0.1f);
        zAngle = Random.Range(-0.1f, 0.1f);
    }

    void Update() {
        if(alive) transform.Rotate(xAngle, yAngle, zAngle);
    }

    void OnCollisionEnter(Collision col) {
    	if(col.gameObject.tag == "Ship") {
            DestroyEnemy();
        }
        else if(col.gameObject.tag == "Shot") {
            Destroy(col.gameObject);
    		if(lives > 1) {
                audio.Play();
    			--lives;
    		}
    		else {
	    		Destroy(col.gameObject);
                DestroyEnemy();
	    		Live.score = Live.score + 10;
	    		scoreText.text = "Score: " + Live.score.ToString();
    		}
    	}
    }

    public void DestroyEnemy() {
        audio.Play();
        alive = false;
        SelfCollider.enabled = false;
        boom.Play();
        Destroy(model);
    }
}
