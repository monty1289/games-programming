using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;

    private List<GameObject> sheepList = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //sets sheep spawn positions
    private void SpawnSheep()
    {        
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }
    //Sets time between spawns
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();

            if(timeBetweenSpawns > 1){
            timeBetweenSpawns -= 10f * Time.deltaTime; //decreases time between spawns of a sheep too increace difficalty 

            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    //removes sheep from game list for better proformance 
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    //clears all sheep away when games finishes 
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}
