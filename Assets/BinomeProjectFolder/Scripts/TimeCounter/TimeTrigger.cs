using System;
using UnityEngine;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField] BoxCollider timeCollider = null;

    public event Action onTrigger = null;

    private void OnTriggerEnter(Collider _other)
    {

        if (!_other) return;

        if(!_other.GetComponent(typeof(Player))) return;

        onTrigger?.Invoke();
    }

    private void OnDrawGizmos()
    { 
        if(!timeCollider) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(timeCollider.transform.position, timeCollider.size);
        Gizmos.color = Color.white;
    }
}
