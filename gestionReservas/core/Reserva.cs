using System;

namespace gestionReservas.core
{
    public class Reserva
    {
        public Reserva(Cliente cliente, Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida, int iva, bool usaGaraje, double precioPorDia,string tipo)
        {
            Cliente = cliente;
            Habitacion = habitacion;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            IVA = iva;
            UsaGaraje = usaGaraje;
            PrecioPorDia = precioPorDia;
            this.IdReserva = this.GenerateID();
            this.Tipo = tipo;
        }

        private int GenerateID()
        {
            string id=this.FechaEntrada.Year+""+this.FechaEntrada.Month+""+this.FechaEntrada.Day+""+this.Habitacion.NumHabitacion+"";
            return Int32.Parse(id);
        }
        public Cliente Cliente
        {
            get;
        }

        public string Tipo
        {
            get;
        }

        public Habitacion Habitacion
        {
            get;
        }

        public DateTime FechaEntrada
        {
            get;
        }

        public DateTime FechaSalida
        {
            get;
            set;
        }

        public int IVA
        {
            get;
            set;
        }

        public bool UsaGaraje
        {
            get;
            set;
        }

        public Double PrecioPorDia
        {
            get;
            set;
        }

        public int IdReserva
        {
            get;
           
        }

    }
}