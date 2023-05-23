double[] dTarr = new double[] { 0.1, 0.001 };
double h = 0.1;
double x_0 = 0;
double x_n = 1;
double t_0 = 0;
double t_n = 1;

int x_nums = (int)((x_n - x_0) / h);

foreach (double dT in dTarr)
{
    int N = (int)((t_n - t_0) / dT);
    double[,] U = new double[N + 1, x_nums + 1];

    for (int x_index = 0; x_index <= x_nums; x_index++)
        U[0, x_index] = GetValueByIndex(x_0, h, x_index) + 1;

    int n = 0;
    while (n < N)
    {
        double[] alpha = new double[x_nums + 1];
        double[] betta = new double[x_nums + 1];

        alpha[0] = 0;
        betta[0] =(n + 1) * dT + 1;
        for (int x_index = 1; x_index < x_nums; x_index++)
        {
            double a = -dT / (h * h);
            double b = 1 + 2 * dT / (h * h);
            double c = a;
            double eps = U[n, x_index] + dT / (n + 1) * dT + 1;

            alpha[x_index] = -a / (b + c * alpha[x_index - 1]);
            betta[x_index] = (eps - c * betta[x_index - 1]) / (b + c * alpha[x_index - 1]);
        }

        U[n + 1, x_nums] = (n + 1) * dT + 1 + 1;

        for (int x_index = x_nums - 1; x_index >= 0; x_index--)
        {
            U[n + 1, x_index] = alpha[x_index] * U[n + 1, x_index + 1] + betta[x_index];
        }

        n++;
    }
    printU(U, x_0, h, dT, t_0);
}

static double GetValueByIndex(double start, double h, int index) => start + Convert.ToDouble(index) * h;


static void printU(double[,] U, double x_start, double h, double dT, double t_start)
{
    Console.WriteLine($"dT = {dT}; h = {h}");
    Console.Write(String.Format("|{0, 6}|", "t\\x"));
    for (int i = 0; i < U.GetLength(1); i++)
        Console.Write(String.Format("{0, 8: 0.000}|", GetValueByIndex(x_start, h, i)));

    Console.WriteLine();
    for (int i = 0; i < U.GetLength(0); i++)
    {
        Console.Write(String.Format("|{0, 5: 0.000}|", GetValueByIndex(t_start, dT, i)));
        for (int j = 0; j < U.GetLength(1); j++)
            Console.Write(String.Format("{0, 8: 0.000}|", U[i, j]));
        Console.WriteLine();
    }
}
