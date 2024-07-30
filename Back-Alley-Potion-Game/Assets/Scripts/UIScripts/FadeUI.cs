///////////////////////////////////////////////////////////
/// Filename: FadeUI.cs
/// Author: Mia Kellett
/// Date Created: 26/07/2024
/// Purpose: To fade the screen to black and back.
///////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour {
	//Singleton.
	public static FadeUI Instance {
		get; private set;
	}

	//Events
	public event EventHandler OnFadeStarted;
	public event EventHandler OnFadeComplete;

	//Variables.
	private Coroutine m_fadeCoroutine = null;
	private Image m_fadeImage;

	//Public functions.
	public void FadeIn(float a_fadeTime, Action a_onFadeCompleted) {
		if (m_fadeCoroutine != null) {
			Debug.LogError("Already running a fade, cancelling previous one.");
			StopCoroutine(m_fadeCoroutine);
		}

		m_fadeCoroutine = StartCoroutine(FadeCoroutine(1, 0, a_fadeTime, a_onFadeCompleted));
	}

	public void FadeOut(float a_fadeTime, Action a_onFadeCompleted) {
		if (m_fadeCoroutine != null) {
			Debug.LogError("Already running a fade, cancelling previous one.");
			StopCoroutine(m_fadeCoroutine);
		}

		m_fadeCoroutine = StartCoroutine(FadeCoroutine(0, 1, a_fadeTime, a_onFadeCompleted));
	}

	//Unity Functions.
	private void Awake() {
		Instance = this;
		m_fadeCoroutine = null; //No coroutine should be running in awake, should wait till start function.
		m_fadeImage = GetComponent<Image>();
	}

	//Private Functions.
	private IEnumerator FadeCoroutine(float a_startAlpha, float a_endAlpha, float a_timerLength, Action a_onFadeCompleted) {
		//Setup colours.
		Color startColour = m_fadeImage.color;
		startColour.a = a_startAlpha;
		Color endColour = m_fadeImage.color;
		endColour.a = a_endAlpha;
		m_fadeImage.color = startColour;

		//Send fade started event.
		OnFadeStarted?.Invoke(this, EventArgs.Empty);

		//Run timer.
		float timer = 0;
		while (timer <= a_timerLength) {
			//Lerp the colours.
			float timeNormalized = timer / a_timerLength;
			m_fadeImage.color = Color.Lerp(startColour, endColour, timeNormalized);

			//Increment timer.
			timer += Time.deltaTime;

			//Wait a frame.
			yield return null;
		}

		//Set fade image colour to end colour
		m_fadeImage.color = endColour;

		//Set coroutine to null.
		m_fadeCoroutine = null;

		//Send finished event.
		OnFadeComplete?.Invoke(this, EventArgs.Empty);
		a_onFadeCompleted?.Invoke();
	}
}
