using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionReservas.core;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gestionReservas.UI
{
    public partial class VisualizacionFactura : Window
    {
        
        
        
        public VisualizacionFactura(string mensaje, int id):this()
        {
            this.Title = "Factura de reserva "+id;
            var tbFactura = this.FindControl<TextBlock>("tbFactura");
            tbFactura.Text = mensaje;


            var btGuardar = this.FindControl<Button>("btGuardarFactura");
            btGuardar.Click += (_, _) => this.OnSave(mensaje,id);
        }

        private void OnSave(string mensaje, int id)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor 
                string nombreArchivo = "\\"+id + "_factura.txt";
                String path=System.Environment.GetEnvironmentVariable("USERPROFILE");
                StreamWriter sw = new StreamWriter(path+nombreArchivo);
                
                string[] array = mensaje.Split("\n");

                
                foreach (var linea in array)
                {
                    sw.WriteLine(linea);
                }
                //Close the file
                sw.Close();
                
            }
            catch(Exception e)
            {
                new GeneralMessage(e.Message,false).Show();
            }
        }


        public VisualizacionFactura()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        
    }
}