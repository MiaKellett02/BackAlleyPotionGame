///////////////////////////////////////////////////////////////////////////////
/// Filename: PotionCraftingUI.cs
/// Author: Mia Kellett
/// Date Created: 29/07/2024
/// Purpose: To handle showing the user any UI related to crafting.
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCraftingUI : MonoBehaviour {
	//Structs.
	[System.Serializable]
	public struct CraftingUIStruct {
		public IngredientScriptableObject ingredient;
		public Button button;
	}

	//Variables to assign via the unity inspector.
	[SerializeField] private List<CraftingUIStruct> m_craftingUI;
	[SerializeField] private GameObject m_ingredientPrefab;
	[SerializeField] private Vector3 m_ingredientSpawnPos;
	[SerializeField] private GameObject m_potionPrefab;
	[SerializeField] private Vector3 m_potionSpawnPos;

	//Variables.
	private PotionHolderScript m_currentPotion;

	//Unity Functions.
	private void Start() {
		//Subscribe to event listeners
		PotionGameManager.Instance.OnGameStateChanged += PotionGameManager_OnGameStateChanged;
		CraftingManager.Instance.OnPotionCrafted += CraftingManager_OnPotionCrafted;
		for (int i = 0; i < m_craftingUI.Count; i++) {
			CraftingUIStruct craftingUI = m_craftingUI[i];
			craftingUI.button.onClick.AddListener(() => {
				GameObject ingredient = Instantiate(m_ingredientPrefab);
				ingredient.transform.position = m_ingredientSpawnPos;
				ingredient.GetComponent<IngredientHolderScript>().SetIngredientType(craftingUI.ingredient);
				Debug.Log("Should be spawning ingredient.");
			});
		}

		Hide();
	}

	private void CraftingManager_OnPotionCrafted(object sender, CraftingManager.OnPotionCraftedEventArgs e) {
		Debug.Log(e.craftedPotion.name + " was crafted");
		//If there's a potion already despawn it.
		if(m_currentPotion != null) {
			Destroy(m_currentPotion.gameObject);
		}

		//Spawn the new potion.
		GameObject potion = Instantiate(m_potionPrefab);
		potion.transform.position = m_potionSpawnPos;
		m_currentPotion = potion.GetComponent<PotionHolderScript>();
		m_currentPotion.SetPotionType(e.craftedPotion);
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(m_ingredientSpawnPos, 0.2f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(m_potionSpawnPos, 0.25f);
	}

	//Private Functions.
	private void PotionGameManager_OnGameStateChanged(object sender, PotionGameManager.OnGameStateChangedEventArgs e) {
		if (e.newGameState == PotionGameManager.GameStates.PotionMaking) {
			Show();
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
