using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APITest : MonoBehaviour
{
    [SerializeField] private string username;
    [SerializeField] private int score;
    [TextArea(3,7)]

    public string dataRetrieved;
    public string[] allScores;
    public List<ScoreEntry> allScoreEntries = new List<ScoreEntry>();

    private void Start()
    {
        StartCoroutine(FetchData());
    }
    public void UploadScore()
    {
        StartCoroutine(SendScoreToDatabase(username, score));
    }

    IEnumerator FetchData()
    {
        UnityWebRequest myRequest = UnityWebRequest.Get("https://rafael-tanks-default-rtdb.firebaseio.com/scores/.json");
        yield return myRequest.SendWebRequest();
        
        Debug.Log(myRequest.downloadHandler.text);
        dataRetrieved = myRequest.downloadHandler.text;

        //CLEANING THE STRING HERE
        dataRetrieved = dataRetrieved.Replace("}", "");
        dataRetrieved = dataRetrieved.Replace("{", "");
        dataRetrieved = dataRetrieved.Replace('"', ' ');
        dataRetrieved = dataRetrieved.Replace(" ", "");

        //BREAK DOWN THE STRING INTO MANY OTHERS
        allScores = dataRetrieved.Split(",");


        foreach(string score in allScores)
        {
            string[] splitedString = score.Split(":");

            string username = splitedString[0];
            string newScore = splitedString[1];
            //string otherElement = score.Split(":")[2];

            ScoreEntry newEntry = new ScoreEntry(username, int.Parse(newScore));
            allScoreEntries.Add(newEntry);
        }

    }

    IEnumerator SendScoreToDatabase(string username, int score)
    {
        UnityWebRequest myRequest = UnityWebRequest.Put("https://rafael-tanks-default-rtdb.firebaseio.com/scores/" + username + "/.json", score.ToString());
        yield return myRequest.SendWebRequest();
        Debug.Log(myRequest.result.ToString());

    }

}


[System.Serializable]
public class ScoreEntry
{
    public string username;
    public int score;

    public ScoreEntry(string user, int newScore)
    {
        username = user;
        score = newScore;
    }
}


//[System.Serializable]
//public class ChuckNorrisJoke
//{
//    public string created_at;
//    public string url;
//    public string value;
//}
