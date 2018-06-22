using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacement : MonoBehaviour {

    public Transform cube;
    public List<Transform> Cubes;
    public Material[] Materials;
    Transform iCube;
    public Generator gen;
    int currentTextureIndex = 0;

    void Start() {
        Cubes = new List<Transform>();
    }

    void Update() {

        if (gen.initialized) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                HandleCreateKeyPress();
            }

            if (iCube != null) {

                FollowMouse();

                if (Input.GetKeyDown(KeyCode.X))
                    HandleChangeTextureKeyPress();


                if (Input.GetMouseButtonDown(0)) {

                    CubeScript cube = iCube.GetComponentInChildren<CubeScript>();

                    if (cube.collisions == 0 && cube.canBePlaced) {
                        iCube.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
                        Cubes.Add(iCube);
                        iCube = null;
                    }
                }

            }
        }
    }

    private void HandleChangeTextureKeyPress() {

        currentTextureIndex++;

        if(currentTextureIndex >= 3) {
            currentTextureIndex = 0;
        }

        iCube.GetComponentInChildren<MeshRenderer>().material = Materials[currentTextureIndex];

    }

    private void HandleCreateKeyPress() {
        if (iCube == null) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                iCube = Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
                currentTextureIndex = 0;
            }

        }
        else {

            Destroy(iCube.gameObject);

        }
    }

    public void DestroyCubes() {
        foreach (Transform cube in Cubes) {
            Destroy(cube.gameObject);
        }

        Cubes = new List<Transform>();
    }

    private void FollowMouse() {

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            iCube.position = hit.point;
            iCube.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }

    }
}
