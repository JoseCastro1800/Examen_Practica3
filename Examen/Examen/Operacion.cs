using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Examen
{
    public class Operacion
    {    //lista publica para poder utilizarla mas adelante en unos metodos
        public List<Perros> Perros;

        //Se le llama desde el main
        internal void Principal()
        {
            Console.WriteLine("Bienvenido al programa de perritos");
            Console.ReadKey();
            Menu();
        }
        public Operacion()
        {   //Lista publica que trabaja con el metodo
            Perros = ObtenerPerros();
        }
 

        //Metodo para que el usuario eliga una opcion y dependeindo de la opcion hara una accion
        public void Menu()
        {    //Despliegue de la lista de objetos  
            try
            {  Console.WriteLine("\n\nFavor de escoger alguna opcion que desea : \n1.- Listas de los perros.\n2.- Salida.");
                //Se captura sin necesidad de crear una variable
                switch (int.Parse(Console.ReadLine()))
                {
                    //Cuando el usuario detalle lo mandara al metodo
                    case 1:
                        Console.Clear();
                        //Se accede al detallado
                        DetallePerro();
                        break;
                    case 2:
                        //Por si el usuario se quiere ir
                        System.Environment.Exit(0);
                        break;
                        //Un default por si se llega a equivocar 
                    default:
                        Console.WriteLine("Escoga una opcion correcta man");
                        Console.ReadKey();
                        Console.Clear();
                        Menu();
                        break;
                }

            }
            //Solo para pedir numeros
            catch (Exception ex) 
            {
                Console.Clear();
                Console.WriteLine("oh no de nuevo amigo, escoga una opcion correcta por favor");
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        //Despliegue de la lista publica 
        public void MostrarPerros() 
        {
            Console.WriteLine("Estos son los perros de la lista: ");
            //foreach para cada elemento de la lista
            foreach (var item in Perros)
            {
                //Se despliega el todos los atributos para saber que informacion tiene el perro
                Console.WriteLine("{0}.- {1}.-{2}.-{3}.-{4}", item.Id, item.Nombre, item.Comida,item.Raza, item.Genero); 
            }
        }
        //Se obtiene la informacion del archivo txt
        public List<string> ObtenerLineas(string path)
        {
            //Creacion de la lista
            List<string> lineas = new List<string>();
            //Se busca el file por si existe
            if (File.Exists(path))
            {   //De esta manera se saca el jugo de la informacion
                string[] datos = File.ReadAllLines(path);
                //Busqueda mediante del foreach para buscar el array
                foreach (var item in datos)
                {   //Agregacion a la nueva lista por cada array
                    lineas.Add(item);
                }
            }
            else
            {
                //Si no se encuentra el archivo este envia mensaje de que no existe(Murio)
                Console.WriteLine("El archivo murio");
                return null;
            }
            //Se regresa al metodo cuando la lista de string esta llena
            return lineas;
        }
        public List<Perros> ObtenerPerros()
        {   //Se instancia la clase perro 
            Perros p = new Perros();
            //Optimizacion de lista con un var
            var lineas = ObtenerLineas("Perritos.txt");
            //Creacion de la lista de objetos
            List<Perros> perros = new List<Perros>();
            //Busqueda en la lista de string mediante un foreach
            foreach (var item in lineas)
            {   //Por cada uno de la lista se crea un arreglo
                string[] datos = item.Split(',');
                perros.Add(new Perros { Id = int.Parse(datos[0]), Nombre = datos[1], Comida = datos[2], Raza = datos[3], Genero = datos[4] });//cada que llenes tu arreglo de 5 elementos, los conviertes en atributos del objeto y los agregas a la lista 
            }
            //Se regresa la lista llena con objetos 
            return perros;
        }
        //Detalles
        public void DetallePerro() 
        {
            //Por si el compa se equivoca 
            try
            {   
                Console.Clear();
                //Mostrar la lista para saber que elegira el usuario
                MostrarPerros();
                //Se instancia y se despliega el objeto
                Perros p = new Perros();
                Console.WriteLine("Seleccione un perrito para poder modificar sus detalles ");
                //Con el id se busca al perro
                int perroid = int.Parse(Console.ReadLine());
                //Busqueda implacable con un foreach
                foreach (var item in Perros)
                {
                    //Si lo que se busca esta se convierte en un objeto y este lo arrojara
                    if (perroid == item.Id)
                    {
                        p = item;
                    }
                }

                //Despliegue de los atributos
                Console.Clear();
                Console.WriteLine("Nombre:  {0}\nComida:  {1}\nRaza:  {2}\nGenero:  {3}", p.Nombre, p.Comida, p.Raza, p.Genero);
               
                Console.WriteLine("\nSi quiere cambiar algo solo de click en 1, si desea volver a lista man de click en 2.");
                //Se le da la opcion de que el usuario quiera editar el objeto y se va a la goma 
                int option = int.Parse(Console.ReadLine());
                //Mediante un if por si el usario elige editar este te mandara al metodo edicion perro
                if (option == 1)
                {
                    p = EdicionPerro(p);
                    Console.Clear();
                    Menu();
                }
                //Por si no quiere editar se va a la goma xd
                else
                {
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
            }
            //En caso de una exepcion se interrumpe la edicion pr si tiene un error de sintaxis
            catch (Exception e) 
            {
                Console.WriteLine("uy no valio kaka: {0}\nPiquele a algo para volver al principio", e.Message);
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        //Edicion del objeto cuando se acepta
        public Perros EdicionPerro(Perros p) 
        {    //Por si hay error de sintaxis
            try
            {
                Console.WriteLine("Escoga algun atributo a modificar man:\n1.-Nombre\n2.-Comida\n3.-Raza\n4.-Genero");
                //Se selecciona el atributo a desear
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        //Se ingresa el nuevo atributo asi como en los demas
                        Console.WriteLine("Ingrese el nuevo nombre: ");
                        string nombre = Console.ReadLine();
                        p.Nombre = nombre;
                        //Manda mensaje de qeu se modifico correctamente
                        Console.WriteLine("Se ha modificado el Nombre.");
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la nueva comida: ");
                        string comida = Console.ReadLine();
                        p.Comida = comida;
                        Console.WriteLine("Se ha modificado la comida");
                        break;
                    case 3:
                        Console.WriteLine("Ingrese la raza");
                        string raza = Console.ReadLine();
                        p.Raza = raza;
                        Console.WriteLine("Se ha modificado la raza");
                        break;
                    case 4:
                        Console.WriteLine("Ingrese el nuevo genero: ");
                        string genero = Console.ReadLine();
                        p.Genero = genero;
                        Console.WriteLine("Se ha modificado el genero.");
                        break;
                    default:
                        
                        break;
                }
                //Al dar enter este te regresa al menu y se actualiza la lista
                Console.WriteLine("Presione ENTER para continuar");
                ActualizacionTxt();
                Console.ReadKey();
                return p;
            }
             

            //Por si hay un error de sintaxis este te devolvera sin ningun cambio 
            catch (Exception exe) 
            {
                Console.WriteLine("otra vez man?: {0}\nDe regreso al principio por feo", exe.Message);
                Menu();
                return p;
            }
        }
        //Modificacion de la lista
        //Es cuando se crea la lista en forma global pero de forma contraria en vez de usarse split se uso join
        public void ActualizacionTxt()
        {//Se hace una lista en la cual se usa una Fusiooon
            List<string> lineas = new List<string>();
            foreach (var item in Perros) 
            {    // Se llena un vector de string, esto es para que se llenen los atributos
                //Entonces aqui se actualiza la lista por cada nuevo objeto
                string[] Nuevo = new string[5];
                Nuevo[0] = Convert.ToString(item.Id);
                Nuevo[1] = item.Nombre;
                Nuevo[2] = item.Comida;
                Nuevo[3] = item.Raza;
                Nuevo[4] = item.Genero;
                lineas.Add(string.Join(",", Nuevo));
            }
            //Ahora se utiliza otro join ya que se fusionen las listas, se usa el blackash ya que esto hace una division y es un reglon a lo que se refiere
            var joinedstring = string.Join("\n", lineas);
            //Ya por ultimo todo se pasa a la lista Perritos a el archivo txt
            File.WriteAllText("Perritos.txt", joinedstring);
        }
    }
}
