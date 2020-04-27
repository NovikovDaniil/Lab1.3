using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    [Serializable]
   public class Note
    {
        public string LastName;
        public string Name;
        public string Patronymic;
        public string Street;
        public ushort House;
        public ushort Flat;
        public string Phone;

        public override bool Equals(object obj)
        {
            Note person = (Note)obj;

            return (Name == person.Name)&&(LastName==person.LastName)&&(Patronymic==person.Patronymic)&&(Street==person.Street)&&(House==person.House)&&(Flat==person.Flat)&&(Phone==person.Phone);
        }

		public override int GetHashCode() {
			return LastName.Length^Name.Length^Patronymic.Length^Street.Length^House^Flat^Phone.Length;
		}
	}
}