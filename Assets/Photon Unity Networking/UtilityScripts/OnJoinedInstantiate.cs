using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    public Transform SpawnPositionPickUp;

    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector
    public GameObject[] PickUpToInstantiate;   // set in 


    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPosition != null)
                {
                    spawnPos = this.SpawnPosition.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;

                PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
            }
        }

        if (this.PickUpToInstantiate != null && PhotonNetwork.isMasterClient)
        {
            foreach (GameObject o in this.PickUpToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPositionPickUp != null)
                {
                    spawnPos = this.SpawnPositionPickUp.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;

                PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
            }
        }
    }

    public void GenerateNewRound()
    {
        if (this.PickUpToInstantiate != null)
        {
            foreach (GameObject o in this.PickUpToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPositionPickUp != null)
                {
                    spawnPos = this.SpawnPositionPickUp.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;

                PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
            }
        }
    }
}
