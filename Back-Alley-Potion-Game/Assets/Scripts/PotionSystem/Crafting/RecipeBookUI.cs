///////////////////////////////////////////////////////////
/// Filename: RecipeBookUI.cs
/// Author: Mia Kellett
/// Date Created: 30/07/2024
/// Purpose: To Display recipes.
///////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookUI : MonoBehaviour {
	//Variables to assign via the unity inspector.
	[SerializeField] private Button m_leftButton;
	[SerializeField] private Button m_rightButton;
	[SerializeField] private List<PotionScriptableObject> m_potions;
	[Header("Images")]
	[SerializeField] private Image m_ingredientOne;
	[SerializeField] private Image m_ingredientTwo;
	[SerializeField] private Image m_ingredientThree;
	[SerializeField] private Image m_potionImage;
	[SerializeField] private TextMeshProUGUI m_potionOutputText;

	//Private Variables.
	private int m_currentIndex = 0;

	//Unity Functions.
	private void Awake() {

		UpdateUI();
	}

	private void Start() {
		m_leftButton.onClick.AddListener(() => {
			m_currentIndex--;
			if (m_currentIndex < 0) {
				m_currentIndex = m_potions.Count - 1;
			}
			UpdateUI();
		});

		m_rightButton.onClick.AddListener(() => {
			m_currentIndex++;
			if (m_currentIndex > m_potions.Count - 1) {
				m_currentIndex = 0;
			}
			UpdateUI();
		});

		UpdateUI();
		this.gameObject.SetActive(false);
	}

	//Private Functions.
	private void UpdateUI() {
		PotionScriptableObject currentPotion = m_potions[m_currentIndex];
		IngredientScriptableObject[] ingredients = currentPotion.GetPotionIngredients();

		//Update ingredients.
		m_ingredientOne.sprite = ingredients[0].GetIngredientVisual();
		m_ingredientTwo.sprite = ingredients[1].GetIngredientVisual();
		m_ingredientThree.sprite = ingredients[2].GetIngredientVisual();

		//Update output potion
		m_potionImage.sprite = currentPotion.GetPotionVisual();
		m_potionOutputText.text = currentPotion.name;
	}
}
