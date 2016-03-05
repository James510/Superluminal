using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour {
	public Text units;
	public static int numOfShipClasses= 5;
	public  int[] numOfShipInClass =new int[numOfShipClasses];
	// Use this for initialization
	void ReceiveList(GameObject[] shiplist)
	{
		string shipName = "";
		for (int x = 0; x < shiplist.Length; x++) //adds 1 to numofShipInClass for the respective class
			numOfShipInClass[shiplist[x].GetComponent<Unit>().shipClass]++;
		string temp ="";
		//for (int i = 0; i < 5; i++)
		//	shipClasses [i] = 0;
		units.text = "";
		/*for (int x = 0; x < shiplist.Length; x++) 
		{
			shipClasses[shiplist[x].GetComponent<Unit>().shipClass] += 1;
		}
		if (shipClasses [0] != 0)
			temp += "Utility: " + shipClasses [0]; 
		if (shipClasses [1] != 0)
			temp += "\nFrigate: " + shipClasses[1]; 
		if (shipClasses [2] != 0)
			temp += "\nCruiser: " + shipClasses[2];
		if (shipClasses [3] != 0)
			temp += "\nBattleship: " + shipClasses[3];
		if (shipClasses [4] != 0)
			temp += "\nCarrier: " + shipClasses[4];
			*/
		for (int y = 0; y <numOfShipClasses; y++) 
		{
			switch (y) {
			case 0:
				shipName = "Utility";
				break;
			case 1:
				shipName = "Frigate";
				break;
			case 2:
				shipName = "Cruiser";
				break;
			case 3:
				shipName = "Battleship";
				break;
			case 4:
				shipName = "Carrier";
				break;
				

			}
			if (numOfShipInClass[y] != 0) {
				temp +=  shipName +": " +  numOfShipInClass[y] + "\n";
			}
		}
		units.text = temp;
		for (int x = 0; x < numOfShipClasses; x++) //resets the ship num
			numOfShipInClass[x]=0;
	}
}
