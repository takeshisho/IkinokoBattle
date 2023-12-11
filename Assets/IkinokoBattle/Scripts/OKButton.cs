using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OKButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ボタンが押された時に呼ばれるメソッドを登録する
        GetComponent<Button>().onClick.AddListener(() => 
        {
            AudioManager.Instance.Play("ok");
        });
    }
}
