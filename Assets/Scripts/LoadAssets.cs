using System;
using System.IO;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    private char loadCheck = Char.ConvertFromUtf32(92).ToCharArray()[0];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            string contents = File.ReadAllText(Application.streamingAssetsPath+"/test.txt");
            // print(Application.streamingAssetsPath+"/test.txt");
            // print("Contents of Test.txt");
            // print(contents);
            print("Loading Fields");
            print("Loading Parts");
            print("Loading Robots");
            string[] mods = Directory.GetDirectories(Application.streamingAssetsPath+"/mods");
            for (int i=0; i<mods.Length; i++)
            {
                print(mods[i]);
                // print(robotPackages[i].Substring());
                string[] robots = Directory.GetFiles(mods[i]+"/robots");
                for (int r=0; r<robots.Length; r++)
                {
                    if (robots[r].Substring(robots[r].Length-5) != ".meta")
                    {
                        print("Loading: "+(mods[i].Substring(mods[i].LastIndexOf(loadCheck)+1))+"_"+robots[r].Substring(robots[r].LastIndexOf(loadCheck)+1).Substring(0,(robots[r].Length-robots[r].LastIndexOf(loadCheck)-1)-5));
                    }
                }
            }
            print("Generating Lang");
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
