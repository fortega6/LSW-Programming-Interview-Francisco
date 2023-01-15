using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPooling : MonoBehaviour
{
    public GameObject [] prefab;
    public int amount = 10;
    public int instantiateGap = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
        InvokeRepeating("GetCustomerFromPool", 1f, instantiateGap);
    }

    public void InitializePool()
    {
        for (int i = 0; i < amount; i++)
        {
            AddCustomerToPool();
        }
    }

    private void AddCustomerToPool()
    {
        GameObject enemy = Instantiate(prefab[Random.Range(0, prefab.Length)], this.transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);
    }

    public GameObject GetCustomerFromPool()
    {
        GameObject enemy = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                enemy = transform.GetChild(i).gameObject;
                break;
            }
        }

        if (enemy == null)
        {
            AddCustomerToPool();
            enemy = transform.GetChild(transform.childCount - 1).gameObject;
        }
        
        return enemy;
    }
}
