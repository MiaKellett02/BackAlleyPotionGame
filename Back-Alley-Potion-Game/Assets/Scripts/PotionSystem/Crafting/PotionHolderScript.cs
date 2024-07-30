////////////////////////////////////////////////////////////////////////////
/// Filename: PotionHolderScript.cs
/// Author: Mia Kellett
/// Date Created: 30/07/2024
/// Purpose: To make potions a physical object.
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHolderScript : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[SerializeField] private PotionScriptableObject m_potionType;
	[SerializeField] private SpriteRenderer m_potionVisual;

	//Public functions.
	public PotionScriptableObject GetPotionType() {
		return m_potionType;
	}

	public void SetPotionType(PotionScriptableObject a_ingredientType) {
		m_potionType = a_ingredientType;
		m_potionVisual.sprite = a_ingredientType.GetPotionVisual();
	}
}
