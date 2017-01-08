using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    GameObject Enemy, Player, Camera;
    Vector3 enemyTargetPos, playerTargetPos;
    private Vector3 velocity1 = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;
    private float dampTime = .2f;
    private bool startAnimation = true;
    private bool switchLights = false;
    private bool ticked = false;
    private bool tickSwitch1 = false;
    public GameObject secondRing;
    public List<GameObject> secondRings = new List<GameObject>();
    private bool startBattle = false;
    // Use this for initialization
    void Start()
    {
        startBattle = true;
        Enemy = GameObject.FindGameObjectsWithTag("Enemy")[0];
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        this.transform.position = Player.transform.position;
        Camera.GetComponent<FollowPlayer>().enabled = false;
        Player.GetComponent<PlayerMove>().enabled = false;
        Enemy.GetComponent<FollowObject>().enabled = false;

        Player.transform.localScale = new Vector3(.2f, .2f, 1f);
        Enemy.transform.localScale = new Vector3(.2f, .2f, 1f);
        //this.transform.localScale = new Vector3(.2f, .2f, 1f);
       
        Vector3 closestSquareVector = closestSquare();

        this.transform.position = new Vector3(closestSquareVector.x, closestSquareVector.y, closestSquareVector.z - 1);
        enemyTargetPos = new Vector3(closestSquareVector.x + 10, closestSquareVector.y, closestSquareVector.z - 1);
        playerTargetPos = new Vector3(closestSquareVector.x - 10, closestSquareVector.y, closestSquareVector.z - 1);
        Camera.transform.position = new Vector3(closestSquareVector.x, closestSquareVector.y, Camera.transform.position.z);
        Camera.GetComponent<Camera>().fieldOfView -= 80;

        //enemyTargetPos = new Vector3(Player.transform.position.x + 30, Player.transform.position.y, Player.transform.position.z);
        //playerTargetPos = new Vector3(Player.transform.position.x - 30, Player.transform.position.y, Player.transform.position.z);


        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (startAnimation)
        {
            foreach (Light light in this.GetComponentsInChildren<Light>())
            {
                if (light.type == LightType.Spot)
                {
                    light.enabled = true;
                    if (light.spotAngle < 20)
                        light.spotAngle = light.spotAngle + 1;
                    else
                    {
                        switchLights = true;
                        startAnimation = false;
                    }
                       
                }
                    
            }
        }
        else if (switchLights)
        {
            foreach (Light light in this.GetComponentsInChildren<Light>())
            {
                if (light.type == LightType.Spot)
                    light.enabled = false;
                if (light.type == LightType.Directional)
                {
                    light.enabled = true;
                    light.intensity -= .1f;
                    if(light.intensity < .1f)
                        switchLights = false;
                }
                    
            }
            
        }

        if (ticked && !switchLights)
        {
            foreach (Light light in this.GetComponentsInChildren<Light>())
            {
                if (light.type == LightType.Directional)
                {
                    if (light.intensity < 1 && !tickSwitch1)
                    {

                        light.intensity += .1f;
                    }
                    else if (light.intensity > 0)
                    {
                        tickSwitch1 = true;
                        light.intensity -= .1f;
                    }
                    else
                        ticked = false;
            }

            }
        }

        foreach (GameObject obj in secondRings)
            obj.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(obj.GetComponent<SpriteRenderer>().transform.localScale.x + .05f, obj.GetComponent<SpriteRenderer>().transform.localScale.y + .05f, obj.GetComponent<SpriteRenderer>().transform.localScale.z);

        Enemy.transform.position = Vector3.SmoothDamp(Enemy.transform.position, enemyTargetPos, ref velocity1, dampTime);
        Player.transform.position = Vector3.SmoothDamp(Player.transform.position, playerTargetPos, ref velocity2, dampTime);
    }

    public void tick()
    {
        if (startBattle)
        {
            ticked = true;
            tickSwitch1 = false;
            secondRings.Add(Instantiate(secondRing, this.transform.position, Quaternion.identity));
            if (secondRings.Count > 5)
            {
                GameObject.Destroy(secondRings[0]);
                secondRings.RemoveAt(0);
            }
                
        }
        //GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
    }

    private Vector3 closestSquare()
    {
        int count = 0;
        Vector3 closestVector = Vector3.zero;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Background"))
        {
            if (count == 0)
                closestVector = obj.transform.position;
            else
            {
                if(Mathf.Abs(obj.transform.position.x - Player.transform.position.x) < Mathf.Abs(closestVector.x - Player.transform.position.x) && Mathf.Abs(obj.transform.position.y - Player.transform.position.y) < Mathf.Abs(closestVector.y - Player.transform.position.y))
                    closestVector = obj.transform.position;
            }
            count++;
        }
        return closestVector;
    }
}
