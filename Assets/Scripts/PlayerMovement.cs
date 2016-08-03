using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float _xspeed = 10f;
	[SerializeField] float _yspeed = -100f;
	[SerializeField] float _playerSpeed = 10f;
	[SerializeField] ParticleSystem _particleSystem;
	public static bool _isDead = false;
	public static int pickUpCount = 0;
	// Use this for initialization
	[SerializeField] Sprite _original;
	[SerializeField] GameObject _level;
	[SerializeField] GameObject _music;
	[SerializeField] GameObject _menu;
	[SerializeField] GameObject _pickUps;
	public static bool _runAnimation = true;
	void Start () {
		pickUpCount = 0;

		if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
			this.gameObject.AddComponent<Rigidbody2D> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!_isDead) {
			float horizontal = Input.GetAxisRaw ("Horizontal");
			if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
				this.gameObject.AddComponent<Rigidbody2D> ();
			} else {
				Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
				rigidbody.velocity = Vector2.right * horizontal * _xspeed;
				rigidbody.AddForce (new Vector2 (0, _yspeed));
//            if (Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(0)) {
				if (Input.GetKey (KeyCode.DownArrow) || Input.GetMouseButton (0)) {
                    
					rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
				}
			}

		} else {
//			Invoke ("DelayAnimation", 1f);
//			DelayAnimation ();
//			this.GetComponent<P
	
			if (_runAnimation) {
//			_particleSystem.Emit (1);

				Invoke ("Death", 0f);
				Invoke ("SetMenuActive", 0.5f);
			}
//			Invoke ("ReloadScene", .1f);
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

		_music.GetComponent<AudioSource> ().Pause ();
		_menu.SetActive (true);
		Time.timeScale = 0;
	}

	void Death() {
		if (this.GetComponentInChildren<ParticleSystem> () != null)
			this.GetComponentInChildren<ParticleSystem> ().Emit (1);
		Destroy (GameObject.FindGameObjectWithTag ("PickUpGroup"));
		Destroy (this.gameObject.GetComponent<Rigidbody2D> ());
		_music.GetComponent<AudioSource> ().Stop ();
		CameraFollow.ShakeCamera (0.5f, 0.5f);
		Destroy (GameObject.FindGameObjectWithTag("ParticleSystem"));
		_runAnimation = false;
	}

	void SetMenuActive() {
		_menu.SetActive(true);
	}
}
