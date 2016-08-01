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
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("PickUp")) {
			print (col.gameObject.tag);
			PlayerMovement.pickUpCount += 1;
			_scoreText.GetComponent<Text> ().text = PlayerMovement.pickUpCount.ToString ();
			Destroy (col.gameObject);
		}

		if (col.gameObject.CompareTag ("Goal")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}
