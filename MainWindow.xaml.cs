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
        // 처음 입력한 숫자 저장
        private double var1;
        // 연산자 이후 입력 숫자 저장
        private double var2;
        private object op;

        public MainWindow()
        {
            InitializeComponent();
        }

        // 입력 숫자 변경 시 숫자포맷 적용
        private static string NumberFormat(String beforeNum)
        {
            string[] dotNum;
            String afterNum;

            if(beforeNum.Contains("."))
            {
                dotNum = beforeNum.Split(".");
                beforeNum = dotNum[0].ToString();
                afterNum = String.Format("{0:#,##0}", Double.Parse(beforeNum));
                afterNum = afterNum + "." + dotNum[1].ToString();
            }
            else
            {
                afterNum = String.Format("{0:#,##0}", Double.Parse(beforeNum));
            }
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
            // 소수점 입력 시 숫자포맷 적용 함수 타지않고 종료
            else if (num == ".")
            {
                txtResult.Text += num;
                return;
            }

            txtResult.Text = NumberFormat(txtResult.Text.ToString());
        }

        // 사칙연산 클릭
        private void OpClick(object sender, RoutedEventArgs e)
        {
            OpFlag = true;
            var1 = double.Parse(txtResult.Text);

            Button btn = (Button)sender;
            op = btn.Content.ToString();

            txtExp.Text = NumberFormat(var1.ToString()) + " " + op;
        }

        // 결과값(=) 클릭
        private void btnEqual(object sender, RoutedEventArgs e)
        {
            // 결과값 출력 후 또 같은 버튼을 누를경우 값 세팅
            if (txtExp.Text.Contains("="))
            {
                var1 = double.Parse(txtResult.Text);
                txtExp.Text = NumberFormat(var1.ToString()) + " " + op;
            }
            else
            {
                var2 = double.Parse(txtResult.Text);
            }
           
            switch (op)
            {
                case "+":
                    txtResult.Text = (var1 + var2).ToString();
                    break;
                case "－":
                    txtResult.Text = (var1 - var2).ToString();
                    break;
                case "×":
                    txtResult.Text = (var1 * var2).ToString();
                    break;
                case "÷":
                    if (var1 > 0)
                    {
                        txtResult.Text = (var1 / var2).ToString();
                    }
                    else
                    {
                        // 연산자, 소수점 클릭 불가처리 해야함
                        txtResult.Text = "0으로 나눌 수 없습니다";
                        txtResult.FontSize = 25;
                        OpFlag = true;
                        return;
                    }
                    break;
            }
            txtResult.Text = NumberFormat(txtResult.Text.ToString());
            txtExp.Text += " " + NumberFormat(var2.ToString()) + " =";
            OpFlag = true;
        }

        // 지우기 버튼
        private void btnClear(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            object clear = btn.Content.ToString();

            switch (clear)
            {
                case "←":
                    if (txtResult.Text.Length == 1 || txtResult.Text == "0")
                    {
                        txtResult.Text = "0";
                    }
                    // 연산자 클릭 또는 결과식 완료 후
                    else if (OpFlag)
                    {
                        if (op == null)
                        {
                            txtExp.Text = "";
                        }
                        else
                        {
                            return;
                        }
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
                    if(OpFlag)
                    {
                        txtExp.Text = "";
                    }
                    txtResult.Text = "0";
                    break;
            }
            txtResult.Text = NumberFormat(txtResult.Text.ToString());
        }

        // ± 클릭
        private void changeSign(object sender, RoutedEventArgs e)
        {
            double changeSign = -double.Parse(txtResult.Text);
            txtResult.Text = NumberFormat(changeSign.ToString());
            // 기록이 있을 경우 negate 붙여야함
        }

        // 제곱 계산
        private void squareClick(object sender, RoutedEventArgs e)
        {
            double square = Math.Pow(double.Parse(txtResult.Text),2);
            txtExp.Text = "sqr(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat(square.ToString());
            OpFlag = true;
        }

        // 루트 계산
        private void routeClick(object sender, RoutedEventArgs e)
        {
            double route = Math.Sqrt(double.Parse(txtResult.Text));
            txtExp.Text = "√(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat(route.ToString());
            OpFlag = true;
        }

        // 분수 계산
        private void fractionClick(object sender, RoutedEventArgs e)
        {
            txtExp.Text = "1/(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat((1.0/double.Parse(txtResult.Text)).ToString());
            OpFlag = true;
        }

        // 퍼센트 계산
        private void percentClick(object sender, RoutedEventArgs e)
        {
           double percent;
           if(!var1.Equals(""))
            {
                percent = double.Parse(txtResult.Text) / 100;
                txtResult.Text = NumberFormat(percent.ToString());
                txtExp.Text = txtExp.Text + " " + txtResult.Text;
            }
           else
            {
                txtResult.Text = "0";
                txtExp.Text = "0";
            }
        }
    }
}
