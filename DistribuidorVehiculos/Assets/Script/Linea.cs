using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistribucionVehiculosGeneticos
{
    public class Linea
    {
        private int tiempoAtencion;
        private int tiempoRestante;
        private int tiempoRestanteInicial;
        private List<char> tiposVehiculos;
        private bool activa;
        private int numVehiculosAsignados;
        private int id;

        public Linea(int pId, int pTiempoAtencion, List<char> pTiposVehiculos, bool pActiva, int pTiempoRestante)
        {
            this.tiempoAtencion = pTiempoAtencion;
            this.tiposVehiculos = pTiposVehiculos;
            this.activa = pActiva;
            this.tiempoRestante = pTiempoRestante;
            this.tiempoRestanteInicial = pTiempoRestante;
            this.numVehiculosAsignados = 0;
            this.id = pId;
        }

        public Linea(Linea l)
        {
            this.tiempoAtencion = l.GetTiempoAtencion();
            this.tiposVehiculos = new List<char>(l.getTiposVehiculos());
            this.activa = l.GetEstaActiva();
            this.tiempoRestante = l.GetTiempoRestante();
            this.tiempoRestanteInicial = l.getTiempoRestanteInicial();
            this.numVehiculosAsignados = l.GetNumVehiculosAsignados();
            this.id = l.GetId();
        }
        public List<char> getTiposVehiculos()
        {
            return this.tiposVehiculos;
        }

        public int GetNumVehiculosAsignados()
        {
            return this.numVehiculosAsignados;
        }

        public void IncrementarVehiculos()
        {
            this.numVehiculosAsignados += 1;
        }

        public void DecrementarVehiculos()
        {
            this.numVehiculosAsignados -= 1;
        }

        public void RestarTiempo(int pTiempo)
        {
            this.tiempoRestante -= pTiempo;
        }

        public int GetId()
        {
            return this.id;
        }

        public int GetTiempoRestante()
        {
            return this.tiempoRestante;
        }

        public int GetTiempoAtencion()
        {
            return this.tiempoAtencion;
        }

        public int getTiempoRestanteInicial()
        {
            return this.tiempoRestanteInicial;
        }

        public bool GetEstaActiva()
        {
            return this.activa;
        }

        public void SetActiva(bool val)
        {
            this.activa = val;
        }

        public void SetNumVehiculosAsignados(int num)
        {
            this.numVehiculosAsignados = num;
        }

        public void RestablecerTiemporestante()
        {
            this.tiempoRestante = this.tiempoRestanteInicial;
        }
    }
}

