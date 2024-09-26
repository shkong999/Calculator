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
                //afterNum = beforeNum.ToString("N");
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
            saveVaule = double.Parse(txtResult.Text);

            Button btn = (Button)sender;
            op = btn.Content.ToString();

            txtExp.Text = NumberFormat(saveVaule.ToString()) + " " + op;
        }

        // 결과값(=) 클릭
        private void btnEqual(object sender, RoutedEventArgs e)
        {
            double var1 = double.Parse(txtResult.Text);

            // 결과값 출력 후 또 같은 버튼을 누를경우
            // op 값 확인, 식 빈값으로 안돌아가는거 확인해야
            if (txtExp.Text.Contains("="))
            {
                /*txtExp.Text = saveVaule.ToString() + "op" + var1.ToString();*/

/*                txtResult.Text += saveVaule;
                txtExp.Text = "";
                txtExp.Text = var1.ToString() + op + saveVaule.ToString();*/
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
            txtResult.Text = NumberFormat(txtResult.Text.ToString());
            txtExp.Text += " " + NumberFormat(var1.ToString()) + " =";
            OpFlag = true;
        }

        // 지우기 버튼
        private void btnClear(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            object clear = btn.Content.ToString();

            if(OpFlag)
            {
                if(!clear.Equals("←"))
                {
                    txtResult.Text = "0";
                    return;
                }
            }
            switch (clear)
            {
                case "←":
                    if (txtResult.Text.Length == 1 || txtResult.Text == "0")
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
            txtResult.Text = NumberFormat(txtResult.Text.ToString());
        }

        // ± 클릭
        private void changeSign(object sender, RoutedEventArgs e)
        {
            double changeSign = -double.Parse(txtResult.Text);
            txtResult.Text = NumberFormat(changeSign.ToString());
            // 기록이 있을 경우 negate 붙여야함
        }

        // 제곱
        private void squareClick(object sender, RoutedEventArgs e)
        {
            double square = Math.Pow(double.Parse(txtResult.Text),2);
            txtExp.Text = "sqr(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat(square.ToString());
            OpFlag = true;
        }

        // 루트
        private void routeClick(object sender, RoutedEventArgs e)
        {
            double route = Math.Sqrt(double.Parse(txtResult.Text));
            txtExp.Text = "√(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat(route.ToString());
            OpFlag = true;
        }

        private void fractionClick(object sender, RoutedEventArgs e)
        {
            txtExp.Text = "1/(" + txtResult.Text + ")";
            txtResult.Text = NumberFormat((1.0/double.Parse(txtResult.Text)).ToString());
            OpFlag = true;
        }

        private void percentClick(object sender, RoutedEventArgs e)
        {
           double percent;
           if(!saveVaule.Equals(""))
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
