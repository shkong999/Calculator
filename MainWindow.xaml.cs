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
        private bool OpFlag;
        private double saveVaule;
        private object op;

        public MainWindow()
        {
            InitializeComponent();
        }

        private static string NumberFormat(string beforNum)
        {

            if (beforNum.Contains("."))
            {
                string[] dotNum = beforNum.Split(".");
                beforNum = dotNum[0];
            }

            // 형변환 찾아보기
            String afterNum = String.Format("{0:#,###}", beforNum);

            return afterNum;
        }

        // 입력 
        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var num = btn.Content.ToString();

            // 초기상태(입력된 값X) 일 경우, 연산자 버튼을 클릭X일 경우
            if (txtResult.Text == "0" || OpFlag)
            {
                // 계산 후 새로운 값 입력 시 기존의 식 제거
                if(txtExp.Text.EndsWith("="))
                {
                    txtExp.Text = "";
                }
                txtResult.Text = num;
                OpFlag = false;
            }
            else if (!txtResult.Text.Contains("."))
            {
                txtResult.Text += num;
            }
            // 조건 없이 사용할 경우 소수점 중복 추가
            else if (num != ".")
            {
                txtResult.Text += num;
            }

            txtResult.Text = NumberFormat(txtResult.Text);
        }

        // 사칙연산 클릭
        private void OpClick(object sender, RoutedEventArgs e)
        {
            OpFlag = true;
            saveVaule = double.Parse(txtResult.Text);

            Button btn = (Button)sender;
            op = btn.Content.ToString();

            txtExp.Text = txtResult.Text + " " + op;
        }

        // 결과값(=) 클릭
        private void btnEqual(object sender, RoutedEventArgs e)
        {
            double var1 = double.Parse(txtResult.Text);

            // 결과값 출력 후 또 같은 버튼을 누를경우
            // op 값 확인, 식 빈값으로 안돌아가는거 확인해야
            if(txtExp.Text.Contains("="))
            {
                txtResult.Text += saveVaule;
                txtExp.Text = "";
                txtExp.Text = var1.ToString() + op + saveVaule.ToString();
            }

            switch (op)
            {
                case "+":
                    txtResult.Text = (saveVaule + var1).ToString();
                    break;
                case "－":
                    txtResult.Text = (saveVaule - var1).ToString();
                    break;
                case "×":
                    txtResult.Text = (saveVaule * var1).ToString();
                    break;
                case "÷":
                    if (var1 > 0)
                    {
                        txtResult.Text = (saveVaule / var1).ToString();
                    }
                    else
                    {
                        txtResult.Text = "0으로 나눌 수 없습니다";
                        txtResult.FontSize = 25;
                        OpFlag = true;
                        return;
                    }
                    break;
            }
            txtExp.Text += " " + var1 + " =";
            OpFlag = true;
        }

        // 지우기 버튼
        private void btnClear(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            object clear = btn.Content.ToString();

            // 연산자 입력 후 지우기 버튼 눌렀을 경우 지울 수 없음
            if (OpFlag)
            {
                return;
            }
            else
            {
                switch (clear)
                {
                    case "←":
                        if(txtResult.Text.Length == 1 || txtResult.Text == "0")
                        {
                            txtResult.Text = "0";
                        }
                        else
                        {
                            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
                        }
                        break;
                    case "C":
                        txtResult.Text = "0";
                        txtExp.Text = "";
                        break;
                    case "CE":
                        txtResult.Text = "0";
                        break;
                }
            }
        }
    }
}
