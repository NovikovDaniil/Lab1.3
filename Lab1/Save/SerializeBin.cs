using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Save
{
    class SerializeBin : ISave
    {
        IFormatter binFormatter;
        public void Save(List<Note> notes, string fileName)
        {
            binFormatter = new BinaryFormatter();
            using (FileStream file=new FileStream(fileName, FileMode.Create, FileAccess.Write,FileShare.None))
            {
                binFormatter.Serialize(file, notes);
            }
        }
    }
}
