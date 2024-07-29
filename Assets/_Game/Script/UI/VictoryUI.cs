using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryUI : ResultUI
{  
    public void OnNext()
    {
        CloseDirectly();
        LevelManager.Instance.CloseLevel();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.CreateLevel(++LevelManager.Instance.level);
    }
}
