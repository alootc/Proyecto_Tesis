using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaskController : MonoBehaviour
{
    public Transform headTransform; // Transform de la cabeza del jugador
    public float proximityDistance = 0.3f; // Distancia para detectar si la m�scara est� cerca de la cara
    public Vector3 maskOffset = new Vector3(0, 0, 0.1f); // Ajuste de posici�n de la m�scara
    public Transform desiredPosition; // Posici�n donde debe ajustarse la m�scara
    public float moveSpeed = 5f; // Velocidad de movimiento al colocarse

    private XRGrabInteractable grabInteractable;
    private Rigidbody maskRigidbody;
    private bool isMovingToFace = false;
    private bool isAttached = false; // Indica si la m�scara est� en la cara

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        maskRigidbody = GetComponent<Rigidbody>();

        // Suscribirse a eventos de agarre y soltar
        grabInteractable.onSelectEntered.AddListener(OnGrabbed);
        grabInteractable.onSelectExited.AddListener(OnReleased);
    }

    void Update()
    {
        if (isMovingToFace)
        {
            // Mover suavemente la m�scara hacia la cara
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredPosition.rotation, moveSpeed * Time.deltaTime);

            // Si la m�scara llega a la posici�n deseada, la fijamos
            if (Vector3.Distance(transform.position, desiredPosition.position) < 0.01f)
            {
                AttachToFace();
            }
            
        }
        print("Distance " + Vector3.Distance(transform.position, headTransform.position));
    }

    private void OnGrabbed(XRBaseInteractor interactor)
    {
        // Si la m�scara est� en la cara, permitir soltarla nuevamente
        if (isAttached)
        {
            DetachFromFace();
        }

        // Restaurar la f�sica para poder mover la m�scara libremente
        isMovingToFace = false;
        maskRigidbody.isKinematic = false;
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        // Si la m�scara est� cerca de la cabeza, activar el movimiento hacia la cara
        if (Vector3.Distance(transform.position, headTransform.position) < proximityDistance)
        {
            print("Attach");
            isMovingToFace = true;
        }
        else
        {
            print("Detach1");
            DetachFromFace();
            
        }
    }

    private void AttachToFace()
    {
        isMovingToFace = false;
        isAttached = true;
        maskRigidbody.isKinematic = true; // Hacer la m�scara cinem�tica para que no caiga
        transform.position = desiredPosition.position;
        transform.rotation = desiredPosition.rotation;
        transform.parent = desiredPosition.parent; // Anclar a la cabeza
    }

    private void DetachFromFace()
    {
        print("Detach2");
        isAttached = false;
        transform.parent = null; // Desanclar la m�scara de la cabeza
        maskRigidbody.isKinematic = false; // Restaurar la f�sica
    }
}
