using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public GameObject fixedPart;
    public GameObject mobilePart;
	public Text scoreText;
	public int lives = 1;
	public AudioSource audioBoom;
    public ParticleSystem effectBoom;

    public GameObject shotPrefab;
    public Transform[] canons;
    public Transform target;
    public AudioSource audioShot;
    public float distance = 50f;

    public Transform grabM;
    public Transform grabF;

    private Collider SelfCollider;
    private int canon;
    private int state = 0;
    //0 => desactivado
    //1 => desactivado i no va a volver
    //2 => vivo
    //3 => muerto

    void Start() {
        SelfCollider = GetComponent<Collider>();
        state = 0;
        canon = 0;
    }

    void Update() {
    	if(state != 3){
    		Vector3 relativePos = target.position - grabM.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            mobilePart.transform.rotation = rotation;

    		//mobilePart.transform.LookAt(target);
    		Vector3 trans = grabF.position - grabM.position;
    		mobilePart.transform.Translate(trans, Space.World);
    	}
    	if(state == 0 && Vector3.Distance(transform.position,target.position) <= distance){
        	state = 2;
        	StartCoroutine(Shoot());
    	}
    }

    void OnCollisionEnter(Collision col) {
    	if(state != 3) Collision(col);
    }

    public void Collision(Collision col) {
    	if(col.gameObject.tag == "Ship") DestroyEnemy();
        else if(col.gameObject.tag == "Shot") {
    		Destroy(col.gameObject);
    		if(lives > 1) {
    			audioBoom.Play();
    			--lives;
    		}
    		else {
                DestroyEnemy();
	    		Live.score = Live.score + 20;
	    		scoreText.text = "Score: " + Live.score.ToString();
    		}
    	}
    	else if(col.gameObject.tag == "WallDisable") state = 1;
    }

    public void DestroyEnemy() {
    	audioBoom.Play();
        SelfCollider.enabled = false;
        state = 3;
        effectBoom.Play();
        Destroy(mobilePart);
        Destroy(fixedPart);
    }

    IEnumerator Shoot() {
    	yield return new WaitForSeconds(2f);
    	if(state == 2) {
    		audioShot.Play();
	    	for(int i = 0; i < 4 && state == 2; i++) {
		    	Vector3 relativePos = target.position - canons[canon].position;
		        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

				GameObject shotInstance;
				shotInstance = Instantiate(shotPrefab, canons[canon].position, rotation) as GameObject;
				shotInstance.SetActive(true);
				canon = (canon + 1) % 4;

				yield return new WaitForSeconds(0.2f);
			}
			StartCoroutine(Shoot());
		}
    }
}
