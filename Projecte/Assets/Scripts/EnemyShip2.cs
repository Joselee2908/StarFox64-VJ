using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShip2 : MonoBehaviour
{
    public GameObject model;
	public Text scoreText;
	public int lives = 1;
    public AudioSource audioBoom;
    public ParticleSystem effectBoom;
    
    public Transform target;
    public float moveSpeed = 5f;
    public float distance = 50f;

    private Collider SelfCollider;
    private int state = 0;
    //0 => desactivado
    //1 => desactivado i no va a volver
    //2 => vivo
    //3 => muerto

    void Start() {
        SelfCollider = GetComponent<Collider>();
        state = 0;
    }

    void Update() {
    	if(state != 3 && state != 1) transform.LookAt(target);
    	if(state == 2 || state == 1) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    	if(state == 0 && Vector3.Distance(transform.position,target.position) <= distance) state = 2;
    }

    void OnCollisionEnter(Collision col) {
    	if(col.gameObject.tag == "Ship") DestroyEnemy();
        else if(col.gameObject.tag == "Shot") {
    		Destroy(col.gameObject);
    		if(lives > 1) {
                audioBoom.Play();
    			--lives;
    		}
    		else {
                DestroyEnemy();
	    		Live.score = Live.score + 30;
	    		scoreText.text = "Score: " + Live.score.ToString();
    		}
    	}
    	else if(col.gameObject.tag == "WallDisable") {
            state = 1;
            Destroy(gameObject, 5f);
        }
    }

    public void DestroyEnemy() {
        audioBoom.Play();
        SelfCollider.enabled = false;
        effectBoom.Play();
        Destroy(model);
        state = 3;
    }
}
