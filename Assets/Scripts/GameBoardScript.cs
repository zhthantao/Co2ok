using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardScript : MonoBehaviour {

    public string[] foodItemNames;
    public Queue<string> foodItemNameStack;
    public string[] foodItemCategories;
    public Queue<string> foodItemCategoriesStack;

    // Use this for initialization
    void Start () {
        foodItemNameStack = new Queue<string>();
        foreach(string itemName in foodItemNames)
        {
            foodItemNameStack.Enqueue(itemName);
        }
        foodItemCategoriesStack = new Queue<string>();
        foreach (string itemCategory in foodItemCategories)
        {
            foodItemCategoriesStack.Enqueue(itemCategory);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
