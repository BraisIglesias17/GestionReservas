using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using gestionReservas.core;

namespace gestionReservas.UI
{
    public partial class InsertarReserva : Window
    {

        
        
        private RegistroReservas reservas;
        private Cliente _cliente;
        private Habitacion _habitacion;
        private List<Cliente> _clientes;
        private List<Habitacion> _habitaciones;
        //private Reserva r;
        private int position;
        public InsertarReserva(RegistroReservas reservas,List<Cliente> clientes, List<Habitacion> habitaciones,Cliente cliente, Habitacion habitacion):this()
        {
            this.position = -1;
            this.reservas = reservas;
            this._cliente = cliente;
            this._habitacion = habitacion;
            this._clientes = clientes;
            this._habitaciones = habitaciones;
            this.renderTemplate();
            this.drawButton("Insertar"); 
        }
        
        public InsertarReserva(RegistroReservas reservas, List<Cliente> clientes, List<Habitacion> habitaciones):this()
        {
            this.position = -1;
            this.reservas = reservas;
            this._clientes = clientes;
            this._habitaciones = habitaciones;
            this.renderTemplate();
            this.drawButton("Insertar");
            
        }
        
        //Constructor para modificacion
        public InsertarReserva(RegistroReservas reservas,int position, List<Cliente> clientes,List<Habitacion> habitaciones):this()
        {
            this.reservas = reservas;
            this.position = position;
            this._clientes = clientes;
            this._habitaciones = habitaciones;
            this.fillData();
            this.renderTemplateModificar();
            this.drawButton("Guardar cambios");
            
        }

        private void renderTemplateModificar()
        {
            var cbClientes = this.FindControl<ComboBox>("cbClienteReserva");
            var cbHabitaciones = this.FindControl<ComboBox>("cbHabitacionReserva");

            /*
            cbClientes.Items = this._clientes;
            cbClientes.SelectedItem = this.reservas[this.position].Cliente;
            cbHabitaciones.Items = this._habitaciones;
            cbHabitaciones.SelectedItem = this.reservas[this.position].Habitacion;

            if (this._clientes.Contains(this._cliente))
            {
                Console.Write("");
            }
                
            
            cbClientes.PlaceholderText = this.reservas[this.position].Cliente.Dni;
            cbHabitaciones.PlaceholderText = this.reservas[this.position].Habitacion.NumHabitacion.ToString();*/
            cbClientes.IsVisible = false;
            cbHabitaciones.IsVisible = false;
            
            cbClientes.IsVisible = false;
            var dpClientes = this.FindControl<DockPanel>("dpClientes");
            dpClientes.IsVisible = true;
            var tbDni = this.FindControl<TextBox>("tbDNICliente");
            tbDni.Text = this.reservas[this.position].Cliente.Dni;
            
            
            cbClientes.IsVisible = false;
            var dpHabitaciones = this.FindControl<DockPanel>("dpHabitaciones");
            dpHabitaciones.IsVisible = true;
            var tbNumH = this.FindControl<TextBox>("tbNumHabitacion");
            tbNumH.Text = this.reservas[this.position].Habitacion.NumHabitacion.ToString();

            tbDni.IsEnabled = true;
            tbNumH.IsEnabled = true;


        }


        public InsertarReserva()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            
           

        }

