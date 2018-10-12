using UnityEngine;
using System.IO;
using System.Xml;

public class Gae : MonoBehaviour
{
    private string fileName;
    private string path;
    private int numberNode;
    private string _name = "son";
    void Start()
    {
        fileName = "PlayerData";
        numberNode = 10;
    }
    private string getPath()
    {

        return Application.persistentDataPath + "/" + fileName;

    }
    public void Save()
    {
        //Đường dẫn lưu file xml, tí nữa mình sẽ nói rõ về phần đường dẫn file cho các nền tảng
        path = getPath() + ".xml";
        //Tạo file XmlDocument
        XmlDocument xmlDoc = new XmlDocument();
        //Tạo 1 element
        XmlElement elmRoot = xmlDoc.CreateElement("Player");
        
        //Thêm element vào document
        xmlDoc.AppendChild(elmRoot);
        for (int i = 0; i < numberNode; i++)
        {
            XmlElement elmChild = xmlDoc.CreateElement("ID");
            elmChild.InnerText = "" + i;
            
            
            //Thêm element vào document
            elmRoot.AppendChild(elmChild);
            XmlElement elmChild1 = xmlDoc.CreateElement("Name");

            elmChild1.InnerText = _name;
            elmChild.AppendChild(elmChild1);
        }
       

        //Sau khi thiết lập thông tin thì chúng ta lưu file và đóng file
        StreamWriter outStream = File.CreateText(path);
        xmlDoc.Save(outStream);
        outStream.Close();
        Debug.Log("Save game information successful");
    }
    public void Load()
    {
        
        //Đường dẫn file
        path = getPath() + ".xml";
        //Tải file lên
        XmlReader reader = XmlReader.Create(path);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        //Lấy NodeList của node cha
        XmlNodeList ObjectData = xmlDoc.GetElementsByTagName("Player");
        //duyệt danh sách của node cha -> "Object"
        for (int i = 0; i < ObjectData.Count; i++)
        {
            //Lấy từng item của node cha ->"Object 1,2,3..."
            XmlNode dataChild = ObjectData.Item(i);
            //lay danh sach cua node con trong node cha
            XmlNodeList allChildNode = dataChild.ChildNodes;
            //Duyệt danh sách của node con -> "ID"
            for (int j = 0; j < allChildNode.Count; j++)
            {
                XmlNode gameObject = allChildNode.Item(j);
                //Lấy dữ liệu trong node
                Debug.Log("data in child" + i + gameObject.InnerText);
            }
        }
        //Đóng file
        reader.Close();
        Debug.Log(Application.persistentDataPath);
    }
    public void Delete()
    {
        //Đường dẫn file
        path = getPath() + ".xml";
        //Tải file lên
        XmlReader reader = XmlReader.Create(path);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        //Lấy NodeList của node cha
        XmlNodeList ObjectData = xmlDoc.GetElementsByTagName("Player");
        for (int i = 0; i < ObjectData.Count; i++)
        {
            //Lấy từng item của node cha ->"Object 1,2,3..."
            XmlNode dataChild = ObjectData.Item(i);
            //lay danh sach cua node con trong node cha
            XmlNodeList allChildNode = dataChild.ChildNodes;
            xmlDoc.RemoveChild(xmlDoc.SelectSingleNode("/Player[@ID='1']"));
        }
    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Delete();
        }
    }
}