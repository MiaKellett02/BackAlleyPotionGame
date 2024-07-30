////////////////////////////////////////////////////////////////
/// Filename: IngredientHolderScript.cs
/// Author: Mia Kellett
/// Date Createed: 29/07/2024
/// Purpose: To let ingredients be physical objects.
////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolderScript : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[SerializeField] private IngredientScriptableObject m_ingredient;
	[SerializeField] private SpriteRenderer m_ingredientVisual;

	//Public functions.
	public IngredientScriptableObject GetIngredientType() {
		return m_ingredient;
	}

	public void SetIngredientType(IngredientScriptableObject a_ingredientType) {
		m_ingredient = a_ingredientType;
		m_ingredientVisual.sprite = a_ingredientType.GetIngredientVisual();
	}
}
