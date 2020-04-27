using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Open
{
    interface IOpen
    {
        List<Note> CreateNewList(string fileName);
        List<Note> AddingData(List<Note> phoneNote, string fileName);
    }
}
