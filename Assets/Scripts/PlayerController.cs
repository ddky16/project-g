using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  private CinemachineFreeLook cameraMachine;
  private Rigidbody rb;

  private Ray ray;
  private Vector3 startMousePosition;
  private Vector3 endMousePosition;

  public bool isPlayerObject = false;

  private void Start()
  {
    cameraMachine = FindAnyObjectByType<CinemachineFreeLook>();
    rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out RaycastHit hitObject))
    {
      if (hitObject.collider.tag != "Player") return;
      isPlayerObject = true;
    }
  }

  private void OnMouseDown()
  {
    if (!isPlayerObject) return;

    startMousePosition = Input.mousePosition;
  }

  void OnMouseUp()
  {
    if (!isPlayerObject) return;

    endMousePosition = Input.mousePosition;
    AddForceBall();
  }

  private void AddForceBall()
  {
    float result = (startMousePosition - endMousePosition).magnitude;

    if (result < 100f) result = 100f;
    else if (result >= 500) result = 600f;

    rb.AddForce(Vector3.forward * (result / 10), ForceMode.Impulse);

    isPlayerObject = false;
  }
}
