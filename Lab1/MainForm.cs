using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using Lab1.Save;
using Lab1.Open;

namespace Lab1
{
    public partial class MainForm : Form
    {
        private List<Note> PhoneNote;
        private List<Note> oldPhoneNote;
        private int current;
        private int count;
        ISave saveBehavior;
        IOpen openBehavior;
        public MainForm()
        {
            InitializeComponent();
            PhoneNote = new List<Note>();
            oldPhoneNote = new List<Note>();
            current = -1;//list is empty
            count = 0;
        }
        private void PrintElement()
        {
            if ((current >= 0) && (current < PhoneNote.Count))
            {    // если есть что выводить
                 // MyRecord - запись списка PhoneNote номер current
                Note MyRecord = PhoneNote[current];
                // записываем в соответствующие элементы на форме 
                // поля из записи MyRecord 
                LastNameTextBox.Text = MyRecord.LastName;
                NameTextBox.Text = MyRecord.Name;
                PatronymicTextBox.Text = MyRecord.Patronymic;
                PhoneMaskedTextBox.Text = MyRecord.Phone;
                StreetTextBox.Text = MyRecord.Street;
                HouseNumericUpDown.Value = MyRecord.House;
                FlatNumericUpDown.Value = MyRecord.Flat;
            }
            else // если current равно -1, т. е. список пуст
            {   // очистить поля формы 
                LastNameTextBox.Text = "";
                NameTextBox.Text = "";
                PatronymicTextBox.Text = "";
                PhoneMaskedTextBox.Text = "";
                StreetTextBox.Text = "";
                HouseNumericUpDown.Value = 1;
                FlatNumericUpDown.Value = 1;
            }
            // обновление строки состояния
            NumberToolStripStatusLabel.Text = (PhoneNote.Count > 0) ? (current + 1).ToString() : current.ToString();
            QuanityToolStripStatusLabel.Text = PhoneNote.Count.ToString();
        }
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаем запись - экземпляр класса Note
            Note MyRecord = new Note();
            // создаем экземпляр формы AddForm
            AddForm _AddForm = new AddForm(PhoneNote, MyRecord, AddOrEdit.Add);

            // открываем форму для добавления записи
            _AddForm.ShowDialog();

            // текущей записью становится последняя
            current = PhoneNote.Count;

