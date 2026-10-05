using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceGame : MonoBehaviour
{
    public Button[] buttons;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;

    private int potentiometerValue = 0;

    private bool showingSequence = false;
    private bool playerTurn = false;

    private Coroutine timerCoroutine;

    void Start()
    {
        StartNewRound();
    }

    public void SetPotentiometer(int value)
    {
        potentiometerValue = value;
    }

    void StartNewRound()
    {
        sequence.Add(Random.Range(0, 4));

        playerIndex = 0;

        Debug.Log("New sequence:");

        foreach (int value in sequence)
        {
            Debug.Log("Button: " + (value + 1));
        }

        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        showingSequence = true;
        playerTurn = false;

        float tempo = Mathf.Lerp(
            1.0f,
            0.2f,
            potentiometerValue / 1023f
        );

        yield return new WaitForSeconds(0.5f);

        foreach (int value in sequence)
        {
            Debug.Log("Playing button: " + (value + 1));

            if (buttons != null && value < buttons.Length)
            {
                buttons[value].interactable = false;
            }

            yield return new WaitForSeconds(tempo);

            if (buttons != null && value < buttons.Length)
            {
                buttons[value].interactable = true;
            }

            yield return new WaitForSeconds(0.2f);
        }

        showingSequence = false;
        playerTurn = true;

        Debug.Log("Your turn. Repeat the sequence.");

        float responseTime = Mathf.Lerp(
            8.0f,
            3.0f,
            potentiometerValue / 1023f
        );

        Debug.Log("You have " + responseTime.ToString("F1") + " seconds.");

        timerCoroutine = StartCoroutine(ResponseTimer(responseTime));
    }

    IEnumerator ResponseTimer(float time)
    {
        yield return new WaitForSeconds(time);

        if (playerTurn)
        {
            Debug.Log("Time's up!");
            Debug.Log("You lost!");

            playerTurn = false;

            sequence.Clear();

            yield return new WaitForSeconds(1.0f);

            StartNewRound();
        }
    }

    public void PressButton(int buttonIndex)
    {
        if (!playerTurn)
        {
            return;
        }

        Debug.Log("Player pressed: " + (buttonIndex + 1));

        if (buttonIndex == sequence[playerIndex])
        {
            playerIndex++;

            if (playerIndex == sequence.Count)
            {
                Debug.Log("Correct sequence!");

                playerTurn = false;

               
                if (timerCoroutine != null)
                {
                    StopCoroutine(timerCoroutine);
                }

                StartNewRound();
            }
        }
        else
        {
            Debug.Log("Incorrect sequence");
            Debug.Log("You lost!");

            playerTurn = false;

            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }

            sequence.Clear();

            StartNewRound();
        }
    }
}