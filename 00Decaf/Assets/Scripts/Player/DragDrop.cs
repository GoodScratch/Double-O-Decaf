using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class DragDrop : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;

    private Camera mainCamera;
    private float mouseDragSpeed = .1f;
    private float mouseDragPhysicsSpeed = 10f;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        {
            mainCamera = Camera.main;
        }
    }
    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDrag>() != null)
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

    }
    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        Vector3 startDistance = (Vector3)(iDragComponent?.GetStartPoint());
        float startZ = startDistance.z;
        float initialDistance = Vector3.Distance(startDistance, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        iDragComponent?.OnStartDrag();
        while (mouseClick.ReadValue<float>() != 0) {

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y,startZ);
                rb.linearVelocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, startZ), ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }


        } iDragComponent?.OnEndDrag();
    
    }
}


    


