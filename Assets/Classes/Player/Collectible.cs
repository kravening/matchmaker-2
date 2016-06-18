using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
	{
<<<<<<< HEAD
	public Text countText;
	private int _count;
	private int _nrOfTotalCollectables;
	private int _nrOfCollectedItems;
=======
	[SerializeField]private Text countText;
	private int count;
	private int nrOfTotalCollectables;
	private int nrOfCollectedItems;
>>>>>>> 9ac3fd791a13c736eaa8c67ebf1d3065ee8fc4af

	void Start () 
	{
		GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
<<<<<<< HEAD
		_nrOfTotalCollectables = collectables.Length;
		_nrOfCollectedItems = 0;
		Debug.Log (_nrOfTotalCollectables);
		_count = 0;
=======
		nrOfTotalCollectables = collectables.Length;
		nrOfCollectedItems = 0;
		count = 0;
>>>>>>> 9ac3fd791a13c736eaa8c67ebf1d3065ee8fc4af
		UpdateUI();
	}
	public void AddCollectable()
	{
<<<<<<< HEAD
		_nrOfCollectedItems++;
		Debug.Log ("You have " + _nrOfCollectedItems + " of the " + _nrOfTotalCollectables);
		_count = _count ++;
=======
		nrOfCollectedItems++;

		count = count + 1;
>>>>>>> 9ac3fd791a13c736eaa8c67ebf1d3065ee8fc4af
		UpdateUI();

	}
	void UpdateUI()
	{
<<<<<<< HEAD
		countText.text = "Score:" + _count.ToString ();
=======
		countText.text = count.ToString ();
>>>>>>> 9ac3fd791a13c736eaa8c67ebf1d3065ee8fc4af
	}
}
