using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {
    [HideInInspector]
    public List<Transform> Walls;
    public CubePlacement cp;
    public BoxCollider restriction;

    private Transform iGround;

    public bool initialized { get; private set; }

    public Transform wall;
    public Transform ground;

    public InputField FieldX, FieldY;

    public float roomHeigth = 1.5f;
    private Vector3 origin;

    private void Start() {
        initialized = false;
    }

    public void Button_OnClick() {

        int x, y;

        if(FieldX.text.Length < 1) 
            return;
        else {
            x = int.Parse(FieldX.text);

            if (x < 1 || x > 20)
                return;
        }

        if (FieldY.text.Length < 1)
            return;
        else {
            y = int.Parse(FieldY.text);

            if (y < 1 || y > 20)
                return;
        }

        if(Walls != null && Walls.Count > 0) {
            DestroyAll();
        }

        Generate(x, y);
    }

    private void Generate(float roomLength, float roomWidth) {
        Walls = new List<Transform>();
        Vector3 scaleFactor = Vector3.zero;
        origin = Vector3.zero;

        //Desine siena
        Transform iWall = Instantiate(wall, origin + new Vector3(roomWidth / 2, roomHeigth / 2, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        iWall.localScale = new Vector3(0.2f, roomHeigth, roomLength + 0.2f);
        Walls.Add(iWall);

        //Virsutine siena
        Transform iWall2 = Instantiate(wall, origin + new Vector3(0, roomHeigth / 2, roomLength / 2), Quaternion.identity);
        iWall2.localScale = new Vector3(0.2f, roomHeigth, roomWidth - 0.2f);
        iWall2.transform.Rotate(new Vector3(0, 90, 0));
        Walls.Add(iWall2);

        //Kaire siena
        Transform iWall3 = Instantiate(wall, origin + new Vector3(-(roomWidth / 2), roomHeigth / 2, 0), Quaternion.identity);
        iWall3.localScale = new Vector3(0.2f, roomHeigth, roomLength + 0.2f);
        Walls.Add(iWall3);

        //Apatine siena
        Transform iWall4 = Instantiate(wall, origin + new Vector3(0, roomHeigth / 2, -(roomLength / 2)), Quaternion.identity);
        iWall4.localScale = new Vector3(0.2f, roomHeigth, roomWidth - 0.2f);
        iWall4.transform.Rotate(new Vector3(0, -90, 0));
        Walls.Add(iWall4);

        //Grindys
        iGround = Instantiate(ground, origin, Quaternion.identity);
        iGround.localScale = new Vector3((float)roomWidth / 10, 0, (float)roomLength / 10);

        restriction.size = new Vector3(roomWidth - 1.2f, roomHeigth, roomLength - 1.2f);

        initialized = true;
    }

    private void DestroyAll() {
        foreach (Transform wall in Walls) {
            Destroy(wall.gameObject);
        }

        Walls = new List<Transform>();

        Destroy(iGround.gameObject);

        cp.DestroyCubes();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
