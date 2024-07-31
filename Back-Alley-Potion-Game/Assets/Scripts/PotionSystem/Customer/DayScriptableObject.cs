using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName = "PotionGame/Day")]
public class DayScriptableObject : ScriptableObject {
	//Variables.
	[SerializeField] private List<PotionScriptableObject> m_possiblePotions;
	[SerializeField] private Customer m_specialCustomer;
	[SerializeField] private int m_numRandomCustomers;

	private bool dayInitialized = false;

	//Public Functions.
	public List<Customer> Initialise() {
		Debug.Log("Initialising Day");
		//Initialize customers.
		List<Customer> customers = new List<Customer>();
		customers.Add(m_specialCustomer);
		List<string> possibleFail = new List<string> {
				"Oi I wanted that potion.",
				"Oh that's a shame, I'll have to take my business elsewhere.",
				"You'll pay for this Wizard >:(",
				"Watch your back shopkeeper",
				"..............",
				"How could you, I was looking forward to using that.",
				"When are you going to learn you don't mess with the likes of me (whispers 'damn wizards').",
				"Today is the last day you will ever make a mistake like not giving me what I want.",
				"Are you dumb? How could you get that wrong?",
				"I have no words....."
			};
		List<string> possibleSuccess = new List<string> {
				"Muahahaha you will be rewarded in time Wizard.",
				"Oi thank you boss man, you're a legend innit.",
				"Great work Wizard, I will be back for more.....",
				"Thanks Wizard, the potion will be put to good use.",
				"You work towards my 'cause' is appreciated shopkeep. I will make sure to keep note of that.",
				"If any of my resistance buddies need a potion I'll recommend you. Good work Wizard.",
				"Am I mad for taking whatever potion you give me? Maybe? But it looks right to me. Good work.",
			};
		List<string> possibleOrderMessage = new List<string> {
			"Oi bossman, give a ",
			"Shhh be quiet, I can't have anybody knowing I want a ",
			"Wizard, you will make me a ",
			"On the sly could you make me a "
		};
		for (int i = 0; i < m_numRandomCustomers; i++) {
			//Create a random message
			string message = possibleOrderMessage[UnityEngine.Random.Range(0, possibleOrderMessage.Count)];

			//Get a possible potion for that customer.
			PotionScriptableObject potion = m_possiblePotions[UnityEngine.Random.Range(0, m_possiblePotions.Count)];

			//Create a fail reaction
			string failReaction = possibleFail[UnityEngine.Random.Range(0, possibleFail.Count)];

			//Create a successReaction.
			string successReaction = possibleSuccess[UnityEngine.Random.Range(0, possibleSuccess.Count)];

			//Create a random customer and add them to the list..
			Customer newCustomer = new Customer(message, potion, failReaction, successReaction, false);
			customers.Add(newCustomer);
		}

		return customers;
	}

	//Private Functions.
}
