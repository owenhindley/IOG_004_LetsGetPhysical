using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EventTracker : MonoBehaviour
{
    public List<GyroEvent> gyroData;
    public float startTime;
    public string dataPath;

    // Use this for initialization
    void Start()
    {
        dataPath = Application.persistentDataPath;
        gyroData = new List<GyroEvent>();
        //gyroData.Add(new GyroEvent{ timeStamp = Time.time, attitude = Input.gyro.attitude, rotationRate = Input.gyro.rotationRate, rotationRateUnbiased = Input.gyro.rotationRateUnbiased, gravity = Input.gyro.gravity, userAcceleration = Input.gyro.userAcceleration });
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (SystemInfo.supportsGyroscope && Input.gyro.enabled == false)
            {
                Input.gyro.enabled = true;
                startTime = Time.time;
            }
                
            gyroData.Add(new GyroEvent{ timeStamp = Time.time - startTime, attitude = Input.gyro.attitude, rotationRate = Input.gyro.rotationRate, rotationRateUnbiased = Input.gyro.rotationRateUnbiased, gravity = Input.gyro.gravity, userAcceleration = Input.gyro.userAcceleration });    
        }
        else
        {
            if (Input.gyro.enabled)
            {
                Save();
                Input.gyro.enabled = false;
                gyroData.Clear();
            }
        }
    }

    private void Save()
    {
        int index = 0;
        var path = Path.Combine(Application.persistentDataPath, "gyroData" + index + ".txt");
        while (File.Exists(path))
        {
            index++;
            path = Path.Combine(Application.persistentDataPath, "gyroData" + index + ".txt");
        }

        var writer = File.CreateText(path);
        writer.Write(MiniJSON.Json.Serialize(gyroData));
        writer.Close();    
    }
}

public struct GyroEvent
{
    public float timeStamp { get; set; }

    public Quaternion attitude { get; set; }

    public Vector3 rotationRate { get; set; }

    public Vector3 rotationRateUnbiased { get; set; }

    public Vector3 gravity { get; set; }

    public Vector3 userAcceleration { get; set; }

    public static GyroEvent Deserialize(object obj)
    {
        var gyroEvent = new GyroEvent();
        Dictionary<string, object> table = obj as Dictionary<string, object>;

        //gyroEvent.timeStamp = float.Parse((string)table["timeStamp"]);

        gyroEvent.attitude = parseQuaternion((string)table["attitude"]);
        gyroEvent.rotationRate = parseVector3((string)table["rotationRate"]);
        gyroEvent.rotationRateUnbiased = parseVector3((string)table["rotationRateUnbiased"]);
        gyroEvent.gravity = parseVector3((string)table["gravity"]);
        gyroEvent.userAcceleration = parseVector3((string)table["userAcceleration"]);

        return gyroEvent;
    }

    private static Quaternion parseQuaternion(string quatStr)
    {
        quatStr = quatStr.TrimStart('(');
        quatStr = quatStr.TrimEnd(')');
        var splt = quatStr.Split(',');
        return new Quaternion(float.Parse(splt[0]), float.Parse(splt[1]), float.Parse(splt[2]), float.Parse(splt[3]));
    }

    private static Vector3 parseVector3(string vectStr)
    {
        vectStr = vectStr.TrimStart('(');
        vectStr = vectStr.TrimEnd(')');
        var splt = vectStr.Split(',');
        return new Vector3(float.Parse(splt[0]), float.Parse(splt[1]), float.Parse(splt[2]));
    }

    private static Vector2 parseVector2(string vectStr)
    {
        vectStr = vectStr.TrimStart('(');
        vectStr = vectStr.TrimEnd(')');
        var splt = vectStr.Split(',');
        return new Vector2(float.Parse(splt[0]), float.Parse(splt[1]));
    }
}
