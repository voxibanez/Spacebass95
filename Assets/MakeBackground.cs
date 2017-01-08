using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBackground : MonoBehaviour {

	public int spacing;
	public GameObject plane;
	public GameObject pObject;
	public GameObject bgObject;

	// Use this for initialization
	void Start () {
		buildPerimeter ();
		buildBackground ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private List<Vector3> getBounds(GameObject go){
		float width = go.GetComponent<Renderer> ().bounds.size.x;
		float height = go.GetComponent<Renderer> ().bounds.size.y;

		Vector3 topRight = go.transform.position, topLeft = go.transform.position, bottomRight = go.transform.position, bottomLeft = go.transform.position;

		topRight.x += width / 2;
		topRight.y += height / 2;

		topLeft.x -= width / 2;
		topLeft.y += height / 2;

		bottomRight.x += width / 2;
		bottomRight.y -= height / 2;

		bottomLeft.x -= width / 2;
		bottomLeft.y -= height / 2;

		List<Vector3> cor_temp = new List<Vector3> ();
		cor_temp.Add (topRight);
		cor_temp.Add (topLeft);
		cor_temp.Add (bottomRight);
		cor_temp.Add (bottomLeft);

		return cor_temp;
	}

	void buildPerimeter(){
		List<Vector3> bounds = getBounds (plane);
		Vector3 topRight = bounds [0];
		Vector3 bottomLeft = bounds [3];

		for (int i = Mathf.RoundToInt (bottomLeft.x); i < Mathf.RoundToInt (topRight.x); i += spacing) {
			Instantiate (pObject, new Vector3 (i, topRight.y, 10), Quaternion.identity);
			Instantiate (pObject, new Vector3 (i, bottomLeft.y, 10), Quaternion.identity);
		}
		for (int i = Mathf.RoundToInt (bottomLeft.y); i < Mathf.RoundToInt (topRight.y); i += spacing) {
			Instantiate (pObject, new Vector3 (topRight.x, i, 10), Quaternion.identity);
			Instantiate (pObject, new Vector3 (bottomLeft.x, i, 10), Quaternion.identity);
		}
			
	}

	void buildBackground(){
		int bgSpacing = 30;
		List<Vector3> bounds = getBounds (plane);
		Vector3 topRight = bounds [0];
		Vector3 bottomLeft = bounds [3];
		int countx = 0, county = 0;
		for (int i = Mathf.RoundToInt (bottomLeft.x) + bgSpacing; i < Mathf.RoundToInt (topRight.x) ; i += bgSpacing) {
			county = 0;
			countx++;
			for (int j = Mathf.RoundToInt (bottomLeft.y) + bgSpacing; j < Mathf.RoundToInt (topRight.y) ; j += bgSpacing) {
				GameObject temp = (GameObject)Instantiate (bgObject, new Vector3 (i, j, 2), Quaternion.identity);
				temp.GetComponent<tickScriptBg> ().offset = countx + county;
				temp.GetComponent<tickScriptBg> ().enabled = true;
				county++;
			}
		}
	}
}
