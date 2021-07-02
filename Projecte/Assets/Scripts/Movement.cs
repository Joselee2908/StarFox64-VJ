using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Movement : MonoBehaviour
{
    public CameraMovement camera;
    public Transform player;
	public LevelLoader lvlloader;
	public string nextLVL;
    public GameObject hitbox;
    public GameObject laser;
	public float moveSpeed = 5f;
    public float turnSpeed = 70f;
    public float restSpeed = 70f;
    public float rollSpeed = 800f;
    public float endSpeed = 20f;
    public float max_ang1 = 10f;
    public float max_ang2 = 25f;
    public bool end = false;
    public CinemachineDollyCart cart;

    private float speedCart;
    private bool speedBoost;
    private bool speedBoostTop;
    private float angx = 0f;
    private float angy = 0f;
    private float angz = 0f;
    private int roll = 0;

    void Start() {
        end = false;
        speedCart = cart.m_Speed;
    }

    void Update() {
        if(speedBoost) {
            if(cart.m_Speed <= speedCart) {
                cart.m_Speed = speedCart;
                speedBoost = false;
            }
            else {
                if(speedBoostTop) cart.m_Speed -= 0.25f;
                else cart.m_Speed += 1f;

                if(!speedBoostTop && cart.m_Speed >= speedCart + 20f) speedBoostTop = true;
            }
        }

        if(end) transform.Translate(Vector3.forward * endSpeed * Time.deltaTime, player);

        if(Input.GetKeyDown(KeyCode.F) && !speedBoost) {
            cart.m_Speed = speedCart  + 10f;
            speedBoost = true;
            speedBoostTop = false;
        }
        if(Input.GetKeyDown(KeyCode.H)) {
            if(Time.timeScale == 1f) Time.timeScale = 2f;
            else Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            if(laser.active) laser.SetActive(false);
            else laser.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
			if (angz >= 0) roll = 1;
			else roll = 2;
            hitbox.GetComponent<Rigidbody>().detectCollisions = false;
        }

    	int mx = 0, my = 0, mz = 0;
        if(Input.GetKey(KeyCode.W)) {
            if(player.InverseTransformPoint(transform.position).y < 2.4)
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, player);

            if (angx > -max_ang1) {
            	if (angx < 0)
        			angx -= turnSpeed * Time.deltaTime;
        		else
        			angx -= 2 * turnSpeed * Time.deltaTime;
        	}
        	mx = (mx+1)%2;
        }
        
        if(Input.GetKey(KeyCode.S)) {
            if(player.InverseTransformPoint(transform.position).y > -2.2)
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, player);

            if (angx < max_ang1) {
            	if (angx < 0)
        			angx += 2 * turnSpeed * Time.deltaTime;
        		else
        			angx += turnSpeed * Time.deltaTime;
        	}
        	mx = (mx+1)%2;
        }
        
        if(Input.GetKey(KeyCode.A)) {
        	if(player.InverseTransformPoint(transform.position).x > -3.2)
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, player);

        	if (angy > -max_ang1) {
            	if (angy < 0)
        			angy -= turnSpeed * Time.deltaTime;
        		else
        			angy -= 2 * turnSpeed * Time.deltaTime;
        	}
        	if (angz < max_ang2 && roll == 0) {
            	if (angz < 0)
        			angz += 2 * turnSpeed * Time.deltaTime;
        		else
        			angz += turnSpeed * Time.deltaTime;
        	}
        	my = (my+1)%2;
        	mz = (mz+1)%2;
       	}
        
        if(Input.GetKey(KeyCode.D)) {
            if(player.InverseTransformPoint(transform.position).x < 5)
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, player.transform);

        	if (angy < max_ang1) {
            	if (angy < 0)
        			angy += 2 * turnSpeed * Time.deltaTime;
        		else
        			angy += turnSpeed * Time.deltaTime;
        	}
        	if (angz > -max_ang2 && roll == 0) {
            	if (angz < 0)
        			angz -= turnSpeed * Time.deltaTime;
        		else
        			angz -= 2 * turnSpeed * Time.deltaTime;
        	}
        	my = (my+1)%2;
        	mz = (mz+1)%2;
        }

        if(roll == 1) {
            mz = 1;
            if(angz < 359.9) angz += rollSpeed * Time.deltaTime;
            else {
                roll = 0;
                angz = 0;
                hitbox.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
		else if(roll == 2) {
            mz = 1;
            if(angz > -359.9) angz -= rollSpeed * Time.deltaTime;
            else {
                roll = 0;
                angz = 0;
                hitbox.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }

        Relax(mx, ref angx);
        Relax(my, ref angy);
        Relax(mz, ref angz);

        transform.localEulerAngles = new Vector3(angx, angy, angz);
    }

    void Relax(int m, ref float ang) {
        if(m == 0 && ang != 0) {
            if(ang > 1 || ang < -1) {
                if(ang < 0) ang += restSpeed * Time.deltaTime;
                else ang -= restSpeed * Time.deltaTime;
            }
            else ang = 0;
        }
    }

    public void End(){
        end = true;
        camera.end = true;
        Invoke("GoMenu", 4f);
    }

    void GoMenu(){
        lvlloader.loadScene(nextLVL);
    }
}