﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class MainMenuController : MonoBehaviour {


	public RectTransform _playButton;
	public Material _bottomPanelMaterial;
	public Material _middlePanelMaterial;

	// Use this for initialization
	void Start () {
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
		Sequence mySequence = DOTween.Sequence();
//		mySequence.Append(_middlePanelMaterial.DOFade (1, 1f));
//		mySequence.Append(_middlePanelMaterial.DOFade (0, 1f));
		mySequence.Append (_middlePanelMaterial.DOColor (Color.white, 0.2f));
		mySequence.Append (_middlePanelMaterial.DOColor (Color.black, 0.2f));
		mySequence.SetLoops (-1, LoopType.Yoyo);
//		_middlePanelMaterial.DOColor (Color.white, 1f).SetLoops(-1, LoopType.Yoyo);
//		_middlePanelMaterial.DOColor (Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
//		mySequence.Play ();

		int highScore = PlayerPrefs.GetInt ("HighScore");
		if (highScore <= 0) {
			PlayerPrefs.SetInt ("HighScore", 0);
		}

	}
	
	// Update is called once per frame
	void Update () {


//		_bottomPanelMaterial.DOColor (Color.blue, 1f).SetLoops(-1, LoopType.Yoyo);

//		_bottomPanelMaterial.DOFade(1, 1f).SetLoops(-1, LoopType.Yoyo);

//		cubeA.DOMove(new Vector3(-2, 2, 0), 1).SetRelative().SetLoops(-1, LoopType.Yoyo);
	}

	public void PlayGame() {
		SceneManager.LoadScene (1);
	}
}
