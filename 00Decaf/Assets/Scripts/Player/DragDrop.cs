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
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.OnStartDrag();
        while (mouseClick.ReadValue<float>() != 0) {

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.linearVelocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }


        } iDragComponent?.OnEndDrag();
    
    }
}


    


