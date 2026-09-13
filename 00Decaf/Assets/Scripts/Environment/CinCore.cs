using UnityEngine;

public class CinCore : MonoBehaviour, IDrag
{

    private Vector3 startloco;


    private void Awake()
    {
        startloco = transform.position;
    }
    public void OnStartDrag()
    {
        Debug.Log("pickup!");
    }

    public void OnEndDrag()
    {
        transform.position = startloco;
    }

    public Vector3 GetStartPoint()
    {
       return startloco;
    }
}
