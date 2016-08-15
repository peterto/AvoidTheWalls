using UnityEngine;
using System.Collections;

public class MenuFadeScript : MonoBehaviour {

	public void FadeIn() {
		StartCoroutine (DoFade ());
	}

	IEnumerator DoFade() {
		CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / 2;
			yield return null;
		}
		canvasGroup.interactable = false;
		yield return null;
	}
}
