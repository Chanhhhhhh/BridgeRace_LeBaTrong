using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState {MainMenu,Level, GamePlay, Pause, Win, Lose }
public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    public static UnityEvent<GameState> OnGameStateChange;

    private void Awake()
    {
        SaveManager.Instance.LoadData();
        ChangeState(GameState.MainMenu);
        Application.targetFrameRate= 60;
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.MainMenu:
                UIManager.Instance.OpenUI<MainMenu>();
                break;
            case GameState.Level:
                UIManager.Instance.OpenUI<LevelUI>();
                break;
            case GameState.GamePlay:
                UIManager.Instance.OpenUI<GamePlayUI>();
                break;
            case GameState.Pause:
                UIManager.Instance.OpenUI<PauseUI>();
                break;
            case GameState.Win:
                LevelManager.Instance.OnWin();
                UIManager.Instance.CloseUI<GamePlayUI>(1.5f);
                UIManager.Instance.OpenUI<VictoryUI>(2f);
                break;
            case GameState.Lose:
                UIManager.Instance.CloseUI<GamePlayUI>(1.5f);
                UIManager.Instance.OpenUI<DefeatUI>(2f);
                break;
            default:
                break;
        }

        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
}
