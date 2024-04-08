using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using static EnemyBase;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance;
    public List<EnemyBase> enemies = new List<EnemyBase>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine("Write");
    }

    IEnumerator Write()
    {
        yield return new WaitForSeconds(1f);
        foreach (EnemyBase enemy in enemies)
        {
            XmlWrite(enemy.myAttributes);
            TxtWrite(enemy.myAttributes);
            JSONWrite(enemy.myAttributes);
        }
    }
    public void TxtWrite(Attributes att)
    {
        string fileLocation = Application.persistentDataPath + "/_enemiesTxts/" + att.name + "Txt.txt";
        if (!File.Exists(fileLocation))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));
        }
        File.WriteAllText(fileLocation, att.ToString());
    }
    public void XmlWrite(Attributes att)
    {
        string fileLocation = Application.persistentDataPath + "/_enemiesXmls/";
        if (!File.Exists(fileLocation))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(fileLocation));
        }
        FileStream fs = File.Create(fileLocation += att.name + "Xml.xml");
        //XmlSerializer serializer = new XmlSerializer(typeof(Attributes));
        XmlWriter xmlWriter = XmlWriter.Create(fs);
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement(att.name);
        xmlWriter.WriteElementString("myAttributes",att.ToString());
        xmlWriter.WriteEndElement();
        xmlWriter.Close();
        fs.Close();

    }
    public void JSONWrite(Attributes att)
    {
        string fileLocation = Application.persistentDataPath + "/_enemiesJSONs/" + att.name + "Txt.txt";
        if (!File.Exists(fileLocation))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));
        }
        string attributesJSON = JsonUtility.ToJson(att);
        File.WriteAllText(fileLocation, attributesJSON);
    }


}
