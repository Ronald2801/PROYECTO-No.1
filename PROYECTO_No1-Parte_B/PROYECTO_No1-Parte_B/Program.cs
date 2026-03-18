using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_No1_Parte_B
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----- SISTEMA SMARTPARK -----");
            Console.ResetColor();

            // Variables del sistema
            string operador;
            string codigoTurno;
            int capacidad = 0;
            string placa;
            int tipoVehiculo = 0;
            string nombreCliente;
            bool clienteVIP = false;
            int minutoEntrada = 0;

            // REGISTRO INICIAL

            Console.Write("Nombre del operador: ");
            operador = Console.ReadLine();

            // Validación del código de turno (4 caracteres)
            do
            {
                Console.Write("Código de turno (4 caracteres): ");
                codigoTurno = Console.ReadLine();

                if (int.Parse(codigoTurno) < 1000 || int.Parse(codigoTurno) > 9999)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("ERROR: El código debe tener exactamente 4 digitos.");
                    Console.WriteLine("--------------------------------------------------");
                    Console.ResetColor();
                }

            } while (int.Parse(codigoTurno) < 1000 || int.Parse(codigoTurno) > 9999);

            // Validación de capacidad mínima
            do
            {
                Console.WriteLine();
                Console.Write("Capacidad del parqueo (mínimo 10): ");
                capacidad = int.Parse(Console.ReadLine());

                if (capacidad < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ERROR: La capacidad mínima es 10.");
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }

            } while (capacidad < 10);


            // Variables de tikcet y estadísticas

            int ticketsCreados = 0;
            int ticketsCerrados = 0;
            double dineroRecaudado = 0;
            int tiempoActual = 0;
            bool ticketActivo = false;

            // MENÚ PRINCIPAL  

            int opcion = 0;

            do
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----- MENU PRINCIPAL -----");
                Console.ResetColor();

                Console.WriteLine("1. Crear ticket de entrada");
                Console.WriteLine("2. Registrar salida y calcular cobro");
                Console.WriteLine("3. Ver estado del parqueo");
                Console.WriteLine("4. Simular paso del tiempo");
                Console.WriteLine("5. Salir");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("--------------------------");
                Console.ResetColor();

                Console.WriteLine();
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());
                Console.WriteLine();


                // OPCIÓN 1: CREAR TICKET

                if (opcion == 1)
                {
                    if (ticketActivo == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("ERROR: Ya existe un ticket activo.");
                        Console.WriteLine("----------------------------------");
                        Console.ResetColor();
                    }
                    else if ((ticketsCreados - ticketsCerrados) >= capacidad)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("ERROR: El parqueo está lleno.");
                        Console.WriteLine("-----------------------------");
                        Console.ResetColor();
                    }
                    else
                    {
                        // Validación de placa
                        do
                        {
                            Console.Write("Placa (6 a 8 caracteres): ");
                            placa = Console.ReadLine();

                            if (placa.Length < 6 || placa.Length > 8 || placa.Contains(" "))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("----------------------");
                                Console.WriteLine("ERROR: Placa inválida.");
                                Console.WriteLine("----------------------");
                                Console.ResetColor();
                            }

                        } while (placa.Length < 6 || placa.Length > 8 || placa.Contains(" "));

                        // Tipo de vehículo
                        do
                        {
                            Console.WriteLine("Tipo de vehículo:");
                            Console.WriteLine("1. Moto");
                            Console.WriteLine("2. Auto");
                            Console.WriteLine("3. Pickup/SUV");
                            Console.Write("Seleccione: ");

                            tipoVehiculo = int.Parse(Console.ReadLine());

                            if (tipoVehiculo < 1 || tipoVehiculo > 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("---------------------");
                                Console.WriteLine("ERROR: Tipo inválido.");
                                Console.WriteLine("---------------------");
                                Console.ResetColor();
                            }

                        } 
                        
                        while (tipoVehiculo < 1 || tipoVehiculo > 3);

                        //datos del cliente

                        Console.Write("Nombre del cliente: ");
                        nombreCliente = Console.ReadLine();

                        Console.Write("¿Cliente VIP? (si [1]/no [0]): ");
                        int vip = int.Parse(Console.ReadLine());

                        if (vip == 1)
                        {     
                            clienteVIP = true; 
                        }

                        // Registrar minuto de entrada
                        minutoEntrada = tiempoActual;

                        ticketActivo = true;
                        ticketsCreados++;

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Ticket creado correctamente.");
                        Console.WriteLine("----------------------------");
                        Console.ResetColor();
                    }
                }


                // OPCIÓN 2: REGISTRAR SALIDA

                else if (opcion == 2)
                {
                    if (ticketActivo == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("ERROR: No existe ticket activo.");
                        Console.WriteLine("-------------------------------");
                        Console.ResetColor();
                    }
                    else
                    {
                        //variables para cálculo de cobro
                        int minutosEstacionado = tiempoActual - minutoEntrada;
                        double tarifa = 0;
                        double subtotal = 0;
                        double multa = 0;
                        double descuento = 0;
                        double total = 0;

                        // Determinar tarifa por tipo de vehículo
                        if (tipoVehiculo == 1) tarifa = 5;
                        if (tipoVehiculo == 2) tarifa = 10;
                        if (tipoVehiculo == 3) tarifa = 15;

                        //no cobrar si el tiempo es menor o igual a 15 minutos
                        if (minutosEstacionado <= 15)
                        {
                            total = 0;
                        }
                        else
                        {
                            double horas = minutosEstacionado / 60.0;
                            int horasCobro = (int)Math.Ceiling(horas);

                            subtotal = horasCobro * tarifa;

                            if (minutosEstacionado > 360)
                            {
                                multa = 25;
                            }

                            if (clienteVIP == true)
                            {
                                descuento = subtotal * 0.10;
                            }

                            total = subtotal + multa - descuento;
                        }

                        dineroRecaudado += total;
                        ticketsCerrados++;
                        ticketActivo = false;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Total a pagar: Q" + total);
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }

  
                // OPCIÓN 3: ESTADO DEL PARQUEO

                else if (opcion == 3)
                {
                    // Calcular espacios ocupados y disponibles
                    int ocupados = ticketsCreados - ticketsCerrados;
                    int disponibles = capacidad - ocupados;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("----- ESTADO DEL PARQUEO -----");
                    Console.ResetColor();

                    Console.WriteLine("Capacidad total: " + capacidad);
                    Console.WriteLine("Espacios ocupados: " + ocupados);
                    Console.WriteLine("Espacios disponibles: " + disponibles);
                    Console.WriteLine("Tiempo simulado: " + tiempoActual + " minutos");
                    Console.WriteLine("Total recaudado: Q" + dineroRecaudado);
                    Console.WriteLine("Tickets creados: " + ticketsCreados);
                    Console.WriteLine("Tickets cerrados: " + ticketsCerrados);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("------------------------------");
                    Console.ResetColor();
                    Console.WriteLine();
                }


                // OPCIÓN 4: SIMULAR TIEMPO

                else if (opcion == 4)
                {
                    int minutos = 0;

                    do
                    {
                        Console.Write("Minutos a simular (1-1440): ");
                        minutos =int.Parse(Console.ReadLine());

                        if (minutos < 1 || minutos > 1440)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------");
                            Console.WriteLine("ERROR: rango inválido.");
                            Console.WriteLine("----------------------");
                            Console.ResetColor();
                        }

                    } while (minutos < 1 || minutos > 1440);

                    tiempoActual += minutos;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tiempo actualizado: " + tiempoActual + " minutos.");
                    Console.ResetColor();
                }


                // OPCIÓN 5: SALIR

                else if (opcion == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("----- RESUMEN DEL TURNO -----");
                    Console.ResetColor();

                    Console.WriteLine("Operador: " + operador);
                    Console.WriteLine("Código de turno: " + codigoTurno);
                    Console.WriteLine("Tickets creados: " + ticketsCreados);
                    Console.WriteLine("Tickets cerrados: " + ticketsCerrados);
                    Console.WriteLine("Total recaudado: Q" + dineroRecaudado);
                    Console.WriteLine("Tiempo simulado: " + tiempoActual + " minutos");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("-----------------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("--------------------");
                    Console.WriteLine("PROGRAMA FINALIZADO.");
                    Console.WriteLine("--------------------");
                    Console.ResetColor();
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("ERROR: opción inválida.");
                    Console.WriteLine("-----------------------");
                    Console.ResetColor();
                }

            } while (opcion != 5);

        }
    }
}
