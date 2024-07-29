using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : UICanvas
{
    [SerializeField] RectTransform ContentBtn;
    [SerializeField] LevelBtn BtnPrefab;
    public override void Setup()
    {
        base.Setup();
        foreach (Transform child in ContentBtn)
        {
            Destroy(child.gameObject);
        }
        int count = LevelManager.Instance.leves.Count;
        for(int i = 0; i < count; i++)
        {
            LevelBtn newBtn = Instantiate(BtnPrefab, ContentBtn);
            newBtn.OnInit(i);
        }
    }

    public void BackMainMenu()
    {
        CloseDirectly();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
