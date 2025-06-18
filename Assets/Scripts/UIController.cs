using UnityEngine;
using TMPro; // Necessário para usar componentes de TextMeshPro

// Responsável por controlar os elementos da Interface do Usuário (UI).
public class UIController : MonoBehaviour
{
    // Padrão Singleton: permite que outros scripts acessem este controlador facilmente.
    public static UIController instance; 

    public TextMeshProUGUI descriptionTextElement; // Referência para o objeto de texto da descrição.

    void Awake()
    {
        // Configuração do Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Garante que o painel de texto comece desativado.
        if (descriptionTextElement != null)
        {
            descriptionTextElement.gameObject.SetActive(false);
        }
    }

    // Ativa o painel e define o texto da descrição.
    public void SetDescriptionText(string text)
    {
        if (descriptionTextElement != null)
        {
            descriptionTextElement.text = text;
            descriptionTextElement.gameObject.SetActive(true);
        }
    }

    // Desativa o painel de texto.
    public void ClearDescriptionText()
    {
        if (descriptionTextElement != null)
        {
            descriptionTextElement.text = "";
            descriptionTextElement.gameObject.SetActive(false);
        }
    }
}
