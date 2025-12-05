using System;
using System.IO;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            string contents = File.ReadAllText(Application.streamingAssetsPath+"/test.txt");
            // print(Application.streamingAssetsPath+"/test.txt");
            print("Contents of Test.txt");
            print(contents);
            // Testing 2
        }
        catch (FileNotFoundException e)
        {
            print("Couldn't load file");
        }
        catch (Exception e)
        {
            print("Error Occured failed to load file");
        }
    }
}
