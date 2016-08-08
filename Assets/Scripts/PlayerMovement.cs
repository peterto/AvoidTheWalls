﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private float lastClickTime;

    public float catchTime = 0.25f;


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

    private Vector3 _endPosition;
    private Vector3 _startPosition;
//    public GameObject thingToMove;
//    public float smooth = 2;
	
    void Start () {
		pickUpCount = 0;

		if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
			this.gameObject.AddComponent<Rigidbody2D> ();
		}
//        thingToMove = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (!_isDead) {


//            #if UNITY_IOS
//                if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
//                    this.gameObject.AddComponent<Rigidbody2D> ();
//                } else {
//                    Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
//                    rigidbody.AddForce (new Vector2 (0, 0));
//                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//                        Vector2 pos = Input.GetTouch(0).position;
//                        pos = Camera.main.ScreenToWorldPoint(pos);
//                        Vector2 newPos = new Vector2(pos.x, this.transform.position.y);
//                        transform.position = pos;
//                    }
//                }
//            #elif UNITY_STANDALONE
//
//    			float horizontal = Input.GetAxisRaw ("Horizontal");
//    			if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
//    				this.gameObject.AddComponent<Rigidbody2D> ();
//    			} else {
//    				Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
//    				rigidbody.velocity = Vector2.right * horizontal * _xspeed;
//    				rigidbody.AddForce (new Vector2 (0, _yspeed));
//    				if (Input.GetKey (KeyCode.DownArrow) || Input.GetMouseButton (0)) {
//    					_isSuper = true;
//    					rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
//    				} else {
//    					_isSuper = false;
//    				}
//    			}
//            #endif
//            /* in line input controls
          if (this.gameObject.GetComponent<Rigidbody2D> () == null) {
              this.gameObject.AddComponent<Rigidbody2D> ();
          } else {
                Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
//                rigidbody.gravityScale = 0;
                rigidbody.AddForce (new Vector2 (0, _yspeed));

//                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
//                    _endPosition = HandleTouchInput();
//                } else {
                    _endPosition = HandleMouseInput();
//                }
                Vector3 newPos = this.transform.position;
                newPos.x = _endPosition.x;
                rigidbody.velocity = new Vector2(0, 0);
//                thingToMove.transform.position = Vector3.Lerp(thingToMove.transform.position, new Vector3(_endPosition.x, _endPosition.y, 0), Time.deltaTime * smooth);
                this.transform.position = newPos;


            }
//            */

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

    private Vector3 HandleTouchInput() {
        for (var i = 0; i < Input.touchCount; i++) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                Vector2 screenPosition = Input.GetTouch(i).position;
                _startPosition = Camera.main.ScreenToWorldPoint(screenPosition);
//                if (Input.touchCount == 2) {
//                    Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D> ();
//                    rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
//                }
            }
        }

        return _startPosition;
    }

    private Vector3 HandleMouseInput() {
        if(Input.GetButtonDown("Fire1")) {
            if(Time.time - lastClickTime < catchTime) {
                //double click
                print("Double click");
                Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
                rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
            } else {
                //normal click
            }
            lastClickTime = Time.time;
        }

            Vector3 screenPosition = Input.mousePosition;
//            screenPosition = new Vector3(screenPosition.x, 0, 0);
            _startPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        return _startPosition;
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