            // добавляем к списку PhoneNote новый элемент - запись MyRecord,
            // взятую из формы AddForm
            if (_AddForm.CheckAdd == true)
            {
                PhoneNote.Add(_AddForm.MyRecord);
                // выводим текущий элемент
                PrintElement();
            }
            else
            {
                --current;
                PrintElement();
            }
        }
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                Note myRecord = new Note();
                myRecord.LastName = LastNameTextBox.Text;
                myRecord.Name = NameTextBox.Text;
                myRecord.Patronymic = PatronymicTextBox.Text;
                myRecord.Phone = PhoneMaskedTextBox.Text;
                myRecord.Street = StreetTextBox.Text;
                myRecord.House = (ushort)HouseNumericUpDown.Value;
                myRecord.Flat = (ushort)FlatNumericUpDown.Value;
                AddForm _AddForm = new AddForm(PhoneNote, myRecord, AddOrEdit.Edit);
                _AddForm.ShowDialog();
                // изменяем текущую запись
                PhoneNote[current] = _AddForm.MyRecord;
            }
            if(current>=0)PrintElement();
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.RemoveAt(current);
                if (current > 0 && current == PhoneNote.Count)
                    --current;
                PrintElement();
                if (PhoneNote.Count == 0)
                    MessageBox.Show("Телефонный справочник пуст", "Небольшая информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Телефонный справочник пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (current > 0)
            {
                current--;
                PrintElement();
            }
            else if (PhoneNote.Count != 0) MessageBox.Show("Предыдущей записи не существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else MessageBox.Show("Телефонный справочник пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (current < PhoneNote.Count - 1)
            {
                current++;
                PrintElement();
            }
            else if (PhoneNote.Count != 0) MessageBox.Show("Следующей записи не существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else MessageBox.Show("Телефонный справочник пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void вТекстовыйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (PhoneNote.Count > 0)
            {
                SaveFileDialog.Filter = "Текст|*.txt";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        saveBehavior = new SaveTxt();
                        saveBehavior.Save(PhoneNote, SaveFileDialog.FileName);
                        MessageBox.Show("Запись успешно выполнена!");
                        SaveFileDialog.FileName = "";
                        oldPhoneNote = PhoneNote.ToList();//так как выполнили сохранение, то перекидываем все элементы
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось сохранить данные! Ошибка: " + ex.Message);

                    }
                }
            }
            else MessageBox.Show("Телефонный справочник пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void изТекстовогоФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count != 0)//если есть записи
            {
                DialogResult dialogResult = MessageBox.Show("Данные могут быть утеряны.\n  Хотите сохранить телефонный справочник в файл?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)//если захотели сохранить, то сохраняем в текстовый файл
                {
                    вТекстовыйФайлToolStripMenuItem_Click(sender, e);
                }
                PhoneNote.Clear();//очищаем от записей телефонный справочник
            }
            OpenFileDialog.Filter = "Текст|*.txt";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openBehavior = new OpenTxt();
                    openBehavior.CreateNewList(PhoneNote, OpenFileDialog.FileName);
                    if (PhoneNote.Count == 0) current = -1;
                    else
                    {
                        current = 0;
                        if (count == 0)
                        {
                            oldPhoneNote = PhoneNote.ToList();
                            ++count;
                        }//запоминаем только первый раз, чтобы на выходе спросить про сохранение
                    }
                    // выводим текущий элемент
                    PrintElement();
                    OpenFileDialog.FileName = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("При открытии файла возникла ошибка! Ошибка: " + ex.Message);
                }
            }
        }

        private void вXmlФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (PhoneNote.Count > 0)
            {
                SaveFileDialog.Filter = "Текст|*.xml";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    saveBehavior = new SaveXml();
                    saveBehavior.Save(PhoneNote, SaveFileDialog.FileName);
                    oldPhoneNote = PhoneNote.ToList();//так как выполнили сохранение, то перекидываем все элементы
                    SaveFileDialog.FileName = "";
                }
            }
            else MessageBox.Show("Телефонный справочник пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void изXmlФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PhoneNote.Count != 0)//если есть записи
            {
                DialogResult dialogResult = MessageBox.Show("Данные могут быть утеряны.\n Хотите сохранить телефонный справочник в файл?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)//если захотели сохранить, то сохраняем в текстовый файл
                {
                    вТекстовыйФайлToolStripMenuItem_Click(sender, e);
                }
                PhoneNote.Clear();//очищаем от записей телефонный справочник
            }
            OpenFileDialog.Filter = "Текст|*.xml";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openBehavior = new OpenXml();
                    openBehavior.CreateNewList(PhoneNote, OpenFileDialog.FileName);
                    if (PhoneNote.Count == 0) current = -1;
                    else
                    {
                        current = 0;
                        if (count == 0)
                        {
                            oldPhoneNote = PhoneNote.ToList();
                            ++count;
                        }//запоминаем только первый раз, чтобы на выходе спросить про сохранение
                    }
                    // выводим текущий элемент
                    PrintElement();
                    OpenFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("xml файла не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckChange())
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    вТекстовыйФайлToolStripMenuItem_Click(sender, e);
                }
            }
        }
        private bool CheckRepeat(Note addPerson)
        {
            if (PhoneNote.Contains(addPerson)) return false;
            else return true;
        }

        private bool CheckChange()
        {
            if (oldPhoneNote.SequenceEqual(PhoneNote)) return false;
            else return true;
        }

        private void поискПоФИОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchNameForm searchName = new SearchNameForm(PhoneNote);
            searchName.ShowDialog();
        }

        private void поискПоАдресуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchAddressForm searchAddress = new SearchAddressForm(PhoneNote);
            searchAddress.ShowDialog();
        }

        private void поТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchPhoneNumberForm searchPhoneNumber = new SearchPhoneNumberForm(PhoneNote);
            searchPhoneNumber.ShowDialog();
        }

        private void поФамилииToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByLastName();
            PrintElement();
        }

        private void поКвартиреToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByFlat();
            PrintElement();
        }

        private void поОтчествуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByPatronomic();
            PrintElement();
        }

        private void поИмениToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByName();
            PrintElement();
        }

        private void поУлицеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByStreet();
            PrintElement();
        }

        private void поДомуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByHouse();
            PrintElement();
        }

        private void поТелефонуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortByPhone();
            PrintElement();
        }
        private void поФамилииToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByLastName();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поИмениToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByName();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поОтчествуToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByPatronomic();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поУлицеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByStreet();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поДомуToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByHouse();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поКвартиреToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByFlat();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void поТелефонуToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SortByPhone();
            PhoneNote.Reverse();
            PrintElement();
        }

        private void SortByLastName()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByLastName());
                current = 0;
            }
        }
        private void SortByName()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByName());
                current = 0;
            }
        }
        private void SortByPatronomic()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByPatronymic());
                current = 0;
            }
        }
        private void SortByHouse()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByHouse());
                current = 0;
            }
        }
        private void SortByFlat()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByFlat());
                current = 0;
            }
        }
        private void SortByStreet()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByStreet());
                current = 0;
            }
        }
        private void SortByPhone()
        {
            if (PhoneNote.Count > 0)
            {
                PhoneNote.Sort(new CompareByPhoneNumber());
                current = 0;
            }
        }

        private void изТекстовогоФайлаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Filter = "Текст|*.txt";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openBehavior = new OpenTxt();
                    openBehavior.AddingData(PhoneNote, OpenFileDialog.FileName);
                    if (PhoneNote.Count == 0) current = -1;
                    else
                    {
                        current = 0;
                        if (count == 0)
                        {
                            oldPhoneNote = PhoneNote.ToList();
                            ++count;
                        };//запоминаем только первый раз, чтобы на выходе спросить про сохранение
                    }
                    // выводим текущий элемент
                    PrintElement();
                    OpenFileDialog.FileName = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("При открытии файла возникла ошибка! Ошибка: " + ex.Message);

                }
            }
        }

        private void изXmlФайлаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Filter = "Текст|*.xml";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openBehavior = new OpenXml();
                    openBehavior.AddingData(PhoneNote, OpenFileDialog.FileName);
                    if (PhoneNote.Count == 0) current = -1;
                    else
                    {
                        current = 0;
                        if (count == 0)
                        {
                            oldPhoneNote = PhoneNote.ToList();
                            ++count;
                        }//запоминаем только первый раз, чтобы на выходе спросить про сохранение
                    }
                    // выводим текущий элемент
                    PrintElement();
                    OpenFileDialog.FileName = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("xml файла не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    class CompareByName : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Name, y.Name);
        }
        #endregion
    }
    class CompareByLastName : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.LastName, y.LastName);
        }
        #endregion
    }
    class CompareByPatronymic : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Patronymic, y.Patronymic);
        }
        #endregion
    }
    class CompareByStreet : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Street, y.Street);
        }
        #endregion
    }
    class CompareByHouse : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return x.House.CompareTo(y.House);
        }
        #endregion
    }

    class CompareByPhoneNumber : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return string.Compare(x.Phone, y.Phone);
        }
        #endregion
    }

    class CompareByFlat : IComparer<Note>
    {
        #region IComparer<Note> Members
        public int Compare(Note x, Note y)
        {
            return x.Flat.CompareTo(y.Flat);
        }
        #endregion
    }

    public enum AddOrEdit
    {
        Add,
        Edit
    }
}
