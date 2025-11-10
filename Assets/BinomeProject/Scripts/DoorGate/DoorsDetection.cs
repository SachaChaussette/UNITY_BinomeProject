using UnityEngine;

public class DoorsDetection : MonoBehaviour
{
    public delegate void FOnPlayerNear(bool _isPlayerNear);
    FOnPlayerNear onPlayerNear = null;

    public FOnPlayerNear OnPlayerNear { get => onPlayerNear; set => onPlayerNear = value; }

    [SerializeField] SphereCollider collider = null;
    [SerializeField] bool isPlayerNear = false;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider == null) return;

        if (!other.gameObject.GetComponent(typeof(Player))) return;

        isPlayerNear = true;

        onPlayerNear?.Invoke(isPlayerNear);
    }

    private void OnTriggerExit(Collider other)
    {
        if (collider == null) return;

        if (!other.gameObject.GetComponent(typeof(Player))) return;

        isPlayerNear = false;

        onPlayerNear?.Invoke(isPlayerNear);
    }

    private void OnDrawGizmos()
    {
        if(!collider) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collider.radius * transform.localScale.x);
        Gizmos.color = Color.white;

    }
}
