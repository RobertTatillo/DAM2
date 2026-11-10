using System;
using System.IO;
using System.Text;

class Program
{
    static string rutaCsv = "./acb2526.csv";

    static void Main(string[] args)
    {
        int opcion = 0;

        do{
            Console.WriteLine("\n----- MENÚ ACB 25-26 -----");
            Console.WriteLine("1. Ver los resultados de un equipo");
            Console.WriteLine("2. Ver los resultados de una jornada");
            Console.WriteLine("3. Ver el top 3 de MVPs");
            Console.WriteLine("4. Ver la clasificación");
            Console.WriteLine("5. Guardar los resultados de cada equipo en su fichero");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");

            try{
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        ResultadosEquipo();
                        break;
                    case 2:
                        ResultadosJornada();
                        break;
                    case 3:
                        TopMvps();
                        break;
                    case 4:
                        Clasificacion();
                        break;
                    case 5:
                        FicherosEquipos();
                        break;
                    case 6:
                        Console.WriteLine("Cerrando la aplicación...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } catch (Exception ex){
                Console.WriteLine("Error: " + ex.Message);
            }
        } while (opcion != 6);
    }

    static void PreguntarGuardar(string texto)
    {
        Console.Write("¿Quieres guardarlo en un fichero? (s/n): ");
        string respuesta = Console.ReadLine();

        if (respuesta == "s")
        {
            Console.Write("Nombre del fichero (por ejemplo datos.txt): ");
            string rutaArchivo = "./" + Console.ReadLine();

            File.WriteAllText(rutaArchivo, texto);
            Console.WriteLine("Fichero guardado.");
        }
    }

    // 1. Resultados de un equipo
    static void ResultadosEquipo()
    {
        Console.Write("Nombre del equipo: ");
        string equipo = Console.ReadLine();

        string[] lines = File.ReadAllLines(rutaCsv);
        StringBuilder sb = new StringBuilder();
        int contador = 0;

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');

            // El equipo puede jugar de local (campo 1) o de visitante (campo 2)
            if (fields[1] == equipo || fields[2] == equipo)
            {
                Console.WriteLine(line);
                sb.AppendLine(line);
                contador++;
            }
        }

        if (contador == 0)
        {
            Console.WriteLine("No se ha encontrado ese equipo.");
        }
        else
        {
            PreguntarGuardar(sb.ToString());
        }
    }

    // 2. Resultados de una jornada
    static void ResultadosJornada()
    {
        Console.Write("Introduce el número de la jornada: ");
        string jornada = "Jornada " + Console.ReadLine();

        string[] lines = File.ReadAllLines(rutaCsv);
        StringBuilder sb = new StringBuilder();
        int contador = 0;

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');

            // En el CSV la jornada viene como "Jornada 5"
            if (fields[0] == jornada)
            {
                Console.WriteLine(line);
                sb.AppendLine(line);
                contador++;
            }
        }

