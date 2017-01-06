using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject Enemy;
	public GameObject plane;
	public GameObject player;
	public int maxObjects; 
	private List<Vector3> quadBounds;
	public float spacing;
	private List<GameObject> enemyList;
	private bool maxEnemiesOnScreen;
	private int maxEnemiesGenerated;
	public int difficultyScalar;
	// Use this for initialization
	void Start () {
		quadBounds = getBounds (plane);
		enemyList = new List<GameObject> ();
		maxEnemiesOnScreen = false;
		maxEnemiesGenerated = 0;
		difficultyScalar = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//If the max amount of enemies are on screen and the list size hasent changed, dont do anything
		if (maxEnemiesOnScreen && enemyList.Count == maxEnemiesGenerated)
			return;

		int runs = 0;
		if (enemyList.Count > maxObjects) {
			return;
		}
			
		//<Vector3> quadBounds = getBounds (plane);
		float randx, randy;
		float size = Random.Range (0.2f, 2f);
		Vector3 topRight = quadBounds [0], bottomLeft = quadBounds [3];

		randx = Random.Range (topRight.x - 10 * size, bottomLeft.x + 10 * size);
		randy = Random.Range (topRight.y - 10 * size,bottomLeft.y + 10 * size);
		bool cont = false;

		while(!cont && enemyList.Count > 0){
			if (runs > 10) {
				print ("Aborting enemy placement, timeout");
				maxEnemiesOnScreen = true;
				maxEnemiesGenerated = enemyList.Count;
				return;
			}
			runs++;
			foreach (GameObject enObj in enemyList){

				BoxCollider2D tempC = enObj.GetComponent<BoxCollider2D> ();
				float centerTempx = tempC.bounds.center.x;
				float centerTempy = tempC.bounds.center.y;
				float leftBound = centerTempx - tempC.bounds.extents.x * 2 * size - spacing;
				float rightBound = centerTempx + tempC.bounds.extents.x * 2* size + spacing;
				float upBound = centerTempy + tempC.bounds.extents.y * 2* size + spacing;
				float downBound = centerTempy - tempC.bounds.extents.y * 2* size - spacing;

				//print ("Left: " + leftBound + " Right: " + rightBound + " Up: " + upBound + " Down: " + downBound);

				bool xIntersect = false, yIntersect = false;


				if (randx < rightBound && randx > leftBound)
					xIntersect = true;
				if (randy < upBound && randy > downBound)
					yIntersect = true;

				if (xIntersect && yIntersect) {
					randx = Random.Range (topRight.x, bottomLeft.x);
					randy = Random.Range (topRight.y, bottomLeft.y);
					cont = false;
					break;
				} else {
					int playerSpacing = 20;
					xIntersect = false;
					yIntersect = false;
					if(randx < player.transform.position.x + playerSpacing && randx > player.transform.position.x - playerSpacing)
						xIntersect = true;
					if(randy < player.transform.position.y + playerSpacing && randy > player.transform.position.y - playerSpacing)
						yIntersect = true;
					if (xIntersect && yIntersect) {
						randx = Random.Range (topRight.x, bottomLeft.x);
						randy = Random.Range (topRight.y, bottomLeft.y);
						cont = false;
						break;
					} else 
						cont = true;
				}

		}
		}

		//Create the game object and then create its stats
		GameObject tempObj = (GameObject)Instantiate(Enemy,new Vector3(randx, randy, 1), Quaternion.identity);
		int hp = Random.Range (5 * difficultyScalar, 50 * difficultyScalar);
		hp = Mathf.RoundToInt (hp * size);
		int atk = Random.Range (1 * difficultyScalar, 20 * difficultyScalar);
		int def = Random.Range (1 * difficultyScalar, 20 * difficultyScalar);
		def = Mathf.RoundToInt (def * size);
		int speed = Mathf.RoundToInt(size * 10);

		tempObj.GetComponent<EnemyStats> ().setStats (hp, atk, def, size, speed);
			enemyList.Add(tempObj);
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
}
