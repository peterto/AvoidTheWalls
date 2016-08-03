using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PickerUpperController : MonoBehaviour {

	[SerializeField] GameObject _scoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_scoreText.GetComponent<Text> ().text = PlayerMovement.pickUpCount.ToString ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("PickUp")) {
			if (PlayerMovement._isSuper) {
				PlayerMovement.pickUpCount += 10;
			} else {
				PlayerMovement.pickUpCount += 1;
			}

			col.gameObject.GetComponent<AudioSource> ().Play ();
			col.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			col.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (col.gameObject, 1f);
		}

		if (col.gameObject.CompareTag ("Goal")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}
