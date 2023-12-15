using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDialog : MonoBehaviour
{
    [SerializeField] private RecipeButton recipeButton;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
