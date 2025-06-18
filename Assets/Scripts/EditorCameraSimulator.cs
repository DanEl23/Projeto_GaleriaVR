using UnityEngine;

// Este script simula o movimento da cabeça no editor do Unity
// quando a simulação nativa do Cardboard não funciona.
public class EditorCameraSimulator : MonoBehaviour
{
#if UNITY_EDITOR // O código abaixo só será incluído no editor do Unity

    public float sensibilidade = 2.0f; // Controla a velocidade da rotação
    private Vector2 rotacao;

    void Update()
    {
        // Verifica se a tecla Option (Alt no Windows) está a ser pressionada
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            // Obtém o movimento do rato e aplica-o à rotação
            rotacao.y += Input.GetAxis("Mouse X") * sensibilidade;
            rotacao.x -= Input.GetAxis("Mouse Y") * sensibilidade;

            // Limita a rotação vertical para não "virar de cabeça para baixo"
            rotacao.x = Mathf.Clamp(rotacao.x, -90f, 90f);

            // Aplica a rotação à câmara
            transform.localEulerAngles = new Vector3(rotacao.x, rotacao.y, 0);
        }
    }

#endif
}