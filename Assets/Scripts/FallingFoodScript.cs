using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFoodScript : MonoBehaviour {

    public GameObject IngredientsList;

    // Set by spawner
    public float fallingSpeed;
    private RectTransform gameBoardRectTransform;
    public bool falling = true;
    public bool clockwise = true;
    public string foodName;
    public string category;

    private float uiPanelHeight;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        PhotonView pV = transform.GetComponent<PhotonView>();

        object[] data = pV.instantiationData;

        fallingSpeed = (float)data[0];
        falling = (bool)data[1];
        clockwise = (bool)data[2];
        foodName = (string)data[3];
        category = (string)data[4];
        string parentName = (string)data[5];
        float scaling = (float)data[6];

        IngredientsList = GameObject.Find("IngredientsList");

        transform.SetParent(GameObject.Find(parentName).transform);

        transform.localScale = Vector3.one * scaling;
        transform.localPosition = Vector3.zero;

        gameBoardRectTransform = GameObject.Find("GameBoard").GetComponent<RectTransform>();

        uiPanelHeight = gameBoardRectTransform.sizeDelta.y + 100;
        if (falling)
            startPosition = this.transform.localPosition;
        else
        {
            this.transform.localPosition = this.transform.localPosition - new Vector3(0, uiPanelHeight, 0);
            startPosition = this.transform.localPosition;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
       if (falling) {
            if (Mathf.Abs(this.transform.localPosition.y - startPosition.y) > uiPanelHeight)
                this.transform.localPosition = startPosition;
            this.transform.position = this.transform.position - new Vector3(0, fallingSpeed, 0);
        } else
        {
            if (Mathf.Abs(this.transform.localPosition.y - startPosition.y) > uiPanelHeight)
                this.transform.localPosition = startPosition;
            this.transform.position = this.transform.position - new Vector3(0, -fallingSpeed, 0);
        }
        if (clockwise)
            this.transform.Rotate(new Vector3(0, 1, 0), Space.Self);
        else
            this.transform.Rotate(new Vector3(0, -1, 0), Space.Self);


    }

    private void OnMouseDown()
    {
        string[] ingredients = IngredientsList.GetComponent<IngredientListScript>().ingredients;
        int index = System.Array.IndexOf(ingredients, category);
        if (index > -1)
        {
            if (!IngredientsList.GetComponent<IngredientListScript>().ingredientCollected[index])
            {
                IngredientsList.GetComponent<IngredientListScript>().ingredientCollected[index] = true;
                GameObject uiElement = IngredientsList.GetComponent<IngredientListScript>().ingredientUIElements[index];
                UnityEngine.UI.Text textElement = uiElement.GetComponentInChildren<UnityEngine.UI.Text>();
                textElement.text = textElement.text + " Done";

                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("DetroyGameObject", PhotonTargets.All);
                PhotonNetwork.player.AddScore(1);
            }
            
        }
        


    }

    [PunRPC]
    void DetroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
