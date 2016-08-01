using UnityEngine;
using System.Collections;

public class LevelMovementController : MonoBehaviour {

	[SerializeField] static float _speed = -2000f;
	private Rigidbody2D _rigidbody;
	// Use this for initialization
	void Start () {
//		_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
//		_rigidbody.velocity = new Vector2(0, _speed);
	}
	
	// Update is called once per frame
	void Update () {

//		this.transform.position = new Vector2 (0, Time.deltaTime * _speed);
		
	}
}
