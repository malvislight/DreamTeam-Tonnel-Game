using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}