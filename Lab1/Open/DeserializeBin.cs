using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Open
{
    class DeserializeBin : IOpen
    {
        IFormatter binFormatter;
        public List<Note> AddingData(List<Note> phoneNote, string fileName)
        {
            binFormatter = new BinaryFormatter();
            List<Note> oldNotes = phoneNote.ToList();//запоминаем старый справочник
            List<Note> notes;//список для новых людей в телефонном справочнике
            using(FileStream file=new FileStream(fileName,FileMode.OpenOrCreate))
            {
                notes = binFormatter.Deserialize(file) as List<Note>;
            }
            foreach (var note in notes)//просматриваем каждый элемент в новом списке
                if (!oldNotes.Contains(note)) oldNotes.Add(note);//если элемента нет в старом списке, то записываем его
            return oldNotes;//на выходе получаем список без повторяющихся элементов
        }

        public List<Note> CreateNewList( string fileName)
        {
            binFormatter = new BinaryFormatter();
            List<Note> notes;
            using (FileStream file=new FileStream(fileName,FileMode.OpenOrCreate))
            {
                notes = binFormatter.Deserialize(file) as List<Note>;
            }
            return notes;
        }
    }
}
