/////////////////////////////////////////////////////////////////////////
/// Filename: CauldronScript.cs
/// Author: Mia Kellett
/// Date Created: 29/07/2024
/// Purpose: To handle retrieving ingredients.
/////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronScript : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[SerializeField] private AudioClip m_ingredientAddedSound;

	//Unity functions,
	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out IngredientHolderScript ingredientHolder)) {
			IngredientScriptableObject ingredient = ingredientHolder.GetIngredientType();
			CraftingManager.Instance.AddIngredientToCauldron(ingredient);
			Destroy(other.gameObject);
			Debug.Log(ingredient.name + " was added to the cauldron.");

			//Play sound.
			AudioSource.PlayClipAtPoint(m_ingredientAddedSound, this.transform.position);
		}
	}

	//Private Functions.
}
