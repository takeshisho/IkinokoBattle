using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public int GameScore = 0;

    // 下でシングルトンしているので、Score唯一のインスタンスを取得するためのプロパティになる。
    // これにより、全て共通のScoreインスタンスを参照することができる。
    public static Score Instance { get; private set; }


    // シングルトン
    private void Awake()
    {
        // シングルトンの呪文
        if (Instance == null)
        {
            // 自身をインスタンスとする
            Instance = this;
        }
        else
        {
            // インスタンスが複数存在しないように、既に存在していたら自身を消去する
            Destroy(gameObject);
        }
    }

    public void AddScore(int score = 1)
    {
        GameScore += score;
        scoreText.text = GameScore.ToString();
    }

}
