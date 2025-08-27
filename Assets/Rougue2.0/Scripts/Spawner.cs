using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Game Settings")]
    public int startingSequenceLength = 2;
    public int maxSequenceLength = 8;
    public float initialShowTime = 1.5f;
    public float timeDecreasePerLevel = 0.1f;
    public float minShowTime = 0.5f;
    
    [Header("UI References")]
    public ComboUI comboUI;
    public CountdownUI countdownUI;
    public ScoreUI scoreUI;
    public HoleCell[] cells;

    private float currentShowTime;
    private int currentLevel = 1;
    private int currentSequenceLength;

    private List<int> currentSequence = new List<int>();
    private int currentClickIndex = 0;
    private bool acceptingInput = false;
    private bool roundEnded = false;
    private bool roundSuccess = false;

    private int score = 0;
    private int comboCount = 0;

    void Start()
    {
        // Initialize cells
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].index = i;
        }
        
        // Initialize game settings
        currentShowTime = initialShowTime;
        currentSequenceLength = startingSequenceLength;
        
        UpdateScoreUI();
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (currentSequenceLength <= maxSequenceLength)
        {
            Debug.Log($"Level {currentLevel}: Sequence Length = {currentSequenceLength}");
            
            yield return RunOneRound();
            
            if (!roundSuccess)
            {
                Debug.Log("Game Over! Restarting...");
                yield return new WaitForSeconds(2f);
                RestartGame();
                yield break;
            }
            else
            {
                // Level completed successfully
                currentLevel++;
                currentSequenceLength++;
                comboCount++;
                score += currentSequenceLength * comboCount;
                
                // Decrease show time to increase difficulty
                currentShowTime = Mathf.Max(currentShowTime - timeDecreasePerLevel, minShowTime);
                
                Debug.Log($"Level Complete! Moving to Level {currentLevel}");
                comboUI.AddCombo(comboCount);
            }
            
            UpdateScoreUI();
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log($"Congratulations! You completed all levels! Final Score: {score}");
    }

    void RestartGame()
    {
        currentLevel = 1;
        currentSequenceLength = startingSequenceLength;
        currentShowTime = initialShowTime;
        score = 0;
        comboCount = 0;
        comboUI.ResetCombo();
        UpdateScoreUI();
        StartCoroutine(GameLoop());
    }

    IEnumerator RunOneRound()
    {
        // Initialize round
        yield return new WaitForSeconds(0.5f);
        ResetAllCells();
        
        currentSequence = GenerateSequence(currentSequenceLength);
        currentClickIndex = 0;
        roundEnded = false;
        roundSuccess = false;
        
        Debug.Log($"Sequence: {string.Join(", ", currentSequence)}");
        
        // Show sequence to player
        foreach (int cellIndex in currentSequence)
        {
            var cell = cells[cellIndex];
            cell.meteorGO.SetActive(true);
            yield return new WaitForSeconds(currentShowTime);
            cell.meteorGO.SetActive(false);
            yield return new WaitForSeconds(0.2f); // Brief pause between targets
        }
        
        // Prepare for player input
        yield return new WaitForSeconds(0.5f);
        
        // Set all cells as clickable targets (no fake targets)
        foreach (var cell in cells)
        {
            cell.SetAsTarget();
        }
        
        // Wait for player input
        acceptingInput = true;
        countdownUI.StartCountdown(5f + currentSequenceLength); // More time for longer sequences
        
        float timer = 0f;
        float timeLimit = 5f + currentSequenceLength;
        
        while (timer < timeLimit && !roundEnded && currentClickIndex < currentSequence.Count)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        if (currentClickIndex < currentSequence.Count && !roundEnded)
        {
            Debug.Log("Time's up!");
            EndRound(false);
        }
        
        acceptingInput = false;
        countdownUI.Hide();
        ResetAllCells();
    }

    public void HandleClick(HoleCell cell, bool isFake)
    {
        if (!acceptingInput || roundEnded) return;
        
        // Simple sequence matching - just check if this is the next expected cell in order
        int expectedIndex = currentSequence[currentClickIndex];
        
        if (cell.index == expectedIndex)
        {
            cell.ShowSuccess();
            currentClickIndex++;
            
            if (currentClickIndex >= currentSequence.Count)
            {
                Debug.Log("Sequence Complete!");
                EndRound(true);
            }
        }
        else
        {
            Debug.Log($"Wrong! Expected {expectedIndex}, clicked {cell.index}");
            cell.ShowErrorFeedback();
            EndRound(false);
        }
    }

    private void EndRound(bool success)
    {
        roundEnded = true;
        roundSuccess = success;
        acceptingInput = false;
        countdownUI.Hide();
        ResetAllCells();

        if (!success)
        {
            comboCount = 0;
            comboUI.ResetCombo();
        }
    }

    private List<int> GenerateSequence(int count)
    {
        var available = new List<int>();
        for (int i = 0; i < cells.Length; i++) available.Add(i);

        var result = new List<int>();
        for (int i = 0; i < count && available.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            result.Add(available[randomIndex]);
            available.RemoveAt(randomIndex);
        }
        return result;
    }

    private void ResetAllCells()
    {
        foreach (var cell in cells)
        {
            cell.ResetCell();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreUI != null) scoreUI.UpdateScore(score);
    }
}