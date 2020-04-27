using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class AddForm : Form
    {
        public Note MyRecord;
        public bool CheckAdd;
        private List<Note> PhoneNote;

        public AddForm(List<Note> _PhoneNote, Note _MyRecord, AddOrEdit myType)
        {
            InitializeComponent();
            PhoneNote = _PhoneNote;
            MyRecord = _MyRecord;
            CheckAdd = false;//флаг того, что в справочник добавится новый элемент
            if (myType == AddOrEdit.Add)
            {
                Text = "Добавление абонента";
                AddButton.Text = "Добавить";
            }
            else
            {
                Text = "Изменение абонента";
                AddButton.Text = "Изменить";
                LastNameTextBox.Text = MyRecord.LastName;
                NameTextBox.Text = MyRecord.Name;
                PatronymicTextBox.Text = MyRecord.Patronymic;
                PhoneMaskedTextBox.Text = MyRecord.Phone;
                StreetTextBox.Text = MyRecord.Street;
                HouseNumericUpDown.Value = MyRecord.House;
                FlatNumericUpDown.Value = MyRecord.Flat;
            }
          
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            // определяем поля записи 
            // берем значения из соответствующих компонентов на форме
            MyRecord.LastName = LastNameTextBox.Text;
            MyRecord.Name = NameTextBox.Text;
            MyRecord.Patronymic = PatronymicTextBox.Text;
            MyRecord.Phone = PhoneMaskedTextBox.Text;
            MyRecord.Street = StreetTextBox.Text;
            MyRecord.House = (ushort)HouseNumericUpDown.Value;
            MyRecord.Flat = (ushort)FlatNumericUpDown.Value;

            if (MyRecord.LastName == "" || MyRecord.Name == "" || MyRecord.Patronymic == "" || MyRecord.Street == "")
            {
                MessageBox.Show("Введены не все данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!(CheckForOutOfNumbers(LastNameTextBox.Text) && CheckForOutOfNumbers(NameTextBox.Text) &&
                CheckForOutOfNumbers(PatronymicTextBox.Text) && CheckForOutOfNumbers(StreetTextBox.Text)))
            {
                MessageBox.Show("Проверьте корректность ввода данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (PhoneNote.Contains(MyRecord))
            {
                MessageBox.Show("Данный человек уже присутствует в справочнике!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CheckAdd = true;
                Close();
            }
            LastNameTextBox.SelectAll();
            NameTextBox.SelectAll();
            PatronymicTextBox.SelectAll();
            PhoneMaskedTextBox.SelectAll();
            StreetTextBox.SelectAll();

        }
        private bool CheckForOutOfNumbers(string s)
        {
            foreach (char tmp in s)
            {
                if (!(tmp >= 'A' && tmp <= 'Z' || tmp >= 'А' && tmp <= 'Я' || tmp >= 'a' && tmp <= 'z' || tmp >= 'а' && tmp <= 'я' || tmp == ' ' || tmp == '-')) return false;
            }
            return true;
        }
    }

}
