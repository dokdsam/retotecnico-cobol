using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoTecnicoCobol
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creación de variables locales
            decimal TotalDebito = 0;
            decimal TotalCredito = 0;
            decimal BalanceFinal = 0;
            string Cod_TransaccionMayor = "";
            decimal Num_TransaccionMayor = 0;
            decimal Num_TransaccionLeida = 0;     
            int NroTR_Debito = 0;
            int NroTR_Credito = 0;

            //Lectura del archivo CSV
            string RutaArchivo = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}/Data/Ventas.csv";
            string[] fileDatos = File.ReadAllLines(RutaArchivo);

            //Inicio de lectura de las líneas de datos
            foreach (var fila in fileDatos)
            {
                //Se separan los datos por el caracter específico
                var dato = fila.Split(';');

                if (dato[0] != "id") //Se valida que no lea la primera línea
                {
                    //Se toma como valor el monto en lectura
                    Num_TransaccionLeida = Convert.ToDecimal(dato[2]);

                    //Se valida si es el monto mayor hasta el momento
                    if (Num_TransaccionLeida > Num_TransaccionMayor)
                    {
                        Cod_TransaccionMayor = dato[0];
                        Num_TransaccionMayor = Num_TransaccionLeida;
                    }
                    
                    //Se hace una validación por tipo de datos de la venta
                    switch (dato[1])
                    {
                        case "Débito":
                            TotalDebito = TotalDebito + Convert.ToDecimal(dato[2]);
                            NroTR_Debito = NroTR_Debito + 1;

                            break;
                        case "Crédito":
                            TotalCredito = TotalDebito + Convert.ToDecimal(dato[2]);
                            NroTR_Credito = NroTR_Credito + 1;

                            break;
                        default:
                            break;
                    }
                }
            }

            //Se calcula el balance final
            BalanceFinal = TotalCredito - TotalDebito;

            //Se imprimen los datos
            Console.WriteLine("Reporte de Transacciones");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Balance Final: {BalanceFinal}");
            Console.WriteLine($"Transacción de Mayor Monto: ID {Cod_TransaccionMayor} - {Num_TransaccionMayor}");
            Console.WriteLine($"Conteo de Transacciones: Crédito: {NroTR_Credito} Débito: {NroTR_Debito}");

            //Se deja abierta la consola para lectura
            Console.Read();
        }
    }
}