        if (contador == 0)
        {
            Console.WriteLine("No se ha encontrado esa jornada.");
        }
        else
        {
            PreguntarGuardar(sb.ToString());
        }
    }

    // 3. Top 3 de MVPs
    static void TopMvps()
    {
        string[] lines = File.ReadAllLines(rutaCsv);

        // En una lista los nombres y en la otra las veces, en la misma posición
        List<string> jugadores = new List<string>();
        List<int> veces = new List<int>();

        // Empezamos en 1 para saltarnos las cabeceras
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            string mvp = fields[4];

            int posicion = jugadores.IndexOf(mvp);
            if (posicion == -1)
            {
                jugadores.Add(mvp);
                veces.Add(1);
            }
            else
            {
                veces[posicion]++;
            }
        }

        StringBuilder sb = new StringBuilder();

        // Buscamos el que más veces tiene, lo sacamos y lo quitamos de las listas
        for (int puesto = 1; puesto <= 3; puesto++)
        {
            int maximo = 0;
            for (int i = 1; i < veces.Count; i++)
            {
                if (veces[i] > veces[maximo])
                {
                    maximo = i;
                }
            }

            string linea = puesto + ". " + jugadores[maximo] + " - " + veces[maximo] + " veces MVP";
            Console.WriteLine(linea);
            sb.AppendLine(linea);

            jugadores.RemoveAt(maximo);
            veces.RemoveAt(maximo);
        }

        PreguntarGuardar(sb.ToString());
    }

    // 4. Clasificación
    static void Clasificacion()
    {
        string[] lines = File.ReadAllLines(rutaCsv);

        // Igual que en los MVPs, cada equipo tiene la misma posición en todas las listas
        List<string> equipos = new List<string>();
        List<int> victorias = new List<int>();
        List<int> derrotas = new List<int>();
        List<int> diferencia = new List<int>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');

            // El resultado viene como "79-69": puntos del local y puntos del visitante
            string[] puntos = fields[3].Split('-');
            int puntosLocal = int.Parse(puntos[0]);
            int puntosVisitante = int.Parse(puntos[1]);

            int local = PosicionEquipo(fields[1], equipos, victorias, derrotas, diferencia);
            int visitante = PosicionEquipo(fields[2], equipos, victorias, derrotas, diferencia);

            if (puntosLocal > puntosVisitante)
            {
                victorias[local]++;
                derrotas[visitante]++;
            }
            else
            {
                victorias[visitante]++;
                derrotas[local]++;
            }

            diferencia[local] += puntosLocal - puntosVisitante;
            diferencia[visitante] += puntosVisitante - puntosLocal;
        }

        StringBuilder sb = new StringBuilder();
        int totalEquipos = equipos.Count;

        // Sacamos cada vez el que más victorias tiene. Si empatan, gana el de más diferencia de puntos
        for (int puesto = 1; puesto <= totalEquipos; puesto++)
        {
            int maximo = 0;
            for (int i = 1; i < equipos.Count; i++)
            {
                if (victorias[i] > victorias[maximo] || (victorias[i] == victorias[maximo] && diferencia[i] > diferencia[maximo]))
                {
                    maximo = i;
                }
            }

            string linea = puesto + ". " + equipos[maximo] + " " + victorias[maximo] + "-" + derrotas[maximo] + " (diferencia " + diferencia[maximo] + ")";
            Console.WriteLine(linea);
            sb.AppendLine(linea);

            equipos.RemoveAt(maximo);
            victorias.RemoveAt(maximo);
            derrotas.RemoveAt(maximo);
            diferencia.RemoveAt(maximo);
        }

        PreguntarGuardar(sb.ToString());
    }

    // Devuelve la posición del equipo en la lista y si no está lo añade
    static int PosicionEquipo(string equipo, List<string> equipos, List<int> victorias, List<int> derrotas, List<int> diferencia)
    {
        int posicion = equipos.IndexOf(equipo);
        if (posicion == -1)
        {
            equipos.Add(equipo);
            victorias.Add(0);
            derrotas.Add(0);
            diferencia.Add(0);
            posicion = equipos.Count - 1;
        }
        return posicion;
    }

    // 5. Un fichero por cada equipo
    static void FicherosEquipos()
    {
        string[] lines = File.ReadAllLines(rutaCsv);

        // Primero sacamos la lista de equipos sin repetir
        List<string> equipos = new List<string>();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            if (equipos.IndexOf(fields[1]) == -1)
            {
                equipos.Add(fields[1]);
            }
        }

        // Para cada equipo juntamos sus partidos y los guardamos en su fichero
        foreach (string equipo in equipos)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields[1] == equipo || fields[2] == equipo)
                {
                    sb.AppendLine(line);
                }
            }

            File.WriteAllText("./" + equipo + ".txt", sb.ToString());
            Console.WriteLine("Fichero creado: " + equipo + ".txt");
        }
    }
}
