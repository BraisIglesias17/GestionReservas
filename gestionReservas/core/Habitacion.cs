namespace gestionReservas.core
{
    public class Habitacion
    {
        public Habitacion(int numHabitacion)
        {
            this.NumHabitacion = numHabitacion;
        }


        public int NumHabitacion
        {
            get;
        }

        public override string ToString()
        {
            return ""+this.NumHabitacion;
        }
    }
}