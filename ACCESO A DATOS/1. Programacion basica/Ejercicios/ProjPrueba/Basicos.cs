
// Pedir al usuario dos números, uno entero y otro real, y mostrar su producto

class Ejercicios
{
    // EJERCICIO 1
    // Pedir un número entero y otro real y mostrar su producto.
    static void Ejercicio1()
    {
        Console.Write("Introduce un número entero: ");
        int numeroEntero = int.Parse(Console.ReadLine());

        Console.Write("Introduce un número real: ");
        double numeroReal = double.Parse(Console.ReadLine());

        double producto = numeroEntero * numeroReal;

        Console.WriteLine("El producto es: " + producto);
    }


    // EJERCICIO 2
    // Calcular el área de un círculo.
    static void Ejercicio2()
    {
        Console.Write("Introduce el radio del círculo: ");
        double radio = double.Parse(Console.ReadLine());

        double area = Math.PI * radio * radio;

        Console.WriteLine("El área del círculo es: " + area);
    }


    // EJERCICIO 3
    // Resolver una ecuación de segundo grado:
    // ax² + bx + c = 0
    static void Ejercicio3()
    {
        Console.Write("Introduce el valor de a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Introduce el valor de b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Introduce el valor de c: ");
        double c = double.Parse(Console.ReadLine());

        double discriminante = b * b - 4 * a * c;

        if (a == 0)
        {
            Console.WriteLine("No es una ecuación de segundo grado.");
        }
        else if (discriminante > 0)
        {
            double raiz1 = (-b + Math.Sqrt(discriminante)) / (2 * a);
            double raiz2 = (-b - Math.Sqrt(discriminante)) / (2 * a);

            Console.WriteLine("La primera raíz es: " + raiz1);
            Console.WriteLine("La segunda raíz es: " + raiz2);
        }
        else if (discriminante == 0)
        {
            double raiz = -b / (2 * a);

            Console.WriteLine("Hay una única raíz: " + raiz);
        }
        else
        {
            Console.WriteLine("No existen raíces reales.");
        }
    }


    // EJERCICIO 4
    // Pedir nombre y edad y mostrar una frase.
    static void Ejercicio4()
    {
        Console.Write("Introduce tu nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Introduce tu edad: ");
        int edad = int.Parse(Console.ReadLine());

        Console.WriteLine("Te llamas " + nombre + " y tienes " + edad + " años.");
    }


    // EJERCICIO 5
    // Pedir un entero y mostrar su cuadrado.
    static void Ejercicio5()
    {
        Console.Write("Introduce un número entero: ");
        int numero = int.Parse(Console.ReadLine());

        int cuadrado = numero * numero;

        Console.WriteLine("El cuadrado es: " + cuadrado);
    }


    // EJERCICIO 6
    // Pedir una cantidad de euros y calcular las monedas necesarias.
    static void Ejercicio6()
    {
        Console.Write("Introduce una cantidad de euros: ");
        double euros = double.Parse(Console.ReadLine());

        // Convertimos a céntimos para evitar problemas con decimales.
        int centimos = (int)Math.Round(euros * 100);

        int monedas2 = centimos / 200;
        centimos = centimos % 200;

        int monedas1 = centimos / 100;
        centimos = centimos % 100;

        int monedas50 = centimos / 50;
        centimos = centimos % 50;

        int monedas20 = centimos / 20;
        centimos = centimos % 20;

        int monedas10 = centimos / 10;
        centimos = centimos % 10;

        int monedas5 = centimos / 5;
        centimos = centimos % 5;

        int monedas2Centimos = centimos / 2;
        centimos = centimos % 2;

        int monedas1Centimo = centimos;

        Console.WriteLine("Monedas de 2 €: " + monedas2);
        Console.WriteLine("Monedas de 1 €: " + monedas1);
        Console.WriteLine("Monedas de 50 céntimos: " + monedas50);
        Console.WriteLine("Monedas de 20 céntimos: " + monedas20);
        Console.WriteLine("Monedas de 10 céntimos: " + monedas10);
        Console.WriteLine("Monedas de 5 céntimos: " + monedas5);
        Console.WriteLine("Monedas de 2 céntimos: " + monedas2Centimos);
        Console.WriteLine("Monedas de 1 céntimo: " + monedas1Centimo);
    }


    // PROGRAMA PRINCIPAL
    static void Main()
    {
        Ejercicio1();

        Console.WriteLine();
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        Ejercicio2();

        Console.WriteLine();
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        Ejercicio3();

        Console.WriteLine();
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        Ejercicio4();

        Console.WriteLine();
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        Ejercicio5();

        Console.WriteLine();
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        Ejercicio6();
    }
}