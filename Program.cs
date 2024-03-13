using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace ConexionTCPcliente
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //configuraciones para conectarse con el servirod

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("192.168.0.3"), 6400);
            try
            {
                //se crea un soocket para enviar datos
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //Al socket le indicamos conectarse con el servidor
                sender.Connect(remoteEP);
                //mensaje de confirmacion de conexion
                Console.WriteLine("Conectado con el servidor....");
                //pedimos al usuario que ingrese la onformacion
                string comando = "";
                //aqui pedimos ingresar hasta que se preione exit pero solo sale el cliente
                while (comando != "exit")
                {


                    comando = Console.ReadLine();
                    //se convierte el texto en un arreglo de bytes
                    byte[] msg = Encoding.ASCII.GetBytes(comando + "<EOF>");
                    //enviar al servidor el mensaje 
                    int byteSent = sender.Send(msg);
                    //recibimos respuesta del servidor
                    byte[] bytes = new byte[1024];
                    int bytesRec = sender.Receive(bytes);
                    string texto = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Servirdor: " + texto);



                }
                Console.ReadKey();




                //cerramos la conexion con el servidor 
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }





    }
}