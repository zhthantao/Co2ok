using System.Collections.Generic;
using UnityEngine;

public class IngredientListScript : MonoBehaviour {

    public string[] ingredients;
    public GameObject ingredientsUiPrefab;
    public GameObject[] ingredientUIElements;
    public bool[] ingredientCollected;


    private List<GameObject> uiElements;

	// Use this for initialization
	void Start () {
        uiElements = new List<GameObject>();
		foreach(string ingredient in ingredients)
        {
            GameObject ingredientItem = Instantiate(ingredientsUiPrefab);
            ingredientItem.transform.SetParent(this.transform);
            ingredientItem.transform.localScale = Vector3.one;
            ingredientItem.transform.localPosition = new Vector3(ingredientItem.transform.localPosition.x, ingredientItem.transform.localPosition.y, 0);
            UnityEngine.UI.Text textScript = ingredientItem.GetComponentInChildren<UnityEngine.UI.Text>();
            textScript.text = ingredient;
            uiElements.Add(ingredientItem);
        }
        ingredientUIElements = uiElements.ToArray();
        ingredientCollected = new bool[ingredientUIElements.Length];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
