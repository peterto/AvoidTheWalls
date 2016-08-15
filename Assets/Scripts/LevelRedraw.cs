using UnityEngine;
using System.Collections;

public class LevelRedraw : MonoBehaviour {

	public GameObject _level;
	public GameObject _pickUps;
	// Use this for initialization
	bool _endLevel = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameObject.transform.position.y <= -260 && _endLevel == false) {
			Instantiate (_level, this.transform.position, Quaternion.identity);
			Instantiate (_pickUps, this.transform.position, Quaternion.identity);
			_endLevel = true;

		}
	
	}
}
