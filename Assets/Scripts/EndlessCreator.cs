using System.Collections.Generic;

using UnityEngine;

public class EndlessCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabCityBlocks = new List<GameObject>();

    private List<GameObject> existingCityBlocks = new List<GameObject>();

    private int totalCount = 1;

    private void Start()
    {
        existingCityBlocks.Add(GameObject.Find("IntroBlock"));
    }

    private void FixedUpdate()
    {
        if (Camera.main.transform.position.x >= (totalCount - 1) * 64)
        {
            int rnd = Random.Range(0, prefabCityBlocks.Count);
            existingCityBlocks.Add(Instantiate(prefabCityBlocks[rnd], new Vector3(totalCount * 64, 0, 0), Quaternion.identity));
            totalCount++;

            if (existingCityBlocks.Count > 2)
            {
                Destroy(existingCityBlocks[0]);
                existingCityBlocks.RemoveAt(0);
            }
        }
    }
}
