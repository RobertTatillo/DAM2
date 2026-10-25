using System;
using System.IO;

class Program
{
    // Ruta del fichero con el que estamos trabajando (se rellena en la opción 1)
    static string rutaArchivo = "";

    static void Main(string[] args)
    {
        int opcion = 0;

        do{
            Console.WriteLine("\n----- MENÚ -----");
            Console.WriteLine("1. Crear un nuevo fichero");
            Console.WriteLine("2. Añadir registros en el fichero");
            Console.WriteLine("3. Eliminar todos los registros del fichero");
            Console.WriteLine("4. Eliminar el fichero");
            Console.WriteLine("5. Visualizar los registros");
            Console.WriteLine("6. Cerrar la aplicación");
            Console.Write("Elige una opción: ");

            try{
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        CrearFichero();
                        break;
                    case 2:
                        AnadirRegistro();
                        break;
                    case 3:
                        EliminarRegistros();
                        break;
                    case 4:
                        EliminarFichero();
                        break;
                    case 5:
                        VisualizarRegistros();
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

    // 1. Crear un nuevo fichero con el nombre que el usuario pasa por consola
    static void CrearFichero()
    {
        Console.Write("Nombre del fichero (por ejemplo datos.txt): ");
        rutaArchivo = "./" + Console.ReadLine();

        FileStream archivo = File.Create(rutaArchivo);
        archivo.Close();
        Console.WriteLine("**Archivo creado exitosamente.**");
    }

    // 2. Añadir registros en el fichero
    static void AnadirRegistro()
    {
        Console.Write("Escribe el registro: ");
        string registro = Console.ReadLine();

        // Guardamos el contenido en una lista
        List<string> lineas = File.ReadAllLines(rutaArchivo).ToList();

        // Añadimos el nuevo contenido a la lista
        lineas.Add(registro);

        // Sobrescribimos el archivo
        File.WriteAllLines(rutaArchivo, lineas);
        Console.WriteLine("Registro añadido.");
    }

    // 3. Eliminar todos los registros del fichero
    static void EliminarRegistros()
    {
        // Si el fichero ya existe, File.Create lo sobrescribe y lo deja vacío
        FileStream archivo = File.Create(rutaArchivo);
        archivo.Close();
        Console.WriteLine("Registros eliminados.");
    }

    // 4. Eliminar el fichero
    static void EliminarFichero()
    {
        File.Delete(rutaArchivo);
        Console.WriteLine("Fichero eliminado.");
    }

    // 5. Visualizar los registros
    static void VisualizarRegistros()
    {
        String[] lineas = File.ReadAllLines(rutaArchivo).ToArray();
        foreach (String s in lineas)
        {
            Console.WriteLine(s);
        }
    }
}
