using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab1.Save
{
    interface ISave
    {
        void Save(List<Note> notes, string fileName);
    }

}
