using UnityEngine;

public class SpawnerP : MonoBehaviour
{
    public GameObject cubeprefab;
    public GameObject spherePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) < 10)
            Instantiate(cubeprefab, transform.position, Quaternion.identity);
        else if (Random.Range(0, 100) < 10)
            Instantiate(spherePrefab, transform.position, Quaternion.identity);
            
    }
}
