using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private ItemsDialog itemDialog;
    [SerializeField] private RecipeDialog recipeDialog;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;

    [SerializeField] private Button itemButton;
    [SerializeField] private Button recipeButton;

    private void Start()
    {
        pausePanel.SetActive(false);
        // onClick.addListener: ボタンが押された時に実行する処理を登録する
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        itemButton.onClick.AddListener(ToggleItemDialog);
        recipeButton.onClick.AddListener(ToggleRecipeDialog);
    }

    private void Pause()
    {
        // timeScale: 時間の流れの速さを決める。1で通常速度、0で停止
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void ToggleItemDialog()
    {
        itemDialog.Toggle();
    }

    private void ToggleRecipeDialog()
    {
        recipeDialog.Toggle();
    }
}
