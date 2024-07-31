using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSwapper : MonoBehaviour {
	[SerializeField] private GameObject specialCustomer;
	[SerializeField] private GameObject genericCustomer;

	// Start is called before the first frame update
	void Start() {
		PotionGameManager.Instance.OnGameStateChanged += PotionGameManager_OnGameStateChanged;
	}

	public void SwapCustomer() {

	}

	private void PotionGameManager_OnGameStateChanged(object sender, PotionGameManager.OnGameStateChangedEventArgs e) {
		Customer customer = PotionGameManager.Instance.GetCurrentCustomer();
		if (customer != null && e.newGameState == PotionGameManager.GameStates.CustomerOrdering || e.newGameState == PotionGameManager.GameStates.ReturnPotion) {
			//Activate correct customer.
			bool isSpecialCustomer = customer.isSpecial;
			if (isSpecialCustomer) {
				specialCustomer.SetActive(true);
				genericCustomer.SetActive(false);
			} else {
				specialCustomer.SetActive(false);
				genericCustomer.SetActive(true);
			}
			return;
		} else if(e.newGameState == PotionGameManager.GameStates.CustomerOrdering || e.newGameState == PotionGameManager.GameStates.ReturnPotion) {
			//Default to generic.
			specialCustomer.SetActive(false);
			genericCustomer.SetActive(true);
			return;
		}

		specialCustomer.SetActive(false);
		genericCustomer.SetActive(false);
	}

	// Update is called once per frame
	void Update() {

	}
}
