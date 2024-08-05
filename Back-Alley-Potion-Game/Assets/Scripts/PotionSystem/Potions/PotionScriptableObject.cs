////////////////////////////////////////////////////////////////////////////////
/// Filename: PotionScriptableObject.cs
/// Author: Mia Kellett
/// Date Created: 28/07/2024
/// Purpose: To hold potion data and the crafting recipe for that potion.
////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PotionGame/Potion")]
public class PotionScriptableObject : ScriptableObject {
	//Variables.
	[SerializeField] private string m_potionName;
	[SerializeField][TextArea()] private string m_description;
	[SerializeField] private Sprite m_potionVisual;
	[SerializeField] private IngredientScriptableObject[] m_potionIngredients;

	//Functions.
	public string GetPotionName() {
		return m_potionName;
	}

	public string GetPotionDescription() {
		return m_description;
	}

	public Sprite GetPotionVisual() {
		return m_potionVisual;
	}

	public IngredientScriptableObject[] GetPotionIngredients() {
		return m_potionIngredients;
	}

	//Unity Functions.
	private void OnEnable() {
#if UNITY_EDITOR
		//Get the name of the ingredient.
		string path = AssetDatabase.GetAssetPath(this);
		string filename = System.IO.Path.GetFileName(path);
		m_potionName = filename.Split(".")[0];
		//Debug.Log("Filename: " + filename);
#endif
	}

	private void OnValidate() {
#if UNITY_EDITOR
		//Get the name of the ingredient.
		string path = AssetDatabase.GetAssetPath(this);
		string filename = System.IO.Path.GetFileName(path);
		m_potionName = filename.Split(".")[0];
		//Debug.Log("Filename: " + filename);
#endif
	}
}
