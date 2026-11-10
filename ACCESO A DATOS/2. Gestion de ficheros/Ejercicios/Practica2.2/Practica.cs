using System;
using System.IO;
using System.Text;
using System.Xml;

class Program
{
    static string rutaCsvOrigen = "../../../NetflixOriginals.csv";
    static string rutaXmlDestino = "../../../NetflixOriginals.xml";
    static string rutaXmlOrigen = "../../../NetflixOriginals.xml";
    static string rutaCsvDestino = "../../../NetflixDesdeXml.csv";

    static string[] cabeceras = { "Title", "Genre", "Premiere", "Runtime", "IMDBScore", "Language" };

    static void Main(string[] args)
    {
        int opcion = -1;

        do{
            Console.WriteLine("\n----- MENÚ -----");
            Console.WriteLine("1. De CSV a XML");
            Console.WriteLine("2. De XML a CSV");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opción: ");

            try{
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        MenuCsvAXml();
                        break;
                    case 2:
                        MenuXmlACsv();
                        break;
                    case 0:
                        Console.WriteLine("Cerrando la aplicación...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } catch (Exception ex){
                Console.WriteLine("Error: " + ex.Message);
            }
        } while (opcion != 0);
    }

    static void MenuCsvAXml()
    {
        int opcion = -1;

        do{
            Console.WriteLine("\n----- DE CSV A XML -----");
            Console.WriteLine("1. Elegir la ruta del CSV");
            Console.WriteLine("2. Elegir dónde guardar el XML");
            Console.WriteLine("3. Exportar todo el documento");
            Console.WriteLine("4. Exportar por género");
            Console.WriteLine("5. Exportar por año");
            Console.WriteLine("6. Exportar por lenguaje");
            Console.WriteLine("0. Volver");
            Console.Write("Elige una opción: ");

            try{
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ruta del CSV: ");
                        rutaCsvOrigen = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Ruta del XML: ");
                        rutaXmlDestino = Console.ReadLine();
                        break;
                    case 3:
                        EscribirXml(LeerCsv(rutaCsvOrigen), rutaXmlDestino);
                        break;
                    case 4:
                        EscribirXml(FiltrarPorLista(LeerCsv(rutaCsvOrigen), 1), rutaXmlDestino);
                        break;
                    case 5:
                        EscribirXml(FiltrarPorAnio(LeerCsv(rutaCsvOrigen)), rutaXmlDestino);
                        break;
                    case 6:
                        EscribirXml(FiltrarPorLista(LeerCsv(rutaCsvOrigen), 5), rutaXmlDestino);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } catch (Exception ex){
                Console.WriteLine("Error: " + ex.Message);
            }
        } while (opcion != 0);
    }

    static void MenuXmlACsv()
    {
        int opcion = -1;

        do{
            Console.WriteLine("\n----- DE XML A CSV -----");
            Console.WriteLine("1. Elegir la ruta del XML");
            Console.WriteLine("2. Elegir dónde guardar el CSV");
            Console.WriteLine("3. Exportar todo el documento");
            Console.WriteLine("4. Exportar por género");
            Console.WriteLine("5. Exportar por año");
            Console.WriteLine("6. Exportar por lenguaje");
            Console.WriteLine("0. Volver");
            Console.Write("Elige una opción: ");

            try{
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ruta del XML: ");
                        rutaXmlOrigen = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Ruta del CSV: ");
                        rutaCsvDestino = Console.ReadLine();
                        break;
                    case 3:
                        EscribirCsv(LeerXml(rutaXmlOrigen), rutaCsvDestino);
                        break;
                    case 4:
                        EscribirCsv(FiltrarPorLista(LeerXml(rutaXmlOrigen), 1), rutaCsvDestino);
                        break;
                    case 5:
                        EscribirCsv(FiltrarPorAnio(LeerXml(rutaXmlOrigen)), rutaCsvDestino);
                        break;
                    case 6:
                        EscribirCsv(FiltrarPorLista(LeerXml(rutaXmlOrigen), 5), rutaCsvDestino);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } catch (Exception ex){
                Console.WriteLine("Error: " + ex.Message);
            }
        } while (opcion != 0);
    }

    static List<string[]> LeerCsv(string ruta)
    {
        List<string[]> peliculas = new List<string[]>();
        string[] lineas = File.ReadAllLines(ruta);

        // Empezamos en 1 para saltarnos las cabeceras
        for (int i = 1; i < lineas.Length; i++)
        {
            peliculas.Add(SepararCampos(lineas[i]));
        }

        return peliculas;
    }

    // No vale Split(',') porque hay campos entre comillas con comas dentro
    static string[] SepararCampos(string linea)
    {
        string[] campos = new string[6];
        int numCampo = 0;
        bool dentroComillas = false;
        string actual = "";

        foreach (char c in linea)
        {
            if (c == '"')
            {
                dentroComillas = !dentroComillas;
            }
            else if (c == ',' && !dentroComillas)
            {
                campos[numCampo] = actual;
                numCampo++;
                actual = "";
            }
            else
            {
                actual += c;
            }
        }
        campos[numCampo] = actual;

        return campos;
    }

    static void EscribirXml(List<string[]> peliculas, string ruta)
    {
        XmlWriter xmlWriter = XmlWriter.Create(ruta);

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("Peliculas");

        foreach (string[] pelicula in peliculas)
        {
            xmlWriter.WriteStartElement("Pelicula");

            for (int i = 0; i < cabeceras.Length; i++)
            {
                xmlWriter.WriteStartElement(cabeceras[i]);
                xmlWriter.WriteString(pelicula[i]);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();

        Console.WriteLine("Archivo XML creado.");
    }

    static List<string[]> LeerXml(string ruta)
    {
        List<string[]> peliculas = new List<string[]>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(ruta);

        foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
        {
            string[] campos = new string[6];

            for (int i = 0; i < 6; i++)
            {
                campos[i] = xmlNode.ChildNodes[i].InnerText;
            }

            peliculas.Add(campos);
        }

        return peliculas;
    }

    static void EscribirCsv(List<string[]> peliculas, string ruta)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Title,Genre,Premiere,Runtime,IMDB Score,Language");

        foreach (string[] pelicula in peliculas)
        {
            string linea = "";

            for (int i = 0; i < pelicula.Length; i++)
            {
                string campo = pelicula[i];

                if (campo.Contains(","))
                {
                    campo = "\"" + campo + "\"";
                }

                if (i > 0)
                {
                    linea += ",";
                }
                linea += campo;
            }

            sb.AppendLine(linea);
        }

        File.WriteAllText(ruta, sb.ToString());
        Console.WriteLine("Archivo CSV creado.");
    }

    static List<string[]> FiltrarPorLista(List<string[]> peliculas, int columna)
    {
        List<string> valores = new List<string>();

        foreach (string[] pelicula in peliculas)
        {
            if (!valores.Contains(pelicula[columna]))
            {
                valores.Add(pelicula[columna]);
            }
        }

        for (int i = 0; i < valores.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {valores[i]}");
        }

        Console.Write("Elige un número: ");
        int eleccion = int.Parse(Console.ReadLine());
        string elegido = valores[eleccion - 1];

        List<string[]> resultado = new List<string[]>();
        foreach (string[] pelicula in peliculas)
        {
            if (pelicula[columna] == elegido)
            {
                resultado.Add(pelicula);
            }
        }

        return resultado;
    }

    static List<string[]> FiltrarPorAnio(List<string[]> peliculas)
    {
        Console.Write("Introduce un año: ");
        int anio = int.Parse(Console.ReadLine());

        List<string[]> resultado = new List<string[]>();
        foreach (string[] pelicula in peliculas)
        {
            // El año son los 4 últimos caracteres de la fecha
            string fecha = pelicula[2];
            int anioPelicula = int.Parse(fecha.Substring(fecha.Length - 4));

            if (anioPelicula >= anio)
            {
                resultado.Add(pelicula);
            }
        }

        return resultado;
    }
}
