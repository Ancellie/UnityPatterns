using UnityEngine;
using UnityEngine.UI;

public class MassegesController : MonoBehaviour
{
    public Text messages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        messages = GetComponent<Text>();
        messages.enabled = false;
    }

    public void ShowMessage(GameObject go)
    {
        messages.text = "You picked up an Item!";
        messages.enabled = true;
        Invoke("TurnOff", 2);
    }

    void TurnOff()
    {
        messages.enabled = false;
    }
}
