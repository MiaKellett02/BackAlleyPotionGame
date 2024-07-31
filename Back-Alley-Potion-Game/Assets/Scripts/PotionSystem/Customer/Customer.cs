using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Customer {
	//Constructor.
	public Customer(string a_message, PotionScriptableObject a_desiredPotion, string a_failReaction, string a_successReaction, bool isSpecial) {
		message = a_message;
		desiredPotion = a_desiredPotion;
		failureReaction = a_failReaction;
		successReaction = a_successReaction;
		this.isSpecial = isSpecial;
	}

	//Variables.
	public string message = "Oi give me a ";
	public PotionScriptableObject desiredPotion;
	public string failureReaction = "Nooooo I needed that potion how could you.";
	public string successReaction = "Thanks boss man.";
	public bool isSpecial = false;
}
