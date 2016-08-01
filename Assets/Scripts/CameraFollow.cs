using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField] GameObject _player;

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
	}
}
