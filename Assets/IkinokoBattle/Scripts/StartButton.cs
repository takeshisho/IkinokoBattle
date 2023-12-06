using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();

        // 関数をラムダ式で書いてる。
        button.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("MainScene");
        });
    }

}
