using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField] GameObject _player;
	public static float _shakeTimer;
	public static float _shakeAmount;

	private Vector3 _newPosition;
	// Use this for initialization
	void Start () {
		_newPosition = this.transform.position;
		_newPosition.z = -10;
	}
	
	// Update is called once per frame
	void Update () {

		_newPosition.y = _player.transform.position.y;

		this.transform.position = _newPosition;

		if (_shakeTimer >= 0) {
			Vector2 shakePosition = Random.insideUnitCircle * _shakeAmount;

			this.gameObject.transform.position = new Vector3 (transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);

			_shakeTimer -= Time.deltaTime;
		}
	}

	public static void ShakeCamera(float shakePower, float shakeDuration){
		_shakeAmount = shakePower;
		_shakeTimer = shakeDuration;
	}
}
