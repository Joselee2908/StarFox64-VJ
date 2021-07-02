using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour
{
    public GameObject model;
	public Text scoreText;
	public int lives = 1;
    public AudioSource audioBoom;
    public ParticleSystem effectBoom;

    public GameObject shotPrefab;
    public Transform canon;
    public Transform target;
    public AudioSource audioShot;
    public float distance = 50f;

    private Collider SelfCollider;
    private float fase;
    private int state = 0;
    //0 => desactivado
    //1 => desactivado i no va a volver
    //2 => vivo
    //3 => muerto

    void Start() {
        SelfCollider = GetComponent<Collider>();
        state = 0;
        fase = Random.Range(0f, 20f);
    }

    void Update() {
    	if(state != 3) {
        	transform.Translate(Vector3.up * (0.0075f * Mathf.Sin(2*Mathf.PI * Time.time / 4 + fase)), Space.World);
        	transform.LookAt(target);
    	}
    	if(state == 0 && Vector3.Distance(transform.position,target.position) <= distance) {
        	state = 2;
	        Invoke("Shoot", 2f);
    	}
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
    	else if(col.gameObject.tag == "WallDisable") state = 1;
    }

    public void DestroyEnemy() {
    	audioBoom.Play();
        SelfCollider.enabled = false;
        effectBoom.Play();
        Destroy(model);
        state = 3;
    }

    void Shoot() {
    	if(state == 2) {
	    	Vector3 relativePos = target.position - canon.position;

	        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

	        audioShot.Play();
			GameObject shotInstance;
			shotInstance = Instantiate(shotPrefab, canon.position, rotation) as GameObject;
			shotInstance.SetActive(true);
			Invoke("Shoot", 2f);
		}
    }
}
