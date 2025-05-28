using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    void Update()
    {
        if(Random.Range(0, 100) < 10)
            ProcCube.CreateCube(this.transform.position);
    }
}
