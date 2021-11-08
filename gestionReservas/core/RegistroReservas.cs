using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using JetBrains.Annotations;

namespace gestionReservas.core
{
    public class RegistroReservas : ObservableCollection<Reserva>
    {
        public RegistroReservas():base(new List<Reserva>())
        {
        }

        public RegistroReservas(List<Reserva> list) : base(list)
        {
        }

        public void AddReserva(Reserva nueva) => base.Add(nueva);

        public void RemoveReserva(Reserva x) => base.Remove(x);

        public void RemoveReservaForIndex(int i) => base.RemoveAt(i);

        public Reserva[] RegistroReservasToArray => this.ToArray();
        public int Length => this.Count;
    }
}