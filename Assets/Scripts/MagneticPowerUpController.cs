using UnityEngine;
using System.Collections;

public class MagneticPowerUpController : MonoBehaviour {


	[SerializeField] GameObject _magneticBoosterText;
	private bool _magneticBooster = false;
	private float _magneticBoosterDuration = 5f;
	private float _magneticBoosterOriginalTime;

	void Start() {
		_magneticBoosterOriginalTime = -5f;
	}

	// Update is called once per frame
	void Update () {
		
		if (Time.time < _magneticBoosterOriginalTime + _magneticBoosterDuration) {
			GameObject [] pickUps = GameObject.FindGameObjectsWithTag ("PickUp");
			for (int i = 0; i < pickUps.Length; i++) {
				float distanceBetweenPlayer = Vector3.Distance (pickUps [i].transform.position, this.gameObject.transform.position);
				if (distanceBetweenPlayer < 10) {
					//move pick up to player
					
				}
			}

		} else {
			_magneticBooster = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("MagneticBooster")) {
			_magneticBoosterOriginalTime = Time.time;
			_magneticBooster = true;
		}
	}
}
