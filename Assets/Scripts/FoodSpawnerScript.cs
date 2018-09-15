using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour {

    public float objectScale;
    public GameObject ingredientsList;

    private GameObject gameBoard;
    private Queue<string> foodItemNameStack;
    public static bool runTheGame = false;


    // Use this for initialization
    void Start() {
        gameBoard = GameObject.Find("GameBoard");
        foodItemNameStack = gameBoard.GetComponent<GameBoardScript>().foodItemNameStack;
    }

    // Update is called once per frame
    void Update() {
        
        if (PhotonNetwork.isMasterClient && runTheGame)

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

                gameBoard.GetComponent<GameBoardScript>().foodItemNameStack.Enqueue(foodName);
                gameBoard.GetComponent<GameBoardScript>().foodItemCategoriesStack.Enqueue(category);
            }
        }

    }

    public void runGame()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("UpdateRunGame", PhotonTargets.All);
    }


    [PunRPC]
    void UpdateRunGame()
    {
        runTheGame = true;
    }
}
