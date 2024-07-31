//////////////////////////////////////////////////////////////////////////
/// Filename: CustomerOrderingUI.cs
/// Author: Mia Kellett
/// Date Created: 31/07/2024
/// Purpose: To show what customers are saying when they order a potion.
//////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderingUI : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[Header("Speech Bubble")]
	[SerializeField] private GameObject m_speechBubble;
	[SerializeField] private TextMeshProUGUI m_speechBubbleText;
	[SerializeField] private Image m_customerRequest;
	[SerializeField] private AudioClip m_speechBubbleAudio;
	[Header("Buttons")]
	[SerializeField] private Button m_makePotionButton;

	//Unity Functions.
	private void Start() {
		//Subscribe to event listeners
		PotionGameManager.Instance.OnGameStateChanged += PotionGameManager_OnGameStateChanged;
		m_makePotionButton.onClick.AddListener(() => {
			PotionGameManager.Instance.CustomerOrderTaken();
		});

		Hide();
	}

	//Private Functions.
	private void CustomerEntrySequence() {
		if (PotionGameManager.Instance.GetCurrentCustomer() == null) {
			Debug.LogError("Current customer is null.");
		}
		PotionScriptableObject potion = PotionGameManager.Instance.GetCurrentCustomer().desiredPotion;
		string message = PotionGameManager.Instance.GetCurrentCustomer().message + potion.name;
		StartCoroutine(ShowMessage(message, 10.0f));
	}

	private IEnumerator ShowMessage(string a_message, float a_time) {
		AudioSource.PlayClipAtPoint(m_speechBubbleAudio, Camera.main.transform.position);
		m_speechBubble.SetActive(true);
		m_speechBubbleText.text = a_message;
		yield return new WaitForSeconds(a_time);
		m_speechBubble.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		m_makePotionButton.gameObject.SetActive(true);
	}

	private void PotionGameManager_OnGameStateChanged(object sender, PotionGameManager.OnGameStateChangedEventArgs e) {
		if (e.newGameState == PotionGameManager.GameStates.CustomerOrdering) {
			//Show the UI.
			Show();
			m_makePotionButton.gameObject.SetActive(false);

			//Start the customer entry sequence.
			Invoke("CustomerEntrySequence", 2.0f);
		} else {
			Hide();
		}
	}

	private void Show() {
		this.gameObject.SetActive(true);
	}

	private void Hide() {
		this.gameObject.SetActive(false);
	}
}
