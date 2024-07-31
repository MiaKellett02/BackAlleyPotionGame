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

	private List<Customer> m_customers = new List<Customer>();
	private bool dayInitialized = false;

	//Public Functions.
	public void Initialise() {
		//Initialize customers.
		m_customers = new List<Customer>();
		m_customers.Add(m_specialCustomer);
		List<string> possibleFail = new List<string> {
				"Oi I wanted that potion.",
				"Oh that's a shame, I'll have to take my business elsewhere.",
				"You'll pay for this Wizard >:(",
				"Watch your back shopkeeper",
				".............."
			};
		List<string> possibleSuccess = new List<string> {
				"Muahahaha you will be rewarded in time Wizard.",
				"Oi thank you boss man, you're a legend innit.",
				"Great work Wizard, I will be back for more....."
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
			m_customers.Add(newCustomer);
		}
	}

	public Customer GetNextCustomer() {
		if(dayInitialized == false) {
			Initialise();
		}

		if(m_customers.Count <= 0) {
			return null; //No customers left.
		}

		//get the customer.
		Customer nextCustomer = m_customers[UnityEngine.Random.Range(0, m_customers.Count)];

		//Remove them from the pool.
		m_customers.Remove(nextCustomer);

		//Return the customer.
		return nextCustomer;
	}

	public bool AnyCustomersLeft() {
		if(m_customers.Count <= 0) {
			return false;
		} else {
			return true;
		}
	}

	//Private Functions.
}
