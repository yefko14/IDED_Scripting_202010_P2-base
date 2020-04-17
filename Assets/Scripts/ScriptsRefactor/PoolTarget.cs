using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTarget : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] int length = 20;

    IPool[] items;
    GameObject[] objects;
    int index = 0;

    private void Awake()
    {
        items = new IPool[length];
        objects = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            objects[i] = Instantiate(item, transform.position, Quaternion.identity);
            objects[i].transform.parent = transform;
            items[i] = objects[i].GetComponent<IPool>();
            items[i].Instantiate();
        }
    }
    public GameObject GetItem()
    {
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0F, 1F), 1F, transform.position.z));
        items[index].Spawn(spawnPoint);
        GameObject tmp = objects[index];
        index++;
        if (index >= items.Length) index = 0;
        return tmp;
    }
}
