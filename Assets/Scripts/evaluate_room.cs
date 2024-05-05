using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Unity.Barracuda;

[System.Serializable]
public class FurnitureCount
{
    public string furnitureType;
    public int count;

    public FurnitureCount(string furnitureType, int count)
    {
        this.furnitureType = furnitureType;
        this.count = count;
    }
}

[System.Serializable]
public class RoomData
{
    public float minDistance;
    public float averageDistance;
    public int totalObjects;
    public float totalColorRange;
    public List<FurnitureCount> furnitureCounts;

    public RoomData(float minDistance, float averageDistance, int totalObjects, float totalColorRange, Dictionary<string, int> typeCounts)
    {
        this.minDistance = minDistance;
        this.averageDistance = averageDistance;
        this.totalObjects = totalObjects;
        this.totalColorRange = totalColorRange;
        this.furnitureCounts = typeCounts.Select(kvp => new FurnitureCount(kvp.Key, kvp.Value)).ToList();
    }
}

public class evaluate_room : MonoBehaviour
{
    // Whether to include inactive GameObjects
    //public string filename = "RoomData.json"; // File name can be set in the Unity Editor
    public bool includeInactive = false;
    public bool getOutput = true;
    //public bool getEval = false;
    public float score;
    public NNModel modelAsset;
    private Model runtimeModel;
    private IWorker worker;
    private float[] inputs = new float[7];
    void Awake()
    {
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);
        //Debug.Log("Current Score: " + DataHolder.Char_sel);
        if (DataHolder.Char_sel == 0)
        {
            var alan = GameObject.Find("alannormalpose");
            alan.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);

        }
        else
        {
            var shawn = GameObject.Find("shawnhappy");
            shawn.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);
        }
        var anima = GameObject.Find("Feedback").GetComponent<Animator>();
        anima.enabled = false;
    }

    void Update()
    {

        //SaveDataToFile(roomData, filename);
    }

    public float getEval(bool change_pose)
    {
        GameObject[] objectsWithTag = FindAndCombineObjects();
        RoomData roomData = AnalyzeObjects(objectsWithTag);
        roomData.minDistance = CheckProximity();

        if (true)
        {
            getOutput = false;
            if (roomData.minDistance < 0.95f)
                inputs[0] = 1.0f;
            else
                inputs[0] = 0.0f;
            inputs[1] = roomData.averageDistance;
            inputs[2] = roomData.totalObjects;
            inputs[3] = roomData.totalColorRange;
            inputs[4] = 0f;
            inputs[5] = 0f;
            inputs[6] = 0f;

            for (int i = 0; i < roomData.furnitureCounts.Count; i++)
            {
                FurnitureCount item = roomData.furnitureCounts[i];
                if (item.furnitureType == "sitting")
                    inputs[4] = item.count;
                else if (item.furnitureType == "utility")
                    inputs[5] = item.count;
                else if (item.furnitureType == "entertainment")
                    inputs[6] = item.count;

            }


            Tensor inputTensor = new Tensor(1, 7, new float[] { inputs[0], inputs[1], inputs[2], inputs[3], inputs[4], inputs[5], inputs[6] });

            // Execute the model with the input tensor
            worker.Execute(inputTensor);

            Tensor outputTensor = worker.PeekOutput();
            //bool output = outputTensor[0] > 0.5f; // Assuming output is a single float that represents a probability
            score = outputTensor[0] * 100;

            if (change_pose)
            {
                if (score > 50)
                {
                    if (DataHolder.Char_sel == 0)
                    {
                        var alan = GameObject.Find("alannormalpose");
                        alan.transform.position = new Vector3(-0, 0, 0);
                        var alanhappy = GameObject.Find("alanhappy");
                        alanhappy.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);
                        var target = GameObject.Find("Main Camera").transform;
                        Vector3 directionToTarget = target.position - alanhappy.transform.position;
                        directionToTarget.y = 0;  // This removes any vertical difference in the look direction

                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        alanhappy.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                    }
                    else
                    {
                        var shawn = GameObject.Find("shawnhappy");
                        shawn.transform.position = new Vector3(-0, 0, 0);
                        var happy = GameObject.Find("happy");
                        happy.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);
                        var target = GameObject.Find("Main Camera").transform;
                        Vector3 directionToTarget = target.position - happy.transform.position;
                        directionToTarget.y = 0;  // This removes any vertical difference in the look direction

                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        happy.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                    }
                }
                else
                {
                    if (DataHolder.Char_sel == 0)
                    {
                        var alan = GameObject.Find("alannormalpose");
                        alan.transform.position = new Vector3(-0, 0, 0);
                        var alanhappy = GameObject.Find("alansad");
                        alanhappy.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);
                        var target = GameObject.Find("Main Camera").transform;
                        Vector3 directionToTarget = target.position - alanhappy.transform.position;
                        directionToTarget.y = 0;  // This removes any vertical difference in the look direction

                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        alanhappy.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                    }
                    else
                    {
                        var shawn = GameObject.Find("shawnhappy");
                        shawn.transform.position = new Vector3(-0, 0, 0);
                        var happy = GameObject.Find("sad");
                        happy.transform.position = new Vector3(-2.77399993f, 2.08f, 5.94199991f);
                        var target = GameObject.Find("Main Camera").transform;
                        Vector3 directionToTarget = target.position - happy.transform.position;
                        directionToTarget.y = 0;  // This removes any vertical difference in the look direction

                        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                        happy.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                    }
                }
            }

            //Debug.Log("Model output: " + score);


            // Dispose of tensors to free memory
            inputTensor.Dispose();
            outputTensor.Dispose();
        }
        return score;
    }
    GameObject[] FindAndCombineObjects()
    {
        GameObject[] sittingObjects = GameObject.FindGameObjectsWithTag("furniture_sitting");
        GameObject[] utilityObjects = GameObject.FindGameObjectsWithTag("furniture_utility");
        GameObject[] entertainmentObjects = GameObject.FindGameObjectsWithTag("furniture_entertainment");

        List<GameObject> allFurnitureObjects = new List<GameObject>();
        allFurnitureObjects.AddRange(sittingObjects);
        allFurnitureObjects.AddRange(utilityObjects);
        allFurnitureObjects.AddRange(entertainmentObjects);

        return allFurnitureObjects.ToArray();
    }

    RoomData AnalyzeObjects(GameObject[] objects)
    {
        float totalDistance = 0f;
        Vector3 centroid = Vector3.zero;
        float totalColorRange = 0f;
        List<Color> colors = new List<Color>();
        int totalObjects = objects.Length;
        Dictionary<string, int> typeCounts = new Dictionary<string, int>();

        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy && !includeInactive)
                continue;

            Color color = obj.GetComponent<Renderer>().material.color;
            colors.Add(color);

            Vector3 position = obj.transform.position;
            centroid += position;

            string tag = obj.tag;
            string furnitureType = tag.Substring("furniture_".Length);
            if (!typeCounts.ContainsKey(furnitureType))
                typeCounts[furnitureType] = 1;
            else
                typeCounts[furnitureType]++;
        }

        centroid /= totalObjects;
        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy && !includeInactive)
                continue;

            Vector3 position = obj.transform.position;
            totalDistance += Vector3.Distance(position, centroid);
        }

        float averageDistance = totalDistance / totalObjects;
        Color averageColor = CalculateAverageColor(colors);
        float sumColorRange = colors.Sum(color => ColorDistance(color, averageColor));
        totalColorRange = sumColorRange / totalObjects;

        return new RoomData(0, averageDistance, totalObjects, totalColorRange, typeCounts);
    }

    float CheckProximity()
    {
        List<GameObject> tvs = FindObjectsEndingWith("_TV");
        if (tvs.Count == 0)
        {
            //Debug.LogWarning("No TV found in the scene.");
            return 0;
        }
        GameObject tv = tvs[0];

        List<GameObject> counters = FindObjectsEndingWith("_Counter");
        List<GameObject> tables = FindObjectsEndingWith("_Table");

        if (counters.Count == 0 && tables.Count == 0)
        {
            //Debug.LogWarning("No counters or tables found in the scene.");
            return 0;
        }

        List<GameObject> furniture = counters.Concat(tables).ToList();
        float minDistance = furniture.Min(item => Vector3.Distance(tv.transform.position, item.transform.position));

        return minDistance;
    }

    //void SaveDataToFile(RoomData data, string filename)
    //{
    //    string path = Path.Combine(Application.persistentDataPath, filename);
    //    string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(path, json);
    //    Debug.Log("Data saved to " + path);
    //}

    Color CalculateAverageColor(List<Color> colors)
    {
        Color avgColor = Color.black;
        foreach (Color color in colors)
            avgColor += color;

        avgColor /= colors.Count;
        return avgColor;
    }

    float ColorDistance(Color color1, Color color2)
    {
        Vector3 color1Vec = new Vector3(color1.r, color1.g, color1.b);
        Vector3 color2Vec = new Vector3(color2.r, color2.g, color2.b);
        return Vector3.Distance(color1Vec, color2Vec);
    }

    List<GameObject> FindObjectsEndingWith(string suffix)
    {
        return GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name.EndsWith(suffix)).ToList();
    }
}
