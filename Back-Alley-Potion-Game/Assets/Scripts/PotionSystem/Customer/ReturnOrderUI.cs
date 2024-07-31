using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReturnOrderUI : MonoBehaviour
{
   //Variables to assign via the unity inspector.
	[Header("Speech Bubble")]
	[SerializeField] private GameObject m_speechBubble;
	[SerializeField] private TextMeshProUGUI m_speechBubbleText;
	[SerializeField] private AudioClip m_speechBubbleAudio;

	//Unity Functions.
	private void Start() {
		//Subscribe to event listeners
		PotionGameManager.Instance.OnGameStateChanged += PotionGameManager_OnGameStateChanged;

		Hide();
	}

	//Private Functions.
	private void CustomerEntrySequence() {
		if (PotionGameManager.Instance.GetCurrentCustomer() == null) {
			Debug.LogError("Current customer is null.");
		}
		bool success = PotionGameManager.Instance.GetCurrentCustomer().desiredPotion == PotionGameManager.Instance.GetReturnedPotion();
		string message;
		if (success) {
			message = PotionGameManager.Instance.GetCurrentCustomer().successReaction;
		} else {
			message = PotionGameManager.Instance.GetCurrentCustomer().failureReaction;
		}
		StartCoroutine(ShowMessage(message, 3.0f));
	}

	private IEnumerator ShowMessage(string a_message, float a_time) {
		AudioSource.PlayClipAtPoint(m_speechBubbleAudio, Camera.main.transform.position);
		m_speechBubble.SetActive(true);
		m_speechBubbleText.text = a_message;
		yield return new WaitForSeconds(a_time);
		m_speechBubble.SetActive(false);
		yield return new WaitForSeconds(2.0f);
		PotionGameManager.Instance.CustomerOrderReturned();
	}

	private void PotionGameManager_OnGameStateChanged(object sender, PotionGameManager.OnGameStateChangedEventArgs e) {
		if (e.newGameState == PotionGameManager.GameStates.ReturnPotion) {
			//Show the UI.
			Show();
			m_speechBubble.SetActive(false);

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