using System;
using System.Collections.Generic;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // true : 연산자 버튼 클릭함 false : 클릭안함
        private bool OpFlag = false;
        private double saveVaule;

        public MainWindow()
        {
            InitializeComponent();
        }

        // 숫자입력 
        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            // 초기상태(입력된 값X) 일 경우, 연산자 버튼을 클릭X일 경우
            if (txtResult.Text == "0" || OpFlag == false)
            {
                txtResult.Text = btn.Content.ToString();
            }
            else
            {
                txtResult.Text += btn.Content.ToString();
            }

            // 연산 추가 후에 추가로 바꿀것
            txtExp.Text += btn.Content.ToString();
        }

        // 소수점 입력
        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            // 소수점이 찍혀있지 않을 경우
            if (!txtResult.Text.Contains("."))
            {
                txtResult.Text += ".";
                txtExp.Text += ".";
            }
            else
                return;
        }

        // 사칙연산 클릭
        private void OpClick(object sender, RoutedEventArgs e)
        {
            OpFlag = true;
            saveVaule = double.Parse(txtResult.Text);
        }
    }
}
