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
        // button.onClick.AddListenerまではラムダ式関係なく、普通に関数を呼び出している。
        // その引数が一つ目の()の中身　この中身がラムダ式で書かれている。

        // ボタンが押されたら、MainSceneに遷移する
        button.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("MainScene");
        });
    }

}
