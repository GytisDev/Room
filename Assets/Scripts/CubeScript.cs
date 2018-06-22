using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    [HideInInspector]
    public int collisions = 0;
    [HideInInspector]
    public bool canBePlaced = false;

    void Start () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "WallTrigger")
            collisions++;
        if (other.gameObject.tag == "RestrictTrigger")
            canBePlaced = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "WallTrigger")
            collisions--;
        if (other.gameObject.tag == "RestrictTrigger")
            canBePlaced = false;
    }

    void Update () {
		
	}
}
