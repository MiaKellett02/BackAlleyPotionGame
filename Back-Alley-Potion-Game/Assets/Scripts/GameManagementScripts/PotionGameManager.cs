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
	[SerializeField] private List<DayScriptableObject> m_days;

	//Private Variables.
	private GameStates m_currentGameState = GameStates.None;
	private Customer m_currentCustomer;
	private List<Customer> m_customers;
	private bool m_potionReturned = false;
	private bool m_finishedLoading = true;
	private PotionScriptableObject m_returnedPotionType;

	//Public Functions.

	public Customer GetCurrentCustomer() {
		return m_currentCustomer;
	}

	public void CustomerOrderTaken() {
		ChangeGameState(GameStates.PotionMaking);
	}

	public void CustomerOrderReturned() {
		m_potionReturned = true;
	}

	public PotionScriptableObject GetReturnedPotion() {
		return m_returnedPotionType;
	}

	//Unity Functions.
	private void Awake() {
		Instance = this;
	}

	private void Start() {
		//Subscribe to events.
		PotionCraftingUI.Instance.OnPotionReturn += PotionCraftingUI_OnPotionReturn;

		//Setup starting gamestate.
		ChangeGameState(m_startingGameState);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			FadeUI.Instance.FadeOut(2.0f, () => {
				SceneManager.LoadScene(0);
			});
		}

		if (!m_finishedLoading) {
			return;
		}

		switch (m_currentGameState) {
			case GameStates.None:
				Debug.Log("Gamestate not set.");
				break;
			case GameStates.FirstGameLoad:
				bool firstGameLoadAnimationFinished = true;
				if (firstGameLoadAnimationFinished) {
					ChangeGameState(GameStates.DayStart);
				}
				break;
			case GameStates.DayStart:
				bool dayStartAnimationFinished = true;
				if (dayStartAnimationFinished) {
					ChangeGameState(GameStates.CustomerOrdering);
				}
				break;
			case GameStates.CustomerOrdering:
				m_potionReturned = false;
				bool customerOrderMade = false;
				if (customerOrderMade) {
					ChangeGameState(GameStates.PotionMaking);
				}
				break;
			case GameStates.PotionMaking:
				break;
			case GameStates.ReturnPotion:
				bool anyCustomersLeft = m_customers.Count > 0;
				if (m_potionReturned && !anyCustomersLeft) {
					ChangeGameState(GameStates.NewspaperRevealed);
				} else if (m_potionReturned && anyCustomersLeft) {
					m_currentCustomer = null;
					ChangeGameState(GameStates.CustomerOrdering);
				}
				break;
			case GameStates.NewspaperRevealed:
				bool newspaperClosed = true;
				bool allDaysFinished = m_days.Count <= 0;
				if (newspaperClosed && !allDaysFinished) {
					ChangeGameState(GameStates.DayEnd);
				} else if (newspaperClosed && allDaysFinished) {
					ChangeGameState(GameStates.LastDayCompleted);
				}
				break;
			case GameStates.DayEnd:
				bool dayEndedAnimationFinished = true;
				if (dayEndedAnimationFinished) {
					ChangeGameState(GameStates.DayStart);
				}
				break;
			case GameStates.LastDayCompleted:
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
		m_finishedLoading = false;

		//Start the fade.
		if (a_newState != GameStates.FirstGameLoad && a_newState != GameStates.DayStart) {
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
		Debug.Log("New game state: " + m_currentGameState.ToString());
		m_finishedLoading = true;
		if (m_currentGameState != GameStates.FirstGameLoad) {
			//Fade back in.
			FadeUI.Instance.FadeIn(m_fadeTime, null);
		}
		if (m_currentGameState == GameStates.NewspaperRevealed) {
			//Remove the current day from the list.
			m_days.Remove(m_days[0]);
		} else if (m_currentGameState == GameStates.DayStart) {
			m_customers = this.GetCurrentDay().Initialise();
		} else if (m_currentGameState == GameStates.CustomerOrdering) {
			//Get the next customer.
			m_currentCustomer = m_customers[UnityEngine.Random.Range(0, m_customers.Count)];
			m_customers.Remove(m_currentCustomer);
		} else if (m_currentGameState == GameStates.LastDayCompleted) {
			FadeUI.Instance.FadeOut(5.0f, () => {
				SceneManager.LoadScene(0);
			});
		}

	}

	private void PotionCraftingUI_OnPotionReturn(object sender, PotionCraftingUI.OnPotionReturnEventArgs e) {
		m_returnedPotionType = e.returnedPotion;
		ChangeGameState(GameStates.ReturnPotion);
	}

	private DayScriptableObject GetCurrentDay() {
		return m_days[0];
	}
}
