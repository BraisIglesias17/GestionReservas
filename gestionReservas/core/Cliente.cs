namespace gestionReservas.core
{
    public class Cliente
    {
        private string nombre;
        
        public Cliente(string dni)
        {
            this.Dni = dni;
        }

        public string Dni { get; set; }

        public override string ToString()
        {
            return this.Dni;
        }
    }
}