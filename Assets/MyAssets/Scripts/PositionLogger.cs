using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;

public class PositionLogger : MonoBehaviour
{
    public float loggingInterval = 0.1f; // 로그를 저장할 간격(초)

    private StreamWriter logWriter;
    private float timer = 0f;

    public Rigidbody rb;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name; // 현재 Scene의 이름 가져오기
        string folderPath = Application.dataPath + "/PositionLog/" + sceneName + "/"; // 폴더 경로 생성
        Directory.CreateDirectory(folderPath); // 폴더가 없으면 생성

        string currentTimeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss"); // 현재 날짜와 시간을 형식화된 문자열로 얻음
        string filePath = folderPath + "position_" + currentTimeStamp + ".txt"; // 파일 경로 생성
        logWriter = new StreamWriter(filePath, false);
        logWriter.WriteLine("Time\tPosition");

        // 로그를 저장할 간격마다 InvokeRepeating을 사용하여 LogPosition 함수를 호출합니다.
        InvokeRepeating("LogPosition", 0f, loggingInterval);
    }

    void Update()
    {
        // 타이머를 업데이트합니다.
        timer += Time.deltaTime;
    }

    void LogPosition()
    {
        // 현재 시각을 얻어옵니다.
        System.DateTime currentTime = System.DateTime.Now;

        // 현재 시간과 위치를 로그에 기록합니다.
        string logEntry = $"{currentTime:yyyy-MM-dd HH:mm:ss.fff}\t{rb.position}";
        logWriter.WriteLine(logEntry);

        // 콘솔에도 로그를 출력합니다(선택 사항).
        Debug.Log(logEntry);
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 로그 파일을 닫습니다.
        if (logWriter != null)
        {
            logWriter.Close();
        }
    }

    string GetLogFilePath()
    {
        // 새로운 로그 파일 경로를 생성합니다.
        System.DateTime currentTime = System.DateTime.Now;
        string logFileName = $"PositionLog_{currentTime:yyyy-MM-dd HH:mm:ss.fff}.txt";
        return logFileName;
    }
}
