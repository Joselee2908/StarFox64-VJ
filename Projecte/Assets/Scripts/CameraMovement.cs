using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public Transform player; 

	public bool end = false;
	public float smoothSpeed = 0.125f;
	public Vector3 offset = new Vector3(0f, 1f, -5.5f);

    void LateUpdate() {
    	if(!end) {
    		Vector3 desiredPosition = player.TransformPoint(player.InverseTransformPoint(target.position) + offset);
    		transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

    		//transform.LookAt(target);
    	}
   	}
}
