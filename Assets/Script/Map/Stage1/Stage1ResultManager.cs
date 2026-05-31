using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1ResultManager : MonoBehaviour
{
    private float playTime = 0f;
    private int killCount = 0;

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
    }

    public void AddKillCount()
    {
        killCount++;
    }

    public void GoVictory()
    {
        GameResultData.isClear = true;
        GameResultData.score = Gold.Instance.gold; // 실제 골드
        GameResultData.wave = killCount;           // 임시로 적 처치 수 표시
        GameResultData.playTime = playTime;

        SceneManager.LoadScene("ResultScene");
    }

    public void GoDefeat()
    {
        GameResultData.isClear = false;
        GameResultData.score = Gold.Instance.gold;
        GameResultData.wave = killCount;
        GameResultData.playTime = playTime;

        SceneManager.LoadScene("ResultScene");
    }
}
