using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Open
{
    class OpenTxt : IOpen
    {
        public void CreateNewList(List<Note> phoneNote,string fileName)
        {
            Note myRecord;
            string tmp;
            using (StreamReader sr = new StreamReader(fileName))
            {

                while (!sr.EndOfStream)
                {
                    myRecord = new Note();
                    tmp = sr.ReadLine();//считываем строку с номером
                    myRecord.LastName = sr.ReadLine();
                    myRecord.Name = sr.ReadLine();
                    myRecord.Patronymic = sr.ReadLine();
                    myRecord.Street = sr.ReadLine();
                    myRecord.House = ushort.Parse(sr.ReadLine());
                    myRecord.Flat = ushort.Parse(sr.ReadLine());
                    myRecord.Phone = sr.ReadLine();
                    //добавляем запись в список
                    phoneNote.Add(myRecord);
                }
            }
        }

        public void AddingData(List<Note> phoneNote, string fileName)
        {
            Note myRecord;
            string tmp;
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    myRecord = new Note();
                    tmp = sr.ReadLine();//считываем строку с номером
                    myRecord.LastName = sr.ReadLine();
                    myRecord.Name = sr.ReadLine();
                    myRecord.Patronymic = sr.ReadLine();
                    myRecord.Street = sr.ReadLine();
                    myRecord.House = ushort.Parse(sr.ReadLine());
                    myRecord.Flat = ushort.Parse(sr.ReadLine());
                    myRecord.Phone = sr.ReadLine();
                    //добавляем запись в список
                    if (!phoneNote.Contains(myRecord)) phoneNote.Add(myRecord);
                }
            }
        }
    }
}