        private void fillData()
        {
            var tipo= this.FindControl<TextBox>("tbTipo");
            var fechaEntrada= this.FindControl<DatePicker>("dpFechaEntrada");
            var fechaSalida= this.FindControl<DatePicker>("dpFechaSalida");
            var iva= this.FindControl<TextBox>("tbIva");
            var garaje= this.FindControl<CheckBox>("cbGaraje");
            var precioDia = this.FindControl<TextBox>("tbImportePorDia");

            tipo.Text = this.reservas[this.position].Tipo;
            fechaEntrada.SelectedDate = this.reservas[this.position].FechaEntrada;
            fechaSalida.SelectedDate = this.reservas[this.position].FechaSalida;
            iva.Text = this.reservas[this.position].IVA.ToString();
            garaje.IsChecked = this.reservas[this.position].UsaGaraje;
            precioDia.Text = this.reservas[this.position].PrecioPorDia.ToString();
            this._cliente = this.reservas[this.position].Cliente;
            this._habitacion = this.reservas[this.position].Habitacion;
        }
        private void drawButton(string op)
        {
            var btInsertarReserva = this.FindControl<Button>("btInsertarReserva");
            btInsertarReserva.Content = op;
            btInsertarReserva.Click += (_,_) => this.OnClick();
        }

        
        /// <summary>
        /// Este metodo establece los combobox de clientes y habitaciones y les establece un valor por defecto si ya vienen referenciados
        /// </summary>
        private void renderTemplate()
        {
            var cbClientes = this.FindControl<ComboBox>("cbClienteReserva");
            var cbHabitaciones = this.FindControl<ComboBox>("cbHabitacionReserva");
            
            if (this._cliente != null)
            {
                cbClientes.IsVisible = false;
                var dpClientes = this.FindControl<DockPanel>("dpClientes");
                dpClientes.IsVisible = true;
                var tbDni = this.FindControl<TextBox>("tbDNICliente");
                tbDni.Text = this._cliente.Dni;
                
            }
            else
            {
                cbClientes.Items = this._clientes;
            }

            if (this._habitacion != null)
            {
                cbHabitaciones.IsVisible = false;
                var dpHabitacoin = this.FindControl<DockPanel>("dpHabitaciones");
                dpHabitacoin.IsVisible = true;
                var tbNum = this.FindControl<TextBox>("tbNumHabitacion");
                tbNum.Text = String.Format("{0}",this._habitacion.NumHabitacion);
            }
            else
            {
                cbHabitaciones.Items = this._habitaciones;
            }
            

        }

        private void OnClick()
        {

            
            //Introducir logica de INSERCION MODIFICACION

            if (this.position == -1)
            {
                this.OnInsert();
            }
            else
            {
                this.OnModify();
            }
                
            
            
            //this.Close();
            
        }

        private void OnModify()
        {
            if (this.CheckIfNull())
            {
                if (this.CheckIfClientExists())
                {
                    if (this.CheckIfHabExists())
                    {
                        var tipo= this.FindControl<TextBox>("tbTipo");
                        var fechaEntrada= this.FindControl<DatePicker>("dpFechaEntrada");
                        var fechaSalida= this.FindControl<DatePicker>("dpFechaSalida");
                        var iva= this.FindControl<TextBox>("tbIva");
                        var garaje= this.FindControl<CheckBox>("cbGaraje");
                        var precioDia = this.FindControl<TextBox>("tbImportePorDia");

                        Cliente cliente = this.GetCliente();
                        Habitacion habitacion = this.GetHabitacion();

                        DateTime fEntrada = fechaEntrada.SelectedDate.Value.DateTime;
                        DateTime fSalida = fechaSalida.SelectedDate.Value.DateTime;
                        bool hayGaraje = (bool) garaje.IsChecked;

                        if (this.CheckNumberInteger(iva.Text) && this.CheckNumberDouble(precioDia.Text))
                        {
                            Reserva toAdd = new Reserva(cliente,habitacion,fEntrada,fSalida,Int32.Parse(iva.Text),hayGaraje,Double.Parse(precioDia.Text),tipo.Text );
                            
                            this.reservas.RemoveReserva(this.reservas[this.position]);
                            this.reservas.AddReserva(toAdd);
                            this.Close();
                        }
                        else
                        {
                            new GeneralMessage("Error en algun dato",false).Show();
                        }
                    }
                    else
                    {
                        new GeneralMessage("El numero de habitación no existe",false).Show();
                    }
                }
                else
                {   
                    new GeneralMessage("No existe un usuario con ese DNI",false).Show();
                }
            }
            else
            {
                new GeneralMessage("Algún campo esta vacío",false).Show();
            }
        }

        private bool CheckIfHabExists()
        {
            bool toret = false;
            var tbHab = this.FindControl<TextBox>("tbNumHabitacion");
            int num = Int32.Parse((tbHab.Text));

            foreach (var h in this._habitaciones)
            {
                if (h.NumHabitacion == num)
                {
                    toret = true;
                    break;
                }
            }

            return toret;
        }

        private bool CheckIfClientExists()
        {
            bool toret = false;
            var tbClient = this.FindControl<TextBox>("tbDNICliente");
            string dni = tbClient.Text;

            foreach (var c in this._clientes)
            {
                if (c.Dni == dni)
                {
                    toret = true;
                    break;
                }
            }

            return toret;
        }

