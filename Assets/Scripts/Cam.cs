using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public float speed = 150;
    public float zoomSpeed = 50;
    public float zoomMin = 4, zoomMax = 20;

    private List<Transform> Walls;
    Transform Hidden, ToReveal;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKey(KeyCode.Q)) {
            transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
            HideWalls();
        } else if (Input.GetKey(KeyCode.E)) {
            transform.RotateAround(Vector3.zero, -Vector3.up, speed * Time.deltaTime);
            HideWalls();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y > zoomMin) {
            transform.position += transform.forward * zoomSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.y < zoomMax) {
            transform.position += -transform.forward * zoomSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();


    }

    void HideWalls() {

        Walls = FindObjectOfType<Generator>().Walls;

        if (Walls != null) {

            float shortest = float.MaxValue;

            ToReveal = Hidden;

            foreach (Transform wall in Walls) {

                float distance = Vector3.Distance(transform.position, wall.position);

                if (distance < shortest) {
                    shortest = distance;

                    Hidden = wall;
                }

            }


            if (ToReveal != null) {

                ToReveal.GetComponent<MeshRenderer>().enabled = true;
                ToReveal.GetComponent<BoxCollider>().enabled = true;

            }

            if (Hidden != null) {

                Hidden.GetComponent<MeshRenderer>().enabled = false;
                Hidden.GetComponent<BoxCollider>().enabled = false;

            }



        }

    }
}
