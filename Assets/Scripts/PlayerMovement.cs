using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float _xspeed = 10f;
	[SerializeField] float _yspeed = -100f;
	[SerializeField] float _playerSpeed = 10f;
	[SerializeField] ParticleSystem _particleSystem;
	public bool _isDead = false;
	public static int pickUpCount = 0;
	// Use this for initialization
	[SerializeField] Sprite _original;
	[SerializeField] GameObject _level;
	void Start () {
		pickUpCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (!_isDead) {
			float horizontal = Input.GetAxisRaw ("Horizontal");
			this.GetComponent<Rigidbody2D> ().velocity = Vector2.right * horizontal * _xspeed;
			this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, _yspeed));
//            if (Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(0)) {
             if (Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(0)) {
                    
                this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, _yspeed * _playerSpeed));
			}

		} else {
			DelayAnimation ();
//			this.GetComponent<P
			_particleSystem.Emit(1);
			Invoke ("ReloadScene", .1f);
//			ResetPlayer();

		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag ("Obstacle")) {
			_isDead = true;
//			this.transform.position = new Vector2 (0, 0);
		}
	}

	void ResetPlayer() {
		_isDead = false;
		this.transform.position = new Vector2 (0, 0);
		this.gameObject.AddComponent<Rigidbody2D> ();
		Rigidbody2D newRbody = this.gameObject.GetComponent<Rigidbody2D> ();
		newRbody.gravityScale = 0;
		newRbody.freezeRotation = true;
		Animator animator = this.GetComponent<Animator> ();
		animator.enabled = false;
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = _original;
	}

	void ReloadScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}


	void DelayAnimation() {
		Destroy (this.gameObject.GetComponent<Rigidbody2D> ());
//		Animator animator = this.GetComponent<Animator> ();
//		animator.enabled = true;
	}
}