        private void OnInsert()
        {
            if (this.CheckIfNull())
            {
                var tipo= this.FindControl<TextBox>("tbTipo");
                var fechaEntrada= this.FindControl<DatePicker>("dpFechaEntrada");
                var fechaSalida= this.FindControl<DatePicker>("dpFechaSalida");
                var iva= this.FindControl<TextBox>("tbIva");
                var garaje= this.FindControl<CheckBox>("cbGaraje");
                var precioDia = this.FindControl<TextBox>("tbImportePorDia");

                Cliente cliente = this.GetCliente();
                Habitacion habitacion = this.GetHabitacion();

                DateTime fEntrada = fechaEntrada.SelectedDate.Value.DateTime;
                DateTime fSalida = fechaSalida.SelectedDate.Value.DateTime;
                bool hayGaraje = (bool) garaje.IsChecked;

                if (this.CheckNumberInteger(iva.Text) && this.CheckNumberDouble(precioDia.Text))
                {
                    Reserva toAdd = new Reserva(cliente,habitacion,fEntrada,fSalida,Int32.Parse(iva.Text),hayGaraje,Double.Parse(precioDia.Text),tipo.Text );

                    this.reservas.AddReserva(toAdd);
                    this.Close();
                }
                else
                {
                    new GeneralMessage("Error en algun dato",false).Show();
                }
            }
            else
            {
                new GeneralMessage("Algún campo esta vacío",false).Show();
            }


        }

        private Cliente GetCliente()
        {
            Cliente toret=null;

            var clientes = this.FindControl<ComboBox>("cbClienteReserva");

            if (this.position == -1)
            {
                if (clientes.IsVisible)
                {
                    toret = (Cliente) clientes.SelectedItem;
                }
                else
                {
                    toret = this._cliente;
                }
            }
            else
            {
                var etClienteDDNI = this.FindControl<TextBox>("tbDNICliente");
                string DNI = etClienteDDNI.Text;
                foreach (var cliente in this._clientes)
                {
                    if (cliente.Dni == DNI)
                    {
                        toret = cliente;
                        break;
                    }   
                }
            }



            return toret;
        }

        private Habitacion GetHabitacion()
        {
            Habitacion toret=null;
    
          
            var habitaciones = this.FindControl<ComboBox>("cbHabitacionReserva");
            
            if (this.position == -1)
            {
                if (habitaciones.IsVisible)
                {
                    toret = (Habitacion) habitaciones.SelectedItem;
                }
                else
                {
                    toret = this._habitacion;
                }
            }
            else
            {
                var etNumHabitacion = this.FindControl<TextBox>("tbNumHabitacion");
                int numHabitacion = Int32.Parse(etNumHabitacion.Text);
                foreach (var habitacion in this._habitaciones)
                {
                    if (habitacion.NumHabitacion == numHabitacion)
                    {
                        toret = habitacion;
                        break;
                    }   
                }
            }

            return toret;
        }
        
        private bool CheckNumberInteger(string value)
        {
            bool toret = true;
            try
            {
                int v=Int32.Parse(value);
            }
            catch (Exception exc)
            {
                toret = false;
            }

            return toret;
        }


        /// <summary>
        /// metodo que comprueba que todos los campos tienen valor
        /// </summary>
        /// <returns></returns>
        private bool CheckIfNull()
        {
            var tipo= this.FindControl<TextBox>("tbTipo");
            var fechaEntrada= this.FindControl<DatePicker>("dpFechaEntrada");
            var fechaSalida= this.FindControl<DatePicker>("dpFechaSalida");
            var iva= this.FindControl<TextBox>("tbIva");
            var garaje= this.FindControl<CheckBox>("cbGaraje");
            var precioDia = this.FindControl<TextBox>("tbImportePorDia");
            var clientes = this.FindControl<ComboBox>("cbClienteReserva");
            var habitaciones = this.FindControl<ComboBox>("cbHabitacionReserva");

            if (tipo.Text == null || fechaEntrada.SelectedDate == null || fechaSalida.SelectedDate == null ||
                precioDia.Text == null
                || iva.Text == null | garaje.IsChecked == null || ( clientes.IsVisible && clientes.SelectedItem == null) ||
                (habitaciones.IsVisible && habitaciones.SelectedItem == null))
            {
                return false;
            }

            return true;

        }
        /// <summary>
        /// Comprueba que donde se pide un numero se escribe
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckNumberDouble(string value)
        {
            bool toret = true;
            try
            {
                int v=Int32.Parse(value);
            }
            catch (Exception exc)
            {
                toret = false;
            }

            return toret;
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}