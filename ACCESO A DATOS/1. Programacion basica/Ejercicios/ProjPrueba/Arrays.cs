using System;

class Program
{
    static void Main(string[] args)
    {
        // Descomenta el que quieras ejecutar:

        // Ejercicio1();
        // Ejercicio2();
        // Ejercicio3();
        // Ejercicio4();
    }

    // ------------------------------------------------------------------------
    // Ejercicio 1: Máximo, mínimo y sus posiciones
    // ------------------------------------------------------------------------
    static void Ejercicio1()
    {
        int[] numeros = new int[10];

        for (int i = 0; i < 10; i++)
        {
            Console.Write($"Introduce el número {i + 1}: ");
            numeros[i] = int.Parse(Console.ReadLine());
        }

        int max = numeros[0];
        int min = numeros[0];
        int posMax = 0;
        int posMin = 0;

        for (int i = 1; i < 10; i++)
        {
            if (numeros[i] > max)
            {
                max = numeros[i];
                posMax = i;
            }
            if (numeros[i] < min)
            {
                min = numeros[i];
                posMin = i;
            }
        }

        Console.WriteLine($"\nMáximo: {max} en la posición {posMax}");
        Console.WriteLine($"Mínimo: {min} en la posición {posMin}");
    }

    // ------------------------------------------------------------------------
    // Ejercicio 2: 20 números aleatorios y filtrado de primos
    // ------------------------------------------------------------------------
    static void Ejercicio2()
    {
        Random rand = new Random();
        int[] numeros = new int[20];
        int contadorPrimos = 0;

        Console.WriteLine("Números generados:");
        for (int i = 0; i < 20; i++)
        {
            numeros[i] = rand.Next(0, 100);
            Console.Write(numeros[i] + " ");

            if (EsPrimo(numeros[i]))
            {
                contadorPrimos++;
            }
        }
        Console.WriteLine();

        // Creamos el array de primos con el tamaño exacto que hemos contado
        int[] primos = new int[contadorPrimos];
        int indicePrimos = 0;

        for (int i = 0; i < 20; i++)
        {
            if (EsPrimo(numeros[i]))
            {
                primos[indicePrimos] = numeros[i];
                indicePrimos++;
            }
        }

        Console.WriteLine("\nNúmeros primos guardados en el nuevo array:");
        for (int i = 0; i < primos.Length; i++)
        {
            Console.Write(primos[i] + " ");
        }
        Console.WriteLine();
    }

    // Función auxiliar para comprobar si un número es primo
    static bool EsPrimo(int numero)
    {
        if (numero <= 1) return false;
        for (int i = 2; i < numero; i++)
        {
            if (numero % i == 0) return false;
        }
        return true;
    }

    // ------------------------------------------------------------------------
    // Ejercicio 3: Suma de dos arrays (array3 = array1 + array2)
    // ------------------------------------------------------------------------
    static void Ejercicio3()
    {
        int[] array1 = new int[5];
        int[] array2 = new int[5];
        int[] array3 = new int[5];

        Console.WriteLine("--- Datos para Array 1 ---");
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"array1[{i}]: ");
            array1[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("\n--- Datos para Array 2 ---");
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"array2[{i}]: ");
            array2[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < 5; i++)
        {
            array3[i] = array1[i] + array2[i];
        }

        Console.WriteLine("\nResultado de array3:");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(array3[i] + " ");
        }
        Console.WriteLine();
    }

    // ------------------------------------------------------------------------
    // Ejercicio 4: Control de notas del alumno
    // ------------------------------------------------------------------------
    static void Ejercicio4()
    {
        double[] notas = new double[5];

        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Introduce la nota {i + 1} (0-10): ");
            notas[i] = double.Parse(Console.ReadLine());

            while (notas[i] < 0 || notas[i] > 10)
            {
                Console.Write("Nota fuera de rango. Introduce un valor entre 0 y 10: ");
                notas[i] = double.Parse(Console.ReadLine());
            }
        }

        double suma = 0;
        double max = notas[0];
        double min = notas[0];

        Console.Write("\nTodas las notas: ");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(notas[i] + " ");
            suma += notas[i];

            if (notas[i] > max) max = notas[i];
            if (notas[i] < min) min = notas[i];
        }

        double media = suma / 5;

        Console.WriteLine($"\nNota media: {media}");
        Console.WriteLine($"Nota más alta: {max}");
        Console.WriteLine($"Nota más baja: {min}");
    }
}