using System;
using UnityEngine;

public class CheckpointDetector : MonoBehaviour
{
    public event Action OnDetect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.GetComponent<Checkpoint>()) return;
        
        OnDetect?.Invoke();
    }
}