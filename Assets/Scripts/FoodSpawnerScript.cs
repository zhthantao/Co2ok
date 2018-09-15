using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour {

    public float objectScale;
    public GameObject ingredientsList;

    private GameObject gameBoard;
    private Queue<string> foodItemNameStack;


    // Use this for initialization
    void Start() {
        gameBoard = GameObject.Find("GameBoard");
        foodItemNameStack = gameBoard.GetComponent<GameBoardScript>().foodItemNameStack;
    }

    // Update is called once per frame
    void Update() {
        /* if (PhotonNetwork.isMasterClient)

         {
             PhotonView photonView = PhotonView.Get(this);
             photonView.RPC("GenerateFoodItems", PhotonTargets.All);
         }*/

        if (PhotonNetwork.isMasterClient)

        {

            if (foodItemNameStack == null)
                foodItemNameStack = gameBoard.GetComponent<GameBoardScript>().foodItemNameStack;
            if (gameObject.transform.childCount == 0 && foodItemNameStack.Count > 0)
            {
                Debug.Log("called GenerateFoodItems");
                string foodName = foodItemNameStack.Dequeue();
                string category = gameBoard.GetComponent<GameBoardScript>().foodItemCategoriesStack.Dequeue();
                /*GameObject fallingFood = Instantiate((Resources.Load("FoodPrefabs/" + category, typeof(GameObject))) as GameObject);
                 */

                object[] instanceData = new object[7];
                instanceData[0] = Random.Range(0.2f, 1f);
                instanceData[1] = (Random.value > 0.5f);
                instanceData[2] = (Random.value > 0.5f);
                instanceData[3] = foodName;
                instanceData[4] = category;
                instanceData[5] = this.name;
                instanceData[6] = objectScale;

                PhotonNetwork.Instantiate("FoodPrefabs/" + category, this.transform.position - new Vector3(0, 100, 0), Quaternion.identity, 0, instanceData);
                //GameObject fallingFood = PhotonNetwork.Instantiate("FoodPrefabs/" + category, this.transform.position - new Vector3(0, 100, 0), Quaternion.identity, 0, instanceData);


                //fallingFood.transform.SetParent(this.transform);


                /* fallingFood.transform.localPosition = Vector3.zero;
                 fallingFood.transform.localScale = Vector3.one * objectScale;
                 FallingFoodScript fallingFoodScript = fallingFood.GetComponent<FallingFoodScript>();
                 fallingFoodScript.fallingSpeed = Random.Range(0.2f, 1f);
                 fallingFoodScript.falling = (Random.value > 0.5f);
                 fallingFoodScript.clockwise = (Random.value > 0.5f);
                 fallingFoodScript.foodName = foodName;
                 fallingFoodScript.category = category;
                 */
                //fallingFoodScript.IngredientsList = ingredientsList;



                gameBoard.GetComponent<GameBoardScript>().foodItemNameStack.Enqueue(foodName);
                gameBoard.GetComponent<GameBoardScript>().foodItemCategoriesStack.Enqueue(category);
            }
        }

    }

    [PunRPC]
    void GenerateFoodItems()
    {
        if (PhotonNetwork.isMasterClient)

        {
            
            if (foodItemNameStack == null)
                foodItemNameStack = gameBoard.GetComponent<GameBoardScript>().foodItemNameStack;
            if (gameObject.transform.childCount == 0 && foodItemNameStack.Count > 0)
            {
                Debug.Log("called GenerateFoodItems");
                string foodName = foodItemNameStack.Dequeue();
                string category = gameBoard.GetComponent<GameBoardScript>().foodItemCategoriesStack.Dequeue();
                /*GameObject fallingFood = Instantiate((Resources.Load("FoodPrefabs/" + category, typeof(GameObject))) as GameObject);
                 */
                GameObject fallingFood = PhotonNetwork.Instantiate("FoodPrefabs/" + category, this.transform.position - new Vector3(0, 100, 0), Quaternion.identity, 0);


                fallingFood.transform.SetParent(this.transform);

                
                fallingFood.transform.localPosition = Vector3.zero;
                fallingFood.transform.localScale = Vector3.one * objectScale;
                FallingFoodScript fallingFoodScript = fallingFood.GetComponent<FallingFoodScript>();
                fallingFoodScript.fallingSpeed = Random.Range(0.2f, 1f);
                fallingFoodScript.falling = (Random.value > 0.5f);
                fallingFoodScript.clockwise = (Random.value > 0.5f);
                fallingFoodScript.foodName = foodName;
                fallingFoodScript.category = category;

                fallingFoodScript.IngredientsList = ingredientsList;



                gameBoard.GetComponent<GameBoardScript>().foodItemNameStack.Enqueue(foodName);
                gameBoard.GetComponent<GameBoardScript>().foodItemCategoriesStack.Enqueue(category);
            }
        }
    }
}
