using UnityEngine;

// Anexe este script a qualquer objeto que deva mostrar uma descrição quando olhado.
public class Interactable : MonoBehaviour
{
    [TextArea(3, 10)] // Deixa o campo de texto maior no Inspector do Unity
    public string descriptionText; // Texto que será exibido para este objeto.

    // Método chamado pelo GazeController para mostrar a informação
    public void ShowInfo()
    {
        // Pede para o controlador de UI (UIController) mostrar a informação deste objeto
        UIController.instance.SetDescriptionText(descriptionText);
    }

    // Método chamado para esconder a informação
    public void HideInfo()
    {
        // Pede para o controlador de UI esconder o texto de informação
        UIController.instance.ClearDescriptionText();
    }
}
