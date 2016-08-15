using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class MenuController : MonoBehaviour {

	[SerializeField] GameObject _menu;
	[SerializeField] CanvasGroup _menuCanvas;
	[SerializeField] GameObject _player;
	[SerializeField] GameObject _particleSystem;
	[SerializeField] GameObject _pickUps;
	[SerializeField] GameObject _music;
	[SerializeField] Text _currentScore;
	[SerializeField] Text _highScore;

	public void RestartLevel() {

//		this.FadeOut ();

//		Invoke ("AnimateMenu", 1f);
		AnimateMenu ();
//		Invoke ("FadeInMenu", 0.1f);
	}

	void AnimateMenu () {
		int highScore = PlayerPrefs.GetInt ("HighScore");
		if (PlayerMovement.pickUpCount > highScore) {
			PlayerPrefs.SetInt ("HighScore", PlayerMovement.pickUpCount);
		}

		_player.transform.position = new Vector2 (0, 0);
		_player.transform.rotation = new Quaternion (0, 0, 0, 0);
		//		_player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1000));
		//		_player.GetComponent<BoxCollider2D> ().enabled = true;

		//		rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
		//		_player.
		//		GameObject ps = Instantiate(_particleSystem);
		//		ps.transform.parent = _player.transform;
		PlayerMovement.pickUpCount = 0;
		Instantiate (_pickUps);
		//		_menu.gameObject.GetComponent<Animator> ().SetBool ("ExitMenu", true);
		this.gameObject.GetComponent<AudioSource> ().Play ();
		PlayerMovement._isSuper = false;
		_player.gameObject.GetComponent<MeshRenderer> ().enabled = true;

		PlayerMovement._isDead = false;
		PlayerMovement._runAnimation = true;
		_menu.SetActive (false);


//		Sequence mySequence = DOTween.Sequence();

		_music.gameObject.GetComponent<AudioSource> ().Play();
	}

	public void FadeOut() {
		StartCoroutine (DoFade ());
	}

	IEnumerator DoFade() {
		CanvasGroup canvasGroup = _menuCanvas;
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime/2;
			yield return null;
		}
		canvasGroup.interactable = false;
		_menu.SetActive (false);
		yield return null;
	}

}
