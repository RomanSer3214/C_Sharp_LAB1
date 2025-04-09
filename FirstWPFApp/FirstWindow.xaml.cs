using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstWPFApp
{
    public class Student
    {
        private string studentID;
        private string fullName;
        private string personalData;

        public string GetStudentID()
        {
            return studentID;
        }

        public void SetStudentID(string value)
        {
            studentID = value;
        }

        public void SetFullName(string value)
        {
            fullName = value;
        }

        public void SetPersonalData(string value)
        {
            personalData = value;
        }

        public override string ToString()
        {
            return $"{studentID}, {fullName}, {personalData}";
        }

        public static Student FromString(string input)
        {
            var parts = input.Split(',');
            var student = new Student();
            student.studentID = parts[0].Trim();
            student.fullName = parts[1].Trim();
            student.personalData = parts[2].Trim();
            return student;
        }
    }

    public partial class FirstWindow : Window
    {
        private const string FilePath = "students.txt";
        public FirstWindow()
        {
            InitializeComponent();
            LoadFileContent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown(); 
        }
        private void GoToMainWin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void LoadFileContent()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string fileContent = File.ReadAllText(FilePath);
                    FileContentTB.Text = fileContent; 
                }
                else
                {
                    FileContentTB.Text = "Файл не знайдено.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при зчитуванні файлу {ex.Message}");
            }
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var studentID = StudentIDTB.Text;
            var fullName = FullNameTB.Text;
            var personalData = PersonalDataTB.Text;

            if (string.IsNullOrEmpty(studentID) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(personalData))
            {
                MessageBox.Show("Заповніть всі поля");
                return;
            }

            var student = new Student();
            student.SetStudentID(studentID);
            student.SetFullName(fullName);
            student.SetPersonalData(personalData);

            try
            {
                File.AppendAllText(FilePath, student.ToString() + Environment.NewLine);
                LoadFileContent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні студента {ex.Message}");
            }
        }

        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var studentIDToDelete = StudentIDTB.Text;

            if (string.IsNullOrEmpty(studentIDToDelete))
            {
                MessageBox.Show("Введіть номер залікової книжки для видалення");
                return;
            }

            try
            {
                var students = File.ReadAllLines(FilePath)
                                   .Select(line => Student.FromString(line))
                                   .ToList();

                var studentToRemove = students.FirstOrDefault(s => s.GetStudentID() == studentIDToDelete);

                if (studentToRemove != null)
                {
                    students.Remove(studentToRemove);
                    File.WriteAllLines(FilePath, students.Select(s => s.ToString()));

                    MessageBox.Show("Студента видалено успішно");
                    LoadFileContent();
                }
                else
                {
                    MessageBox.Show("Студент не знайдений");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні студента: {ex.Message}");
            }
        }
    }
}
