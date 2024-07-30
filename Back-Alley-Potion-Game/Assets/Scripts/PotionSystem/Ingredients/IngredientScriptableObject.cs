//////////////////////////////////////////////////////////////////////////////
/// Filename: IngredientScriptableObject.cs
/// Author: Mia Kellett
/// Date Created: 28/07/2024
/// Purpose: To store ingredient data.
//////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PotionGame/Ingredient")]
public class IngredientScriptableObject : ScriptableObject {
	//Variables.
	[SerializeField] private string m_ingredientName;
	[SerializeField] private Sprite m_ingredientVisual;

	//Functions.
	public string GetIngredientName() {
		return m_ingredientName;
	}

	public Sprite GetIngredientVisual() {
		return m_ingredientVisual;
	}

	//Unity Functions.
	private void OnEnable() {
		//Get the name of the ingredient.
		string path = AssetDatabase.GetAssetPath(this);
		string filename = System.IO.Path.GetFileName(path);
		m_ingredientName = filename.Split(".")[0];
		//Debug.Log("Filename: " + filename);
	}

	private void OnValidate() {
		//Get the name of the ingredient.
		string path = AssetDatabase.GetAssetPath(this);
		string filename = System.IO.Path.GetFileName(path);
		m_ingredientName = filename.Split(".")[0];
		//Debug.Log("Filename: " + filename);
	}
}
