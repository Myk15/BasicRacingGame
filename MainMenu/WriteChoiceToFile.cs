using System.IO;
using UnityEngine;

[SerializeField]
public class ReadAndWriteCarAttributes
{
    private static string path;
   
    public static void Write(float Shield, float MaxSpeed, float MaxAcceleration) //get all the players attributes and make a new car attributes object
    {

#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/Resources/CarChoice.json";

#elif UNITY_ANDROID
        path = "jar:file://" + Application.dataPath + "!/Resources/CarChoice.json";
#endif

#if UNITY_STANDALONE_WIN
        path = Application.streamingAssetsPath + "/Resources/CarChoice.json";
#endif


        CarAttributes Choice = new CarAttributes("Player", MaxAcceleration, Shield, MaxSpeed);

        StreamWriter writer = new StreamWriter(path, false);

       string json = JsonUtility.ToJson(Choice); //convert to a json object and write to file
        writer.Write(json);

        writer.Close();
    }

    public CarAttributes ConvertBackToObject(string path) // when we need the car att back
    {
        string data = File.ReadAllText(path);

        CarAttributes Car = JsonUtility.FromJson<CarAttributes>(data);

        return Car;
    }
}
[System.Serializable]
public class CarAttributes
    {
  
    public string Name;
    public float Acceleration;
    public float Shield;
    public float TopSpeed;

    public CarAttributes(string n, float A, float S, float T)
    {
        Name = n;
        Acceleration = A;
        Shield = S;
        TopSpeed = T;
    }
    }