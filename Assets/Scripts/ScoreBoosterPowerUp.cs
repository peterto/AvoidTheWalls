using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ScoreBoosterPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		DOTween.Init ();
		this.transform.DORotate (new Vector3 (180, 180, 180), 0.4f).SetLoops (-1, LoopType.Yoyo);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
