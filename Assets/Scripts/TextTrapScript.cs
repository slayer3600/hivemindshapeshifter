using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrapScript : MonoBehaviour {

    public Text TrapText;
    public Animator dialogSettings;
    public int numberOfCharacters = 4;
    
    private string randomText;
    private int positionToSolve = 0;
    private string[] randomTextArray;
    public PlayerController player;

    // Use this for initialization
    void Start () {

        randomTextArray = new string[numberOfCharacters];
        ResetPuzzle();

    }
	
	// Update is called once per frame
	void Update () {

        string keyPressed = Input.inputString.ToUpper();
        

        if (!String.IsNullOrEmpty(keyPressed))
        {
            string currentChar = randomText.Substring(positionToSolve, 1);

            if (keyPressed == currentChar)
            {
                //Character solved
                randomTextArray[positionToSolve] = "<color=#008000>" + currentChar  + "</color>";
                UpdateTextTrap(randomTextArray);

                positionToSolve++;
            }

            if ((positionToSolve) == numberOfCharacters)
            {
                CloseTextTrap();
                //If you run it too quickly the next puzzle is revealed before it slides off screen.
                Invoke("ResetPuzzle", 1.5F);
            }
        }


    }

    public void OpenTextTrap()
    {
        dialogSettings.SetBool("IsHidden", false);
    }

    public void CloseTextTrap()
    {
        dialogSettings.SetBool("IsHidden", true);
    }

    private string GenerateRandomString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var stringChars = new char[length];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }

    private void ResetPuzzle()
    {

        randomText = GenerateRandomString(numberOfCharacters);
        TrapText.text = randomText;
        positionToSolve = 0;

        for (int i = 0; i < randomText.Length; i++)
        {
            randomTextArray[i] = randomText[i].ToString();
        }

        if (player != null)
        {
            player.IsSolvingTextTrap = false;
        }     

    }

    private void UpdateTextTrap(string[] stringArray)
    {

        string result = string.Empty;

        for (int i = 0; i < stringArray.Length; i++)
        {
            result += randomTextArray[i].ToString();
        }

        TrapText.text = result;

    }
}
