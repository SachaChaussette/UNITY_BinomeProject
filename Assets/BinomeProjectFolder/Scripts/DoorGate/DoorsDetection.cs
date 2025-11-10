using UnityEngine;

public class DoorsDetection : MonoBehaviour
{
    public delegate void FOnPlayerNear(bool _isPlayerNear);
    FOnPlayerNear onPlayerNear = null;

    public FOnPlayerNear OnPlayerNear { get => onPlayerNear; set => onPlayerNear = value; }

    [SerializeField] SphereCollider sphereCollider = null;
    [SerializeField] bool isPlayerNear = false;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sphereCollider == null) return;

        if (!other.gameObject.GetComponent(typeof(Player))) return;

        isPlayerNear = true;

        onPlayerNear?.Invoke(isPlayerNear);
    }

    private void OnTriggerExit(Collider other)
    {
        if (sphereCollider == null) return;

        if (!other.gameObject.GetComponent(typeof(Player))) return;

        isPlayerNear = false;

        onPlayerNear?.Invoke(isPlayerNear);
    }

    private void OnDrawGizmos()
    {
        if(!sphereCollider) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius * transform.localScale.x);
        Gizmos.color = Color.white;

    }
}
