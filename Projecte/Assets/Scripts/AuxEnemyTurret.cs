using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxEnemyTurret : MonoBehaviour
{
	public Turret turret;

    void OnCollisionEnter(Collision col) {
    	turret.Collision(col);
    }
}
