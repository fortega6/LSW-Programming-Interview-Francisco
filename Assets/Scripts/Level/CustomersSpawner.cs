using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersSpawner : MonoBehaviour
{
    const float waitingTime = 5f;

    public List<Customer> customerList = new List<Customer>();
    public CustomerPooling customerPooling;
    public LevelEntrance [] levelEntrances;

    public void InstantiateCustomerOnLevel()
    {

        StartCoroutine(Spawn());
        //player.transform.parent = playerParent.transform;
        //this.followCamera.Follow = player.transform;

        // When player is instantiated and moved, reset path
        //playerPath.levelEntrance = null;
    }

    private IEnumerator Spawn()
    {
        LevelEntrance levelEntrance = levelEntrances[Random.Range(0, levelEntrances.Length)];
        Customer player = customerPooling.GetEnemyFromPool().GetComponent<Customer>();
        player.Target = 0;
        player.wayPoints = levelEntrance.GetRandomPath();
        Transform entrance = GetLevelEntrance(levelEntrance.entrance);

        player.transform.position = levelEntrance.transform.position;
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine(Spawn());
    }

    private Transform GetLevelEntrance(LevelEntranceSO playerEntrance)
    {
        if (playerEntrance == null)
        {
            // No path for player, instantiate it at default position
            return this.transform.GetChild(0).transform;
        }

        var levelEntrances = GameObject.FindObjectsOfType<LevelEntrance>();

        foreach (LevelEntrance levelEntrance in levelEntrances)
        {
            if (levelEntrance.entrance == playerEntrance)
            {
                return levelEntrance.gameObject.transform;
            }
        }

        // No entrance found, return default
        return this.transform.GetChild(0).transform;
    }
}
