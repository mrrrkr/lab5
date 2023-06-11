using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git
{
    public class LinearEquation
    {
        List<float> coefficients;
        public int Size => coefficients.Count;

        public LinearEquation(params float[] coefficients)
        {
            this.coefficients = new List<float>();
            this.coefficients.AddRange(coefficients);
        }
        public LinearEquation(List<float> coefficients)
        {
            this.coefficients = new List<float>();
            this.coefficients = coefficients;
        }

        // Суммирует свободный член first с second

        static public LinearEquation operator +(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.Size - 1] += second;
            return equation;
        }

        // Вычитает second из свободного члена first

        static public LinearEquation operator -(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.Size - 1] -= second;
            return equation;
        }
        public override bool Equals(object obj)
        {
            if (obj is LinearEquation equation)
            {
                if (Size != equation.Size)
                    return false;
                for (int i = 0; i < Size; i++)
                {
                    if (this.coefficients[i] != equation.coefficients[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        static public bool operator ==(LinearEquation first, LinearEquation second)
        {
            return first.Equals(second);
        }
        static public bool operator !=(LinearEquation first, LinearEquation second)
        {
            return !first.Equals(second);
        }
        public float this[int i]
        {
            get { return coefficients[i]; }
            set { coefficients[i] = value; }
        }

        public static LinearEquation operator +(LinearEquation a, LinearEquation b)
        {
            if (a.Size != b.Size)
                throw new IndexOutOfRangeException("Уравнения разной длинны");

            //копируем уравнение
            LinearEquation temp = a;

            //складываем коэф
            for (int i = 0; i < temp.Size; i++)
                temp.coefficients[i] += b.coefficients[i];

            return temp;
        }
        public static LinearEquation operator -(LinearEquation a, LinearEquation b)
        {
            if (a.Size != b.Size)
                throw new IndexOutOfRangeException("Уравнения разной длинны");

            //копируем уравнение
            LinearEquation temp = a;

            //складываем коэф
            for (int i = 0; i < temp.Size; i++)
                temp.coefficients[i] -= b.coefficients[i];

            return temp;
        }

        public static implicit operator bool(LinearEquation a)
        {
            //если свободный член равен 0(последний в массиве), то уравнение имеет корни
            if (a.coefficients[a.Size - 1] == 0)
                return true;
            //если хотя бы один коэф. не ноль, то уравнение имеет корни
            for (int i = 0; i < a.Size - 1; i++)
                if (a.coefficients[i] != 0)
                    return true;
            //иначе уравнение не имеет корней
            return false;
        }

        public float? GetRoot(LinearEquation a)
        {
            if (a.Size != 2)
                return null;
            else
                return -a.coefficients[1] / coefficients[0];
        }

        public override string ToString()
        {
            string str = "";
            //проверка на введённое первое значение в строку
            bool first = true;
            //пробегаемс по коэф при неизвестном
            for (int i = 0; i < this.Size - 1; i++)
            {
                //если коэф. первый, то не ставим никаких знаков(если не 0)
                if (this.coefficients[i] != 0 && first)
                {
                    //если 1
                    if (this.coefficients[i] == 1)
                        str += "x";
                    //если -1
                    else if (this.coefficients[i] == -1)
                        str += $"-x";
                    //иначе ставим коэф.
                    else
                        str += $"{this.coefficients[i]}x";
                    first = false;
                }
                //если не первый
                else
                {
                    //если отрицательный, то минус сам поставится
                    if (this.coefficients[i] < 0)
                    {
                        //если -1
                        if (this.coefficients[i] == -1)
                            str += $"-x";
                        //иначе
                        else
                            str += $"{this.coefficients[i]}x";
                    }
                    //если коэф положительный
                    else if (this.coefficients[i] != 0)
                    {
                        if (this.coefficients[i] == 1)
                            str += $"+x";
                        else
                            str += $"+{this.coefficients[i]}x";
                    }
                }
            }
            //добавляем свободный член
            if (this.coefficients[this.Size - 1] < 0)
                str += $"{this.coefficients[this.Size - 1]}";
            else if (this.coefficients[this.Size - 1] != 0)
                str += $"+{this.coefficients[this.Size - 1]}";

            //добавляем в конце =0
            str += "=0";
            return str;
        }
        static public Random rand = new Random();
        public static LinearEquation RandomLinearEq(int count)
        {
            List<float> array = new List<float>();
            for (int i = 0; i < count; i++)
                array.Add(rand.Next(-100, 100));
            LinearEquation a = new LinearEquation(array);
            return a;
        }

        public static LinearEquation SpecificLinearEq(int count, float value)
        {
            List<float> array = new List<float>();
            for (int i = 0; i < count; i++)
                array.Add(value);
            LinearEquation a = new LinearEquation(array);
            return a;
        }
        public static LinearEquation operator *(float value, LinearEquation a)
        {
            for (int i = 0; i < a.Size; i++)
                a.coefficients[i] *= value;
            return a;
        }
        public static LinearEquation operator *(LinearEquation a, float value)
        {
            for (int i = 0; i < a.Size; i++)
            {
                a.coefficients[i] *= value;
                a.coefficients[i] = (float)Math.Round(a.coefficients[i], 2);
            }
            return a;
        }
    }
}
