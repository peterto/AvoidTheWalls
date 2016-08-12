using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MagneticPowerUpController : MonoBehaviour {


	[SerializeField] GameObject _magneticBoosterText;
	private bool _magneticBooster = false;
	private float _magneticBoosterDuration = 5f;
	private float _magneticBoosterOriginalTime;
	public Material _flicker;

	void Start() {
		_magneticBoosterOriginalTime = -5f;
	}

	// Update is called once per frame
	void Update () {
		
		if (Time.time < _magneticBoosterOriginalTime + _magneticBoosterDuration) {
			GameObject [] pickUps = GameObject.FindGameObjectsWithTag ("PickUp");
			for (int i = 0; i < pickUps.Length; i++) {
				float distanceBetweenPlayer = Vector3.Distance (pickUps [i].transform.position, this.gameObject.transform.position);
				if (distanceBetweenPlayer < 3) {
					//move pick up to player

					DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
					pickUps [i].transform.DOMove (this.gameObject.transform.position, 0.2f);
				}
			}

		} else {
			_magneticBooster = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("MagneticBooster")) {
			DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(_flicker.DOFade (1, 0.1f));
			mySequence.Append(_flicker.DOFade (0, 0.1f));
			Camera.main.DOShakeRotation (0.2f, 5f, 1, 1f);

			col.gameObject.GetComponent<AudioSource> ().Play ();

			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			col.gameObject.GetComponent<BoxCollider2D> ().enabled = false;


//			_flicker.DOFade (0, 0.1f);
			_magneticBoosterOriginalTime = Time.time;
			_magneticBooster = true;
		}
	}
}
