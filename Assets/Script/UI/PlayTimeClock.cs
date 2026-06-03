using UnityEngine;
using TMPro;

public class PlayTimeClock : MonoBehaviour
{
    [Header("시간을 표시할 TextMeshPro UI")]
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("시계 앞에 붙일 글자")]
    [SerializeField] private string prefix = "TIME ";

    // 게임이 시작된 후 흐른 시간
    private float playTime = 0f;

    private void Update()
    {
        // 매 프레임마다 지난 시간을 누적한다.
        // Time.deltaTime은 이전 프레임부터 현재 프레임까지 걸린 시간이다.
        playTime += Time.deltaTime;

        // 초 단위 시간을 분과 초로 변환한다.
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);

        // 00:00 형식으로 화면에 표시한다.
        timeText.text = $"{prefix}{minutes:00}:{seconds:00}";
    }

    public void ResetClock()
    {
        // 필요할 때 시간을 0으로 초기화할 수 있는 함수
        playTime = 0f;
    }
}