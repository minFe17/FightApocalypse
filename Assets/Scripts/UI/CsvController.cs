using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class CsvController : MonoBehaviour
{
    public List<stGunData> lstGun = new List<stGunData>();

    void ReadGunData()
    {
        string path = Application.dataPath + "/Assets/Resource/Datas/GunData.csv";
        if(File.Exists(path))
        {
            string source;
            using (StreamReader sr = new StreamReader(path))
            {
                string[] lines;
                source = sr.ReadToEnd();
                lines = Regex.Split(source, @"\r\n|\n\r|\n|\r");
                string[] header = Regex.Split(lines[0], ",");
                for (int i=1; i< lines.Length; i++)
                {
                    string[] values = Regex.Split(lines[i], ",");
                    if (values.Length == 0 || string.IsNullOrEmpty(values[0])) continue;

                    stGunData temp = new stGunData();
                    temp.INDEX = int.Parse(values[0]);
                    temp.Name = values[1];
                    temp.continuousFire = int.Parse(values[2]);
                    temp.Dmg = int.Parse(values[3]); 
                    temp.Magazine = int.Parse(values[4]);
                    temp.Price = int.Parse(values[5]); 
                }
            }
        }
    }
    void Start()
    {
        ReadGunData();
    }

    

    
    public struct stGunData
    {
        public int INDEX;
        public string Name;
        public int continuousFire;
        public int Dmg;
        public int Magazine;
        public int Price;
    }
}

