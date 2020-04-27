using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Open
{
    interface IOpen
    {
        void CreateNewList(List<Note> phneNote, string fileName);
       void AddingData(List<Note> phoneNote, string fileName);
    }
}
