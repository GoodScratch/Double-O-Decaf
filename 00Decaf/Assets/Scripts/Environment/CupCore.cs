using Unity.VisualScripting;
using UnityEngine;

public class CupCore : MonoBehaviour, IDrag
{
    private Vector3 startloco;
   [SerializeField] private Transform centerPoint;
    [SerializeField] private LayerMask cupInteract;

    private void Awake()
    {
       startloco = transform.position;
    }
    public  void OnStartDrag()
    {
        Debug.Log("pickUp!");
    }

    public void OnEndDrag()
    {
        float radius = 1;
        float distance = 5;

        Physics.SphereCast(centerPoint.position, radius, transform.forward, out RaycastHit hit, distance, cupInteract);
        if (hit.collider != null)
        {
            Debug.Log("spherecast!");

            if (hit.collider.TryGetComponent(out ICupInteract cupInteractable)) {

                cupInteractable.Interact();
                transform.position = cupInteractable.GetTransform().position;
            }

        }

        else
        {
            transform.position = startloco;
        }
    }
}

