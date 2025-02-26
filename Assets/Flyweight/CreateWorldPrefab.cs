using UnityEngine;

public class CreateWorldPrefab : MonoBehaviour
{
    public int width;
    public int depth;
    public GameObject cube;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < width; x++)
            for (int z = 0; z < depth; z++)
            {
                Vector3 pos = new Vector3(x, 
                    Mathf.PerlinNoise(x * 0.25f, z * 0.25f) * 3, 
                    z);
                GameObject go = Instantiate(cube, pos, Quaternion.identity);
            }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
