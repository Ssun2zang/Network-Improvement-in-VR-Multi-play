using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class FPSStatistics : MonoBehaviour
{
    private float fps;
    private float frameTime;
    private string filePath;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name; // 현재 Scene의 이름 가져오기
        string folderPath = Application.dataPath + "/FPSData/" + sceneName + "/"; // 폴더 경로 생성
        Directory.CreateDirectory(folderPath); // 폴더가 없으면 생성

        string currentTimeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss"); // 현재 날짜와 시간을 형식화된 문자열로 얻음
        filePath = folderPath + "performance_" + currentTimeStamp + ".txt"; // 파일 경로 생성
        File.WriteAllText(filePath, "Frame, FPS, Frame Time\n");
    }

    void Update()
    {
        fps = 1.0f / Time.deltaTime;
        frameTime = Time.deltaTime;

        // 이 부분에서 추가적인 계산을 할 수 있습니다.

        // 파일에 데이터 기록
        string data = Time.frameCount + ", " + fps + ", " + frameTime + "\n";
        File.AppendAllText(filePath, data);
    }
}
