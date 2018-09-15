using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLogic : MonoBehaviour {
    private bool gameOver;
    private int gameTotalTimer;
    // Use this for initialization
    void Start () {
        gameOver = false;
        gameTotalTimer = 18;
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
            gameOver = true;
        }
    }
    public void GameOver()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("GameReset", PhotonTargets.All);
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
                Destroy(res);
            }
        }
        PhotonNetwork.player.SetScore(0);
    }





}
