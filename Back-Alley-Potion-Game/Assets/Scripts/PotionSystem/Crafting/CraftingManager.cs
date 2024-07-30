/////////////////////////////////////////////////////////////////////////////
/// Filename: CraftingManager.cs
/// Author: Mia Kellett
/// Date Created: 29/07/2024
/// Purpose: To manage whenever the user wants to craft a potion.
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {
	//Singleton.
	public static CraftingManager Instance;

	//Events
	public class OnPotionCraftedEventArgs : EventArgs {
		public PotionScriptableObject craftedPotion;
	}
	public event EventHandler<OnPotionCraftedEventArgs> OnPotionCrafted;

	//Variables to assign via the unity inspector.
	[SerializeField] private PotionScriptableObject m_failedPotion;
	[SerializeField] private List<PotionScriptableObject> m_possiblePotions;
	[SerializeField] private int m_cauldronSize = 3;

	//Private Variables.
	private List<IngredientScriptableObject> m_ingredientsInCauldron;

	//Public Functions.
	public void AddIngredientToCauldron(IngredientScriptableObject a_inredient) {
		//Add the ingredient to the cauldron.
		m_ingredientsInCauldron.Add(a_inredient);

		//Check if the cauldron is full.
		if (m_ingredientsInCauldron.Count >= m_cauldronSize) {
			//Now the cauldron is full, craft the potion.
			CraftPotion();

			//Clear the cauldron.
			m_ingredientsInCauldron.Clear();
		}
	}

	//Unity Functions.
	private void Awake() {
		Instance = this;
		m_ingredientsInCauldron = new List<IngredientScriptableObject>();
	}

	//Private Functions.
	private void CraftPotion() {
		foreach(PotionScriptableObject potion in m_possiblePotions) {
			if(DoCorrectIngredientsExist(potion.GetPotionIngredients(), m_ingredientsInCauldron.ToArray())) {
				Debug.Log("Potion crafting success.");
				OnPotionCrafted?.Invoke(this, new OnPotionCraftedEventArgs {
					craftedPotion = potion
				});
				return;
			}
		}

		Debug.Log("Potion crafting failure.");
		OnPotionCrafted?.Invoke(this, new OnPotionCraftedEventArgs {
			craftedPotion = m_failedPotion
		});
	}

	private bool DoCorrectIngredientsExist(IngredientScriptableObject[] a_arr1, IngredientScriptableObject[] a_arr2) {
		if(a_arr1.Length != a_arr2.Length) {
			return false;//Different num ingredients added.
		}

		//Create dictionaries to compare ingredient counts.
		Dictionary<IngredientScriptableObject, int> arr1Dictionary = new Dictionary<IngredientScriptableObject, int>();
		foreach(IngredientScriptableObject ingredient in a_arr1) {
			arr1Dictionary[ingredient]++;
		}

		Dictionary<IngredientScriptableObject, int> arr2Dictionary = new Dictionary<IngredientScriptableObject, int>();
		foreach(IngredientScriptableObject ingredient in a_arr2) {
			arr2Dictionary[ingredient]++;
		}

		//Compare the dictionaries.
		foreach(KeyValuePair<IngredientScriptableObject, int> keyValuePair in arr1Dictionary) {
			if(arr2Dictionary[keyValuePair.Key] != keyValuePair.Value) {
				//If any ingredient count doesn't match immediately return false.
				return false;
			}
		}

		//All ingredients match so return true.
		return true;
	}
}
