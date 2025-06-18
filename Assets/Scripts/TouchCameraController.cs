using UnityEngine;

// Este script permite controlar a câmara com o deslizar do dedo no ecrã (e com o rato no editor).
public class TouchCameraController : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerBody; // Arraste o objeto XRRig para aqui

    [Header("Configurações")]
    public float sensibilidadeRato = 100f;
    public float sensibilidadeToque = 0.2f;

    private float rotacaoX = 0f;

    void Update()
    {
        // --- Controlo para o Editor (Rato) ---
        #if UNITY_EDITOR
            float ratoX = Input.GetAxis("Mouse X") * sensibilidadeRato * Time.deltaTime;
            float ratoY = Input.GetAxis("Mouse Y") * sensibilidadeRato * Time.deltaTime;

            // Rotação Vertical (olhar para cima/baixo)
            rotacaoX -= ratoY;
            rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);
            transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);

            // Rotação Horizontal (virar o corpo)
            playerBody.Rotate(Vector3.up * ratoX);
        #endif

        // --- Controlo para o Telemóvel (Toque) ---
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Moved)
            {
                // Obtém o movimento do toque
                float toqueDeltaX = toque.deltaPosition.x * sensibilidadeToque * Time.deltaTime;
                float toqueDeltaY = toque.deltaPosition.y * sensibilidadeToque * Time.deltaTime;

                // Rotação Vertical
                rotacaoX -= toqueDeltaY;
                rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);
                transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);
                
                // Rotação Horizontal
                playerBody.Rotate(Vector3.up * toqueDeltaX);
            }
        }
    }
}