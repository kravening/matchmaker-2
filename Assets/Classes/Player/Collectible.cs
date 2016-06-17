using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
	{
	[SerializeField]private Text countText;
	private int count;
	private int nrOfTotalCollectables;
	private int nrOfCollectedItems;

	void Start () 
	{
		GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
		nrOfTotalCollectables = collectables.Length;
		nrOfCollectedItems = 0;
		count = 0;
		UpdateUI();
	}
	public void AddCollectable()
	{
		nrOfCollectedItems++;

		count = count + 1;
		UpdateUI();

	}
	void UpdateUI()
	{
		countText.text = count.ToString ();
	}
}
