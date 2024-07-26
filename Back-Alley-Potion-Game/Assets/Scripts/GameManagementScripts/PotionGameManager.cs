////////////////////////////////////////////////////////////////
/// Filename: PotionGameManager.cs
/// Author: Mia Kellett
/// Date Created: 26/07/2024
/// Purpose: To manage all of the game states.
/// ////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionGameManager : MonoBehaviour {
	//Enums.
	public enum GameStates {
		None = -1,
		FirstGameLoad,
		DayStart,
		CustomerOrdering,
		PotionMaking,
		ReturnPotion,
		NewspaperRevealed,
		DayEnd,
		LastDayCompleted
	}

	//Make the game manager a singleton.
	public static PotionGameManager Instance {
		get; private set;
	}

	//Events.
	public class OnGameStateChangedEventArgs : EventArgs {
		public GameStates newGameState;
	}
	public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

	//Variables to assign via the unity inspector.
	[SerializeField] private float m_fadeTime = 1.0f;
	[SerializeField] private GameStates m_startingGameState = GameStates.FirstGameLoad;

	//Private Variables.
	private GameStates m_currentGameState = GameStates.None;

	//Public Functions.

	//Unity Functions.
	private void Awake() {
		Instance = this;
	}

	private void Start() {
		//Subscribe to events.

		//Setup starting gamestate.
		ChangeGameState(m_startingGameState);
	}

	private void Update() {
		switch (m_currentGameState) {
			case GameStates.None:
				Debug.Log("Gamestate not set.");
				break;
			case GameStates.FirstGameLoad:
				bool firstGameLoadAnimationFinished = false;
				if (firstGameLoadAnimationFinished) {
					ChangeGameState(GameStates.DayStart);
				}
				break;
			case GameStates.DayStart:
				bool dayStartAnimationFinished = false;
				if (dayStartAnimationFinished) {
					ChangeGameState(GameStates.CustomerOrdering);
				}
				break;
			case GameStates.CustomerOrdering:
				bool customerOrderMade = false;
				if (customerOrderMade) {
					ChangeGameState(GameStates.PotionMaking);
				}
				break;
			case GameStates.PotionMaking:
				bool potionFinished = false;
				if (potionFinished) {
					ChangeGameState(GameStates.ReturnPotion);
				}
				break;
			case GameStates.ReturnPotion:
				bool potionReturned = false;
				if (potionReturned) {
					ChangeGameState(GameStates.NewspaperRevealed);
				}
				break;
			case GameStates.NewspaperRevealed:
				bool newspaperClosed = false;
				if (newspaperClosed) {
					ChangeGameState(GameStates.DayEnd);
				}
				break;
			case GameStates.DayEnd:
				bool dayEndedAnimationFinished = false;
				bool allDaysFinished = false;
				if (dayEndedAnimationFinished && allDaysFinished) {
					ChangeGameState(GameStates.LastDayCompleted);
				} else {
					ChangeGameState(GameStates.DayStart);
				}
				break;
			case GameStates.LastDayCompleted:
				Debug.LogError("HAVE NOT IMPLEMENTED SCENE LOADING YET.");
				break;
			default:
				break;
		}
	}

	private void OnValidate() {
		if (m_currentGameState != GameStates.None) {
			ChangeGameState(m_startingGameState);
		}
	}

	//Private Functions.
	private void ChangeGameState(GameStates a_newState) {
		//Start the fade.
		if (a_newState != GameStates.FirstGameLoad) {
			FadeUI.Instance.FadeOut(m_fadeTime, FinishChangingGameState);
		} else {
			FadeUI.Instance.FadeIn(m_fadeTime, FinishChangingGameState);
		}

		//Set what the current state should be.
		m_currentGameState = a_newState;
	}

	private void FinishChangingGameState() {
		OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs {
			newGameState = m_currentGameState
		});

		if (m_currentGameState != GameStates.FirstGameLoad) {
			//Fade back in.
			FadeUI.Instance.FadeIn(m_fadeTime, null);
		}
	}
}
