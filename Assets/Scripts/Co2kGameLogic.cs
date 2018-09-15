using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co2kGameLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!PhotonNetwork.inRoom)
        {
            return;
        }

        if (PhotonNetwork.player.GetScore() > 1)

        {
            //Debug.Log("Current Player score = " + PhotonNetwork.player.GetScore());

        }


    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        PhotonNetwork.LoadLevelAsync("Co2k2");
    }
}
