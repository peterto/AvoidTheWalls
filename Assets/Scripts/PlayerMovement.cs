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
	public static bool _isSuper = false;
	// Use this for initialization
	[SerializeField] Sprite _original;
	[SerializeField] GameObject _level;
	[SerializeField] GameObject _music;
	[SerializeField] GameObject _menu;
	[SerializeField] GameObject _pickUps;
	[SerializeField] GameObject _menuController;
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


            #if UNITY_IOS
                if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
                    this.gameObject.AddComponent<Rigidbody2D> ();
                } else {
                    Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
                    rigidbody.AddForce (new Vector2 (0, 0));
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//                        Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch(0).position.x, this.transform.position.y, 0));
//                        transform.position = new Vector3(pos.x, pos.y, pos.z);
                        Vector2 pos = Input.GetTouch(0).position;
                        pos = Camera.main.ScreenToWorldPoint(pos);
//                        Vector2 newPos = new Vector2(pos.x, this.transform.position.y);
                        transform.position = pos;
                    }
                }
            #elif UNITY_STANDALONE

    			float horizontal = Input.GetAxisRaw ("Horizontal");
    			if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
    				this.gameObject.AddComponent<Rigidbody2D> ();
    			} else {
    				Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
    				rigidbody.velocity = Vector2.right * horizontal * _xspeed;
    				rigidbody.AddForce (new Vector2 (0, _yspeed));
//    				if (Input.GetKey (KeyCode.DownArrow) || Input.GetMouseButton (0)) {
//    					_isSuper = true;
//    					rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
//    				} else {
//    					_isSuper = false;
//    				}
    			}
            #endif

		} else {
	
			if (_runAnimation) {
				Invoke ("Death", 0f);
				Invoke ("SetMenuActive", 0.5f);
			}

			if (Input.GetKeyDown(KeyCode.Escape)) {
				_menuController.GetComponent<MenuController> ().RestartLevel ();
			}


		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag ("Obstacle")) {
			this.gameObject.GetComponent<AudioSource> ().Play ();
			_isDead = true;
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
		this.GetComponentInChildren<ParticleSystem> ().Emit (10);
		Destroy (GameObject.FindGameObjectWithTag ("PickUpGroup"));
		Destroy (this.gameObject.GetComponent<Rigidbody2D> ());
		_music.GetComponent<AudioSource> ().Stop ();
		CameraFollow.ShakeCamera (0.5f, 0.5f);
		_runAnimation = false;
	}

	void SetMenuActive() {
		_menu.SetActive(true);
	}
}
