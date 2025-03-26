using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class XRMenuController : MonoBehaviour
{
    public GameObject menuCanvas; // El Canvas completo del men�
    public GameObject panelOpciones; // Panel de opciones
    public GameObject panelCreditos; // Panel de cr�ditos

    private bool isMenuVisible = false;

    void Update()
    {
        // Abrir/cerrar el men� con la tecla "M" (puedes cambiar la tecla)
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuCanvas.SetActive(isMenuVisible); // Activa o desactiva el Canvas completo
    }

    // M�todo para el bot�n "Iniciar"
    public void OnIniciarPressed()
    {
        Debug.Log("Iniciar juego...");
        SceneManager.LoadScene("MainVR"); // Cambia "NombreDeTuEscenaDeJuego" por el nombre de tu escena
    }

    // M�todo para el bot�n "Opciones"
    public void OnOpcionesPressed()
    {
        Debug.Log("Mostrando opciones...");
        panelOpciones.SetActive(true); // Muestra el panel de opciones
        panelCreditos.SetActive(false); // Oculta el panel de cr�ditos
    }

    // M�todo para el bot�n "Cr�ditos"
    public void OnCreditosPressed()
    {
        Debug.Log("Mostrando cr�ditos...");
        panelCreditos.SetActive(true); // Muestra el panel de cr�ditos
        panelOpciones.SetActive(false); // Oculta el panel de opciones
    }

    // M�todo para el bot�n "Salir"
    public void OnSalirPressed()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra la aplicaci�n (solo funciona en builds, no en el editor)
    }
}