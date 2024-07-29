using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.tvOS;

public enum ColorType { Default, Red, Green, Blue, Pink, Yellow };

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Default, ColorType.Red, ColorType.Green, ColorType.Blue, ColorType.Pink, ColorType.Yellow };

    private Level currentLevel;
    private List<Bot> listBot = new List<Bot>();
    private int characterAmount => currentLevel.BotAmount + 1;

    [SerializeField] private Bot BotPref;
    [SerializeField] private NavMeshData navMeshData;
    [SerializeField] private Player player;

    public List<Level> leves = new List<Level>();
    public int level;
    public ColorData colordata;



    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        List<ColorType> colors = colorTypes; // color
        List<Vector3> startPoints = new List<Vector3>(); // position
        for (int i = 0; i < characterAmount; i++)
        {
            startPoints.Add(currentLevel.startPoint.position + Vector3.right * 3f * i);
        }
        // player
        int rand  = Random.Range(1,colors.Count);
        player.changColor(colors[rand]);
        colors.RemoveAt(rand);

        int randPos = Random.Range(0, characterAmount);
        player.TF.position = startPoints[randPos];
        startPoints.RemoveAt(randPos);
        player.gameObject.SetActive(true);
        player.OnInit();

        // bot
        for (int i = 0; i < currentLevel.BotAmount; i++)
        {
            Bot bot = Instantiate(BotPref, startPoints[i], Quaternion.identity);
            bot.changColor(colors[i + 1]);
            bot.OnInit();
            listBot.Add(bot);
        }

    }

    public void CreateLevel(int index)
    {
        //Debug.Log(index);
        if(index > leves.Count-1)
        {
            index = leves.Count-1;
        }       
        level = index;
        currentLevel = Instantiate(leves[index], Vector3.zero, Quaternion.identity);
        if(currentLevel != null)
        {
            OnInit();
        }
    }

    public Vector3 GetFinishPos()
    {
        if(currentLevel != null)
        {
            return currentLevel.finishPoint.position;
        }
        return Vector3.zero;
    }

    public void OnWin()
    {
        foreach(Bot bot in listBot)
        {
            bot.EndLevel();
        }
        if(level == SaveManager.Instance.UnlockLevel)
        {
            SaveManager.Instance.UnlockLevel = level + 1;
        }
        GameManager.ChangeState(GameState.Win);
    }

    public void OnLose(Bot bot)
    {
        foreach(Bot b in listBot)
        {
            if (b != bot)
            {               
                b.EndLevel();
            }
        }
        player.changAnim(Constants.ANIM_IDLE);
        GameManager.ChangeState(GameState.Lose);
    }

    internal void CloseLevel()
    {
        SimplePool.CollectAll();
        Destroy(currentLevel.gameObject);
        foreach(Bot bot in listBot)
        {
            Destroy(bot.gameObject);
        }
        listBot.Clear();
        player.gameObject.SetActive(false);
    }

    public int GetScore()
    {
        return player.BrickCounts;
    }
}
