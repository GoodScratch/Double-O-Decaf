using UnityEngine;

public class EspressoCore : MonoBehaviour, ICupInteract
{
    [SerializeField] private Transform cupSpot;
    [SerializeField] private LayerMask cupLayer;
    [SerializeField] private int coffee = 1;
  public Transform GetTransform()
    {
        return cupSpot;
    }

    public void Interact()
    {
        
        float radius = 1f;
        Collider[] colliderArray = Physics.OverlapSphere(cupSpot.position, radius, cupLayer);
        foreach (Collider collider in colliderArray)
        {
            collider.TryGetComponent<IAddToCup>(out IAddToCup addCup);
            addCup.AddCoffee(coffee);
            
        }


    }
}
