using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //para el display de las vidas
using UnityEngine.SceneManagement;

public class Live : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;
	public PauseMenu menu;
	public Movement move;
	public Shooting shoot;
	public GameObject model;
	public GameObject effects;
	public ParticleSystem effectBoom;
	public ParticleSystem sparks1;
    public ParticleSystem sparks2;

	public GameObject shield;
	
	public static int lives;
	public static int score;
	public AudioSource audio;
	public AudioClip boomClip;
	public AudioClip deathClip;
	public AudioSource background;

	private int god = 0;
	private int maxLives;

	public Image im;
	private bool hit;

    void Start() {
        lives = 10;
        maxLives = lives;
        score = 0;
		slider.maxValue = maxLives;
		slider.value = lives;
		fill.color = gradient.Evaluate(1f);
		hit = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.G)) god = (god+1)%2;
        if(Input.GetKeyDown(KeyCode.Z)) {
        	if(shield.active) Shield(0);
        	else Shield(1);
        }
        if(Input.GetKeyDown(KeyCode.C)) {
        	Heal(3);
        }
    }

    void OnCollisionEnter(Collision col) {
    	if(col.gameObject.tag == "EnemyShot" || col.gameObject.tag == "Enemy") {
    		if(!hit) {
    			hit = true;
	    		if(shield.active) Shield(0);
	    		else if(god == 0) {
	    			if(col.gameObject.tag == "EnemyShot") --lives;
	    			else lives -= 2;
	    			if(lives <= maxLives/2) {
	    				sparks1.Play();
	    				sparks2.Play();
	    			}
	    			if(lives < 0) lives = 0;
	    		}
	    		slider.value = lives;
	    		fill.color = gradient.Evaluate(slider.normalizedValue);
	    		audio.PlayOneShot(boomClip, 0.5f);
	    		if(lives > 0) StartCoroutine(Damaged(0.2f));
				else {
					GetComponent<Rigidbody>().detectCollisions = false;
					move.enabled = false;
					shoot.enabled = false;
					model.SetActive(false);
					effects.SetActive(false);
					background.mute = true;
					effectBoom.Play();
					audio.PlayOneShot(deathClip, 1f);
					Invoke("GameOver", 2f);
				}
		    }
		    if(col.gameObject.tag == "EnemyShot") Destroy(col.gameObject);
		}
    }
	
	IEnumerator Damaged(float s) {
		im.enabled = true;
		yield return new WaitForSeconds(s);
		im.enabled = false;
		yield return new WaitForSeconds(s);
		im.enabled = true;
		yield return new WaitForSeconds(s);
		im.enabled = false;
		hit = false;
	}

    public void Shield(int x) {
    	if(x == 1 && !shield.active) shield.SetActive(true);
    	else if(x == 0 && shield.active) shield.SetActive(false);
    }

    public void Heal(int x) {
    	if(lives + x > maxLives) lives = maxLives;
    	else lives += x;

    	slider.value = lives;
	    fill.color = gradient.Evaluate(slider.normalizedValue);

    	if(lives > maxLives/2) {
			sparks1.Stop();
			sparks2.Stop();
		}
    }

    void GameOver() {
    	menu.GameOver();
    }
}