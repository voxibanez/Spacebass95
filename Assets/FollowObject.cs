using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject followObject;
    public float hardDampTime = 0.5f;
    private float dampTime;
    private Vector3 velocity = Vector3.zero;
    private Transform target;
    bool battleInt = false;
    // Use this for initialization
    void Start () {
        target = followObject.transform;
        dampTime = hardDampTime;
    }
	
	// Update is called once per frame
	void Update () {
        target = followObject.transform;
        float targetx = target.position.x, targety = target.position.y;
        float thisx = this.transform.position.x, thisy = this.transform.position.y;

        if (targetx - 2 < thisx && thisx < targetx + 2)
        {
            if (targety - 2 < thisy && thisy < targety + 2 && !battleInt)
                startBattle();
        }
        if (target)
        {
            float temp = Mathf.Pow((Mathf.Abs(this.transform.position.x - followObject.transform.position.x) + Mathf.Abs(this.transform.position.y - followObject.transform.position.y)),2f);
            dampTime = temp/100 ;
            if (dampTime < .2)
                dampTime = .2f;

            this.transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, dampTime);
        }

    }

    private void startBattle() {
        battleInt = true;
        print("BattleStarted!");
        GameObject battle = GameObject.Find("BattleHandler");
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (obj.GetComponent<EnemyStats>().uid != this.GetComponent<EnemyStats>().uid)
                obj.SetActive(false);
        }
        battle.GetComponent<Battle>().enabled = true;
    }
}
