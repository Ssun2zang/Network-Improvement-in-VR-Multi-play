using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;

public class PositionLogger : MonoBehaviour
{
    public float loggingInterval = 0.1f; // �α׸� ������ ����(��)

    private StreamWriter logWriter;
    private float timer = 0f;

    public Rigidbody rb;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name; // ���� Scene�� �̸� ��������
        string folderPath = Application.dataPath + "/PositionLog/" + sceneName + "/"; // ���� ��� ����
        Directory.CreateDirectory(folderPath); // ������ ������ ����

        string currentTimeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss"); // ���� ��¥�� �ð��� ����ȭ�� ���ڿ��� ����
        string filePath = folderPath + "position_" + currentTimeStamp + ".txt"; // ���� ��� ����
        logWriter = new StreamWriter(filePath, false);
        logWriter.WriteLine("Time\tPosition");

        // �α׸� ������ ���ݸ��� InvokeRepeating�� ����Ͽ� LogPosition �Լ��� ȣ���մϴ�.
        InvokeRepeating("LogPosition", 0f, loggingInterval);
    }

    void Update()
    {
        // Ÿ�̸Ӹ� ������Ʈ�մϴ�.
        timer += Time.deltaTime;
    }

    void LogPosition()
    {
        // ���� �ð��� ���ɴϴ�.
        System.DateTime currentTime = System.DateTime.Now;

        // ���� �ð��� ��ġ�� �α׿� ����մϴ�.
        string logEntry = $"{currentTime:yyyy-MM-dd HH:mm:ss.fff}\t{rb.position}";
        logWriter.WriteLine(logEntry);

        // �ֿܼ��� �α׸� ����մϴ�(���� ����).
        Debug.Log(logEntry);
    }

    void OnDestroy()
    {
        // ��ũ��Ʈ�� �ı��� �� �α� ������ �ݽ��ϴ�.
        if (logWriter != null)
        {
            logWriter.Close();
        }
    }

    string GetLogFilePath()
    {
        // ���ο� �α� ���� ��θ� �����մϴ�.
        System.DateTime currentTime = System.DateTime.Now;
        string logFileName = $"PositionLog_{currentTime:yyyy-MM-dd HH:mm:ss.fff}.txt";
        return logFileName;
    }
}
