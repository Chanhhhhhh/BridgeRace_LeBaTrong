using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI textScore;
    public override void Setup()
    {
        base.Setup();
        SetScore(LevelManager.Instance.GetScore());
    }
    public void SetScore(int score)
    {
        textScore.text = "Score   " + score.ToString();
    }
    public void BackMainMenu()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.MainMenu);
    }

    public void OnRetry()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.CreateLevel(LevelManager.Instance.level);
    }
}
