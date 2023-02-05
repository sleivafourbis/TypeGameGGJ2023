using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class WordsService
{
    public static List<Word> words;

    public static void GetData()
    {
        var jsonString = File.ReadAllText (Application.dataPath + "/Data/Words.json");
        var data = JsonUtility.FromJson<Words>(jsonString);
        words = data.WordList;
    }

    public static string GetWord(int difficulty)
    {
        var filteredWords = words.Where(c => c.Difficulty == difficulty);
        var random = Random.Range(0, filteredWords.Count());

        return filteredWords.FirstOrDefault(c => c.Id == random).Text;
    }
}
