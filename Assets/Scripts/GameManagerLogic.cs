using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour {
    private bool gameOver;
    private int gameTotalTimer;
    public FoodSpawnerScript fsscript;
    public Text finishText;

    public GameObject IngredientsList;

    // Use this for initialization
    void Start () {
        gameOver = false;
        gameTotalTimer = 18;
        IngredientsList = GameObject.Find("IngredientsList");
       // finishText.text = "";

        //if(PhotonNetwork.isMasterClient)
        // {
        // Invoke("GameOver", gameTotalTimer);
        // }
    }

    // Update is called once per frame
    void Update () {

        if (!PhotonNetwork.inRoom)
        {
            return;
        }

        if (PhotonNetwork.player.GetScore() > 1 && !gameOver)

        {
            //Debug.Log("Current Player score = " + PhotonNetwork.player.GetScore());
            Debug.Log("GameOver");
            //gameOver = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //PhotonNetwork.LoadLevel("MainGame_Hantao2");
            gameOver = true;
            GameOver();

        }
    }
    public void GameOver()
    {
        // fsscript.StopGame();

        GameObject foodSpawner  = GameObject.Find("FoodSpawner");
        if (foodSpawner != null)
        {
            foodSpawner.GetComponent<FoodSpawnerScript>().StopGame();
        }
        else
        {
            Debug.LogError("foodSpawner = null");
        }


        finishText.text = "You win!";


        PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("GameReset", PhotonTargets.AllBuffered);
        photonView.RPC("GameReset", PhotonTargets.AllViaServer);

        photonView.RPC("LoseMessage", PhotonTargets.Others);

        PhotonNetwork.player.SetScore(0);

    }

    public void GameRestart()
    {
        gameOver = false;
        finishText.text = "";
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ResetMessage", PhotonTargets.Others);

    }



    [PunRPC]
    void GameReset()
    {
        GameObject[] respawns;
        // Clean screen remaining objects 
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
        if (respawns != null)
        {
            foreach (GameObject res in respawns)
            {
                if (res.active)
                {
                    Destroy(res);
                }
            }
        }
        PhotonNetwork.player.SetScore(0);

        // Reset ingredient list 
        IngredientsList.GetComponent<IngredientListScript>().Reset();
    }

    [PunRPC]
    void LoseMessage()
    {
        finishText.text = "You lose..";
    }

    [PunRPC]
    void ResetMessage()
    {
        finishText.text = "";
    }
}
