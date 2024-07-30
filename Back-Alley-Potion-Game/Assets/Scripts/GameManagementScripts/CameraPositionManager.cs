/////////////////////////////////////////////////////////////////////////////
/// Filename: CameraPositionManager.cs
/// Author: Mia Kellett
/// Date Created: 29/07/2024
/// Purpose: To position the camera in the correct place based on game state.
/////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionManager : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[SerializeField] private Transform m_defaultPos;
	[SerializeField] private Transform m_craftingPos;

	//Private Variables.

	//Unity Functions.
	private void Start() {
		PotionGameManager.Instance.OnGameStateChanged += PotionGameManager_OnGameStateChanged;
	}

	private void PotionGameManager_OnGameStateChanged(object sender, PotionGameManager.OnGameStateChangedEventArgs e) {
		switch (e.newGameState) {
			case PotionGameManager.GameStates.FirstGameLoad:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.DayStart:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.CustomerOrdering:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.PotionMaking:
				this.transform.position = m_craftingPos.position;
				this.transform.rotation = m_craftingPos.rotation;
				break;
			case PotionGameManager.GameStates.ReturnPotion:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.NewspaperRevealed:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.DayEnd:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
			case PotionGameManager.GameStates.LastDayCompleted:
				this.transform.position = m_defaultPos.position;
				this.transform.rotation = m_defaultPos.rotation;
				break;
		}
	}
}
