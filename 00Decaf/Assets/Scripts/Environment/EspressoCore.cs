using UnityEngine;

public class EspressoCore : MonoBehaviour, ICupInteract
{
    [SerializeField] private Transform cupSpot;
  public Transform GetTransform()
    {
        return cupSpot;
    }

    public void Interact()
    {
        Debug.Log("cup is filled!");
    }
}
