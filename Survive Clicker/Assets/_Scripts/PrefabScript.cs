using UnityEngine;

public class PrefabScript : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.isGameOver == true) { Destroy(gameObject); }
    }
}
