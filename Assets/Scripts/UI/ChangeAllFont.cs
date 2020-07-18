using UnityEngine;
using UnityEngine.UI;

public class ChangeAllFont : MonoBehaviour
{

    public Font font;

    private void Start()
    {
        var text = FindObjectsOfType<Text>();
        
        foreach(var item in text) {
            item.font = font;
        }

        Debug.Log("Change All Font");
    }
}