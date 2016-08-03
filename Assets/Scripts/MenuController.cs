using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

	[SerializeField] GameObject _menu;
	[SerializeField] GameObject _player;
	[SerializeField] GameObject _particleSystem;
	[SerializeField] GameObject _pickUps;
	[SerializeField] GameObject _music;
	public void RestartLevel() {
		
		_player.transform.position = new Vector2 (0, 0);
		//		_player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1000));
		//		_player.GetComponent<BoxCollider2D> ().enabled = true;

		//		rigidbody.AddForce (new Vector2 (0, _yspeed * _playerSpeed));
		//		_player.
//		GameObject ps = Instantiate(_particleSystem);
//		ps.transform.parent = _player.transform;
		PlayerMovement.pickUpCount = 0;
		Instantiate (_pickUps);
		_menu.gameObject.GetComponent<Animator> ().SetBool ("ExitMenu", true);
		print (_menu.gameObject.GetComponent<Animator> ().GetBool ("ExitMenu"));
		this.gameObject.GetComponent<AudioSource> ().Play ();
		Invoke ("AnimateMenu", 1f);
	}

	void AnimateMenu () {
		PlayerMovement._isDead = false;
		PlayerMovement._runAnimation = true;
		_menu.SetActive (false);
		_music.gameObject.GetComponent<AudioSource> ().Play();
	}

}
