using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
	public float hp;
	public float atk;
	public float def;
	public float size;
	public float speed;
	private bool statsSet;

	// Use this for initialization
	void Start () {
		hp = -1f;
		atk = -1f;
		def = -1f;
		size = -1f;
		speed = -1f;
		statsSet = false;
	}
	
	public bool setStats(float _hp, float _atk, float _def, float _size, float _speed){
		if (!statsSet) {
			if (_hp > 0)
				hp = _hp;
			else
				return false;

			if (_atk > 0)
				atk = _atk;
			else
				return false;

			if (_def > 0)
				def = _def;
			else
				return false;

			if (_size > 0)
				size = _size;
			else
				return false;

			if (_speed > 0)
				speed = _speed;
			else
				return false;

			this.transform.localScale = new Vector3 (1 * size, 1 * size, 1);
			
			statsSet = true;

			return true;
		}
		return false;

	}
}
