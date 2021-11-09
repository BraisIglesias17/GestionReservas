using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using gestionReservas.core;
using gestionReservas.core.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gestionReservas.UI
{
    public partial class MainWindow : Window
    {
        private List<Cliente> c;
        private List<Habitacion> h;
        Cliente c1=new Cliente("00000000h");
        Cliente c2=new Cliente("44235500l");
        Habitacion h1=new Habitacion(1);
       Habitacion h2= new Habitacion(2);
        private RegistroReservas listaReservas;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            try
            {
                listaReservas = this.OnLoad("reservas.txt");
            }
            catch (Exception exc)
            {
                listaReservas = new RegistroReservas();
            }
            List<Cliente> clientes = new List<Cliente>();
                List<Habitacion> habitacions = new List<Habitacion>();
                clientes.Add(c1);
                clientes.Add(c2);
                habitacions.Add(h1);
                habitacions.Add(h2);
                

                this.c = clientes;
                this.h = habitacions;
                var opInsertar = this.FindControl<MenuItem>("OpInsert");
                var dtReservas = this.FindControl<DataGrid>("DtReservas");
                var opGuardar = this.FindControl<MenuItem>("OpGuardar");
                var opModificar = this.FindControl<Button>("btModificar");
                var opEliminar = this.FindControl<Button>("btEliminar");
                var opSalir = this.FindControl<MenuItem>("OpExit");
                dtReservas.Items = this.listaReservas;
                
            
                opGuardar.Click += (_, _) => this.OnSave("reservas.txt");
                opInsertar.Click += (_, _) => this.OnInsert();
                opEliminar.Click += (_, _) => this.OnDelete(dtReservas.SelectedIndex);
                opModificar.Click += (_, _) => this.OnModify(dtReservas.SelectedIndex);
                opSalir.Click += (_, _) => this.OnExit();

        }

        private void OnExit()
        {
            this.Close();
        }

        private async void OnDelete(int position)
        {
            if (position != -1)
            {
                try
                { //async awaait
                    GeneralMessage dialog = new GeneralMessage("Seguro que deseas eliminar esta reserva", true);
                    await dialog.ShowDialog(this);
                    if (!dialog.IsCancelled)
                    {
                        this.listaReservas.RemoveAt(position);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
            
        }

        private void OnModify(int position)
        {
            if (position != -1)
            {
                //new InsertarReserva(this.listaReservas,this.listaReservas[position],this.c,this.h).ShowDialog(this);
                new InsertarReserva(this.listaReservas,position,this.c,this.h).ShowDialog(this);
            }
            else
            {
                new GeneralMessage("Debes seleccionar una fila antes",false).Show();
            }
        }

        private void OnInsert()
        { 
            //new InsertarReserva(this.listaReservas,this.c,this.h,c1,h1).ShowDialog(this);
            new InsertarReserva(this.listaReservas,this.c,this.h).ShowDialog(this);
        }
        
        private void OnSave(string nf)
        {
            XmlRegistroReservas toXml = new XmlRegistroReservas(this.listaReservas);
            toXml.Guardar(nf);
        }
        
        private RegistroReservas OnLoad(string nf)
        {
            return XmlRegistroReservas.RecuperarXML(nf); //toXml = new XmlRegistroReservas(this.listaReservas);
            
        }
        private void OnUpdateCount()
        {
            var count = this.FindControl<Label>("LbNumReservas");
            count.Content=this.listaReservas.Length.ToString();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ModificarReserva()
        {
            
        }

        private void EliminarReserva()
        {
            
        }
    }
}