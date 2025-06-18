using UnityEngine;
using UnityEngine.UI; // Necessário para controlar a cor do Retículo

// Controla a interação baseada no olhar (gaze) e o teleporte.
public class GazeController : MonoBehaviour
{
    [Header("Referências de Cena")]
    public Transform xrOrigin; // Referência ao objeto XR Origin (o jogador inteiro)
    public Image reticleImage; // A imagem do retículo (mira) na UI

    [Header("Configurações de Interação")]
    public float maxInteractionDistance = 50f; // Distância máxima do raio
    public Color teleportHighlightColor = Color.cyan; // Cor quando olha para um ponto de teleporte

    private Color originalReticleColor;
    private Interactable lastInteractable; // Guarda o último objeto interativo que olhamos

    void Start()
    {
        if (reticleImage != null)
        {
            originalReticleColor = reticleImage.color;
        }
    }

    void Update()
    {

        Debug.Log("GazeController está a ser executado!"); // <-- ADICIONE ESTA LINHA

        // Dispara um raio a partir da posição da câmera, para frente.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractionDistance))
        {
            HandleRaycastHit(hit);
        }
        else
        {
            // Se não estiver olhando para nada, limpa a última interação.
            ClearLastInteractable();
            ResetReticleColor();
        }

        // Checa por clique/toque na tela para iniciar o teleporte.
        CheckForTeleportClick(hit);
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        // Checa se o objeto atingido é um ponto de teleporte
        if (hit.collider.CompareTag("TeleportPoint"))
        {
            ClearLastInteractable();
            SetReticleColor(teleportHighlightColor);
        }
        // Checa se o objeto atingido é interativo
        else if (hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
        {
            if (interactable != lastInteractable)
            {
                ClearLastInteractable();
                lastInteractable = interactable;
                lastInteractable.ShowInfo();
            }
            ResetReticleColor();
        }
        else // Atingiu um objeto qualquer (parede, chão...)
        {
            ClearLastInteractable();
            ResetReticleColor();
        }
    }

    private void CheckForTeleportClick(RaycastHit hit)
    {
        // Input.GetMouseButtonDown(0) funciona para o toque na tela do Cardboard.
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            if (hit.collider.CompareTag("TeleportPoint"))
            {
                // Move o jogador para o ponto de teleporte
                Vector3 targetPosition = hit.point;
                // Mantém a altura original do jogador para não cair ou voar
                targetPosition.y = xrOrigin.position.y; 
                xrOrigin.position = targetPosition;
            }
        }
    }

    private void ClearLastInteractable()
    {
        if (lastInteractable != null)
        {
            lastInteractable.HideInfo();
            lastInteractable = null;
        }
    }

    private void SetReticleColor(Color color)
    {
        if (reticleImage != null) reticleImage.color = color;
    }

    private void ResetReticleColor()
    {
        if (reticleImage != null) reticleImage.color = originalReticleColor;
    }
}
