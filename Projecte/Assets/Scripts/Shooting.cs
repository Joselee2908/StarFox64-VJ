using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip shotClip;
	public GameObject shotPrefab;
    public Transform barrelEnd;

    private bool cooldown = false;
    private float cooldownTime = 0f;
    private float delay = 0.5f;

    void Update() {
    	if (Input.GetKeyDown(KeyCode.X)) {
            if(delay == 0.5f) RPS(1);
            else RPS(0);
    	}
    	if(cooldown) {
			cooldownTime += Time.deltaTime;
			if(cooldownTime >= delay) {
                cooldown = false;
				cooldownTime = 0;
			}
		}
		if(Input.GetKey(KeyCode.Space) && !cooldown) {
			audio.PlayOneShot(shotClip, 0.5f);
			GameObject rocketInstance;
			rocketInstance = Instantiate(shotPrefab, barrelEnd.position, barrelEnd.rotation) as GameObject;
			rocketInstance.SetActive(true);
			cooldown = true;
		}
    }

    public void RPS(int x) {
        if(x == 1) delay = 0.25f;
        else if(x == 0) delay = 0.5f;
    }
}
