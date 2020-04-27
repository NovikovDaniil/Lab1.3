using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab1.Open
{
    class OpenXml:IOpen
    {
        public void CreateNewList(List<Note> phoneNote, string fileName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Note node = new Note();
                foreach (XmlNode cnode in xnode.ChildNodes)
                {
                    if (cnode.Name == "Lastname") node.LastName = cnode.InnerText;
                    if (cnode.Name == "Name") node.Name = cnode.InnerText;
                    if (cnode.Name == "Patronymic") node.Patronymic = cnode.InnerText;
                    if (cnode.Name == "Street") node.Street = cnode.InnerText;
                    if (cnode.Name == "House") node.House = ushort.Parse(cnode.InnerText);
                    if (cnode.Name == "Flat") node.Flat = ushort.Parse(cnode.InnerText);
                    if (cnode.Name == "Phone") node.Phone = cnode.InnerText;
                }
                 phoneNote.Add(node);
            }
        }

        public void AddingData(List<Note> phoneNote, string fileName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Note node = new Note();
                foreach (XmlNode cnode in xnode.ChildNodes)
                {
                    if (cnode.Name == "Lastname") node.LastName = cnode.InnerText;
                    if (cnode.Name == "Name") node.Name = cnode.InnerText;
                    if (cnode.Name == "Patronymic") node.Patronymic = cnode.InnerText;
                    if (cnode.Name == "Street") node.Street = cnode.InnerText;
                    if (cnode.Name == "House") node.House = ushort.Parse(cnode.InnerText);
                    if (cnode.Name == "Flat") node.Flat = ushort.Parse(cnode.InnerText);
                    if (cnode.Name == "Phone") node.Phone = cnode.InnerText;
                }
                if (!phoneNote.Contains(node)) phoneNote.Add(node);
            }
        }
    }
}
