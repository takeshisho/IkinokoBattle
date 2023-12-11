using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CancelButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            AudioManager.Instance.Play("cancel");
        });
    }
}
