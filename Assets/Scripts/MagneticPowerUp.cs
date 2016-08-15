using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MagneticPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		DOTween.Init ();

		this.transform.DORotate (new Vector3 (0, 0, 180), 0.4f).SetLoops (-1, LoopType.Yoyo);
	
	}
	

}
