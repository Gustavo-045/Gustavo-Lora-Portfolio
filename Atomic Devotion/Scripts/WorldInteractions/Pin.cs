using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pin : InteractableObject
{
    [SerializeField] private int initialPosition; // Posición inicial del pin (1 a 4)
    [SerializeField] private int currentPosition; // Posición actual del pin

    [SerializeField] private PinPuzzle pinPuzzle;
    [SerializeField] private TMP_Text DebugText; // Texto para mostrar el valor actual del pin
    [SerializeField] private int pinIndex; // Índice del pin para referenciar en el PinPuzzle
    [SerializeField] private Camera puzzleCamera; // Cámara del puzzle

    private void Start()
    {
        pinPuzzle = GetComponentInParent<PinPuzzle>();
        currentPosition = initialPosition; // Establece la posición inicial del pin
        UpdateDebugText();
    }

    // Se ejecuta cuando se hace clic sobre el objeto con un collider
    private void OnMouseDown()
    {
        // Verifica si la cámara del puzzle está activa
        if (puzzleCamera != null && puzzleCamera.gameObject.activeSelf)
        {
            Interact(); // Si la cámara del puzzle está activa, interactúa con el pin
        }
    }

    public override void Interact()
    {
        pinPuzzle.ChangePin(pinIndex); // Notifica al puzzle principal
    }

    // Cambia el estado del pin, de 1 a 4
    public void ChangeState()
    {
        currentPosition = currentPosition % 4 + 1; // Cicla entre 1 y 4
        UpdateDebugText();
    }

    // Retorna la posición actual del pin
    public int GetCurrentPosition()
    {
        return currentPosition;
    }

    // Actualiza el texto de depuración para mostrar el estado actual del pin
    private void UpdateDebugText()
    {
        if (DebugText != null)
        {
            DebugText.text = $"Pin {pinIndex + 1}: {currentPosition}";
        }
    }
}
