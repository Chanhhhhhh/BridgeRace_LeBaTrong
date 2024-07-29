using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    UnityAction action;
    [SerializeField] TextMeshProUGUI txt_level;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        action.Invoke();
    }

    public void OnInit(int level)
    {
        this.level = level;
        txt_level.text = "Level " + (level + 1).ToString();
        action += () =>
        {
            GameManager.ChangeState(GameState.GamePlay);
            UIManager.Instance.CloseUI<LevelUI>();
            LevelManager.Instance.CreateLevel(level);
        };
        if (level <= SaveManager.Instance.UnlockLevel)
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;

        }
    }
}
