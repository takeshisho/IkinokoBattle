using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobltemDropper : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float dropRate = 0.1f;
    [SerializeField] private Item[] itemPrefab;
    [SerializeField] private int number = 1; // アイテム出現個数

    private MobStatus _status;
    private bool _isDropInvoked;
    
    private void Start() {
        _status = GetComponent<MobStatus>();
    }

    private void Update()
    {
        if (_status.Life <= 0) DropIfNeeded();
    }

    private void DropIfNeeded()
    {
        if (_isDropInvoked) return;
        _isDropInvoked = true;

        if (Random.Range(0, 1f) >= dropRate) return;

        // number個のアイテム出現
        for (var i = 0; i < number; i++)
        {
            
            var item = Instantiate(itemPrefab[RandomItemIndex()], transform.position, Quaternion.identity);
            item.Initialize();
        }
    }

    private int RandomItemIndex()
    {
        Debug.Log(itemPrefab.Length);
        return Random.Range(0, itemPrefab.Length);
    }
}
