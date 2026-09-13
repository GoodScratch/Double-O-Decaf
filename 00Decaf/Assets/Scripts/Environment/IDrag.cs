using UnityEngine;

public interface IDrag 
{
   public  void OnStartDrag();

  public void OnEndDrag();

    public Vector3 GetStartPoint();
}
