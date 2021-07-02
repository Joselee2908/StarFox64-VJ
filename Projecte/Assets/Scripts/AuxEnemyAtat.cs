using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxEnemyAtat : MonoBehaviour
{
	public Atat atat;

    void OnCollisionEnter(Collision col) {
        atat.Collision(col);
    }
}
