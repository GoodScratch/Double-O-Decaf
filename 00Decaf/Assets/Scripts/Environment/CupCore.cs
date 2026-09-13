using Unity.VisualScripting;
using UnityEngine;

public class CupCore : MonoBehaviour, IDrag, IAddToCup
{
    private Vector3 startloco;
    [SerializeField] private GameObject startlocoTransform;
   [SerializeField] private Transform centerPoint;
    [SerializeField] private LayerMask cupInteract;

    private int sugar = 0;
    private int cream = 0;
    private int coffee = 0;
    private int cin = 0;
    private int foam = 0;

    private void Awake()
    {
       startloco = transform.position;
 
    }

    private void UpdateLook()
    {
        if (coffee != 0)
        {
            Debug.Log("coffeeadded!");
        }
    }

    public Vector3 GetStartPoint()
    {
        return startloco;
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
            hit.collider.TryGetComponent(out ICupInteract cupInteractable);
                
                cupInteractable.Interact();
                transform.position = cupInteractable.GetTransform().position;
            }

        else
        {
            transform.position = startloco;
        }
    }

    public void AddSugar(int addSugar)
    {
        sugar += addSugar;
    }

    public void AddCoffee(int addCoffee)
    {
        coffee += addCoffee;
        Debug.Log("interact!");
        UpdateLook();
    }

    public void AddCinnamon(int addCin)
    {
        cin += addCin;
    }

    public void AddCream(int addCream)
    {
        cream += addCream;  
    }

    public void AddFoam(int addFoam)
    {
        foam += addFoam;
    }
}

