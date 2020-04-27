using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Save
{
    class SaveTxt : ISave
    {
        public void Save(List<Note> phoneNote, string fileName)
        {
            int i = 1;
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (Note MyRecord in phoneNote)
                {
                    sw.WriteLine("№" + i.ToString());
                    sw.WriteLine(MyRecord.LastName);
                    sw.WriteLine(MyRecord.Name);
                    sw.WriteLine(MyRecord.Patronymic);
                    sw.WriteLine(MyRecord.Street);
                    sw.WriteLine(MyRecord.House);
                    sw.WriteLine(MyRecord.Flat);
                    sw.WriteLine(MyRecord.Phone);
                    ++i;
                }
            }
        }
    }
}
