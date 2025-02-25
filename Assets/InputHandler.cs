using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;

    Animator anim;
    Command keyQ, keyW, keyE;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyQ = new PerformJump();
        keyW = new PerformKick();
        keyE = new PerformPunch();
        
        anim = actor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keyW.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyE.Execute(anim);
        }
    }
}
