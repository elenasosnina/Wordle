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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wordle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private TextBox[][] textBoxArrays;
        private string _targetWord;
        private bool isFirstArrayFilled = false;

        private void StartNewGame()
        {
            // Инициализация массива текстовых полей
            textBoxArrays = new TextBox[5][];
            for (int i = 0; i < 5; i++)
            {
                textBoxArrays[i] = new TextBox[5];
                for (int j = 0; j < 5; j++)
                {
                    textBoxArrays[i][j] = (TextBox)this.FindName("LetterBox" + (i * 5 + j + 1));
                    textBoxArrays[i][j].IsReadOnly = true;
                }
            }

            
        }
        int k =0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonText = clickedButton.Content.ToString();

            // Заполнение текстовых полей
            for (int i = k; i < 5; i++)
            {
              
                for (int j = 0; j < 5; j++)
                {
                    // Проверяем, пустое ли текущее текстовое поле
                    if (string.IsNullOrEmpty(textBoxArrays[i][j].Text))
                    {
                        textBoxArrays[i][j].Text = buttonText;

                        // Переход к следующему полю, если это необходимо
                        if (i == 4 && j == 4)
                        {
                            isFirstArrayFilled = !isFirstArrayFilled;
                        }
                        else if (j < 4)
                        {
                            textBoxArrays[i][j + 1].Focus();
                        }

                        break; // Выход из внутреннего цикла
                    }
                }
                // Выход из внешнего цикла, если поле найдено и заполнено
                if (!string.IsNullOrEmpty(textBoxArrays[i][0].Text))
                {
                    break;
                }
            }
        }

        private void LetterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Перемещение фокуса на следующее текстовое поле
            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                for (int i = k; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (textBoxArrays[i][j] == sender)
                        {
                            if (j < 4)
                            {
                                textBoxArrays[i][j + 1].Focus();
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void Sub_click(object sender, RoutedEventArgs e)
        {
            string word = "БУФЕТ";
            char[] letters = word.ToCharArray();
            // Проверка введенного слова
            int p = 0;
            for (int i = k; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (textBoxArrays[k][j].Text.ToUpper() == letters[j].ToString())
                    {
                        textBoxArrays[k][j].Background = System.Windows.Media.Brushes.Green;
                        p++;
                    }
                    else if (word.Contains(textBoxArrays[k][j].Text.ToUpper()))
                    {
                        textBoxArrays[k][j].Background = System.Windows.Media.Brushes.Yellow;
                    }
                    else
                    {
                        textBoxArrays[k][j].Background = System.Windows.Media.Brushes.Red;
                    }
                }
            }
            // Проверка на победу
            if (p == 5)
            {
                MessageBox.Show($"Молодцы! Загаданное слово - {word}");
            }
            else if (p !=5 && k==4)
            {
                MessageBox.Show($"Вы не угадали слово. Загаданное слово - {word}");
            }
            k++;
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            // Очистка всех текстовых полей
            for (int i = k; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    textBoxArrays[i][j].Text = "";
                    textBoxArrays[i][j].Background = System.Windows.Media.Brushes.White;
                }
            }
        }
    }

}

