// PinPuzzle.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPuzzle : MonoBehaviour
{
    [SerializeField] private Pin[] pins;
    [SerializeField] private int[] CorrectCode = new int[4] {2, 1, 2, 4}; // Código correcto: 3-1-2-4
    [SerializeField] private GameObject EnergyCellLocked;
    [SerializeField] private GameObject EnergyCellUnlocked;

    void Start()
    {
         EnergyCellUnlocked.SetActive(false);
        EnergyCellLocked.SetActive(true);
    }
    // Verifica si el código ingresado es correcto
    private void CheckPuzzle()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i].GetCurrentPosition() != CorrectCode[i])
            {
                return; // Si algún pin no coincide, se sale de la función
            }
        }

        // Si todos los pines coinciden, se completa el puzzle
        UnlockPuzzle();
    }

    // Llama a este método al completar el puzzle
    private void UnlockPuzzle()
    {
        Debug.Log("Puzzle Completo! Energía desbloqueada.");
        EnergyCellUnlocked.SetActive(true);
        EnergyCellLocked.SetActive(false);
        // Aquí puedes activar el objeto desbloqueado, como el Energy Cell
    }

    // Cambia el valor de un pin específico
    public void ChangePin(int pinIndex)
    {
        if (pinIndex >= 0 && pinIndex < pins.Length)
        {
            pins[pinIndex].ChangeState(); // Cambia el estado del pin
            CheckPuzzle(); // Verifica si el puzzle está resuelto
        }
    }

    
}
