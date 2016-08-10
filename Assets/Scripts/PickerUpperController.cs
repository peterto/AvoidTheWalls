using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PickerUpperController : MonoBehaviour {

	[SerializeField] GameObject _scoreText;
	[SerializeField] GameObject _scoreBoosterText;
	private int _scoreBoosterMultiplier = 2;
	private bool _scoreBooster = false;
	private float _scoreBoosterDuration = 5f;
	private int _baseScore = 1;
	private int _superPickUp = 10;
	private float _scoreBoosterOriginalTime;
	// Use this for initialization
	void Start () {
		_scoreBoosterText.GetComponent<Text> ().text = "x1";
		_scoreBoosterOriginalTime = -5f;
	}
	
	// Update is called once per frame
	void Update () {
		_scoreText.GetComponent<Text> ().text = PlayerMovement.pickUpCount.ToString ();

		if (Time.time < _scoreBoosterOriginalTime + _scoreBoosterDuration) {
			_scoreBoosterText.GetComponent<Text> ().text = "x" + _scoreBoosterMultiplier.ToString ();
		} else {
			_scoreBoosterText.GetComponent<Text> ().text = "x1";
			_scoreBooster = false;
		}
			

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("PickUp")) {
			if (PlayerMovement._isSuper) {
				PlayerMovement.pickUpCount += _superPickUp;
			} else if (_scoreBooster) {
				PlayerMovement.pickUpCount += (_scoreBoosterMultiplier * _baseScore);
			} else {
				PlayerMovement.pickUpCount += _baseScore;
			}
				

			col.gameObject.GetComponent<AudioSource> ().Play ();
			col.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			col.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (col.gameObject, 1f);
		}

		if (col.gameObject.CompareTag ("Goal")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}

		if (col.gameObject.CompareTag ("ScoreBooster")) {
			_scoreBoosterOriginalTime = Time.time;
			_scoreBooster = true;
		}
	}
}
