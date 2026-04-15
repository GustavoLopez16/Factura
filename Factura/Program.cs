using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;


namespace Factura
{
    class Program
    {
        public static void Main()
        {

            DetalleFactura martillo = new DetalleFactura("Martillo", 8.50M, 2);
            DetalleFactura clavos = new DetalleFactura("Clavos 1Kg", 3.25M, 5);
            DetalleFactura serrucho = new DetalleFactura("Serrucho", 22.99M, 1);

            Factura factura1 = new Factura(1, "Construcciones Pérez S.A.");
            factura1.AgregarDetalle(martillo);
            factura1.AgregarDetalle(serrucho);
            factura1.AgregarDetalle(clavos);

            factura1.MostrarFactura();
            factura1.ModificarProducto("Martillos");
            factura1.MostrarFactura();

        }

        public class DetalleFactura
        {
            public string Descripcion { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int Cantidad { get; set; }

            public DetalleFactura(string descripcion, decimal precioUnitario, int cantidad)
            {
                Descripcion = descripcion;
                PrecioUnitario = precioUnitario;
                Cantidad = cantidad;

            }


            public decimal CalcularSubtotal()
            {
                return PrecioUnitario * Cantidad;

            }

        }

        class Factura
        {
            private int NumeroFactura { get; set; }
            private DateTime Fecha { get; set; } = DateTime.Now;
            private string Cliente { get; set; }

            private List<DetalleFactura> ListaProductos;

            public Factura(int numeroFactura, string cliente)
            {

                NumeroFactura = numeroFactura;
                Fecha = DateTime.Now;
                Cliente = cliente;
                ListaProductos = new List<DetalleFactura>();
            }

            public void AgregarDetalle(DetalleFactura detalleFactura)
            {
                ListaProductos.Add(detalleFactura);
            }




            public void ModificarProducto(string descripcion)
            {
                bool encontrado = false;
                foreach (var des in this.ListaProductos)
                {
                    if (des.Descripcion == descripcion)
                    {
                        Console.WriteLine($"Ingrese El Nuevo Precio: ");
                        des.PrecioUnitario = decimal.Parse(Console.ReadLine());
                        Console.WriteLine($"Ingrese La Cantidad: ");
                        des.Cantidad = int.Parse(Console.ReadLine());
                        encontrado = true;
                        break;
                    }
                   
                    
                    
                }
                if (!encontrado)
                {
                    Console.WriteLine("No Se Encontro El Producto Solicitado");
                }



            }

            public decimal CalcularSubtotalGeneral()
            {
                decimal suma = 0;
                foreach (var des in this.ListaProductos)
                {
                    suma += des.CalcularSubtotal();

                }
                return suma;
            }

            public decimal CalcularIva()
            {

                return this.CalcularSubtotalGeneral() * 0.16M;

            }

            public decimal CalcularTotal()
            {
                return this.CalcularSubtotalGeneral() + this.CalcularIva();

            }

            public void MostrarFactura()
            {

                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Numero Factura: {this.NumeroFactura}\nFecha: {this.Fecha}\nNombre del Cliente: {this.Cliente}");
                foreach (var des in this.ListaProductos)
                {

                    Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
                    Console.WriteLine($"Nombre del producto: {des.Descripcion}\nPrecio Unitario: {des.PrecioUnitario}$\nCantidad:{des.Cantidad}\nSubtotal: {des.CalcularSubtotal()}$");


                }
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - ");

                Console.WriteLine($"Subtotal: {Math.Round(this.CalcularSubtotalGeneral(), 2)}$\nIva(16%): {Math.Round(this.CalcularIva(), 2)}$\nTotal: {Math.Round(this.CalcularTotal(), 2)}$");
                Console.WriteLine("---------------------------------------");
            }

        }

    }

}



