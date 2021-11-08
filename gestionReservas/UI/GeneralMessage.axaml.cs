using System;
using System.Collections.Generic;
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
    public partial class GeneralMessage : Window
    {
        private bool confirmacion=false;
        
        public GeneralMessage(string mensaje,bool confirmacion):this()
        {
            var texBlock = this.FindControl<TextBlock>("AboutText");
            texBlock.Text=mensaje;
            this.confirmacion = confirmacion;
            
            var boton = this.FindControl<Button>("salirAyuda");
            if (confirmacion)
            {
                boton.IsVisible = false;
                var btAceptar = this.FindControl<Button>("btAceptar");
                var btCancelar = this.FindControl<Button>("btCancelar");
                btAceptar.IsVisible = true;
                btCancelar.IsVisible = true;
                btAceptar.Click += (_, _) => this.OnAccept();
                btCancelar.Click += (_, _) => this.OnCancel();
                
            }
            else
            {
                boton.Click += (_, _) => this.Close();
            }


        }
        
        
        public GeneralMessage()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            
           

        }

        private void OnCancel()
        {
            this.Close();
        }

        private void OnAccept()
        {
            
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}