using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistribucionVehiculosGeneticos
{
    public class Vehiculo
    {
        private int id;
        private char tipo;
        private int tiempo;

        private double probAsignado;
        private Linea lineaAsignada;

        public Vehiculo(int pId, char pTipo, int pTiempo)
        {
            this.id = pId;
            this.tipo = pTipo;
            this.tiempo = pTiempo;
            this.probAsignado = 0;
            this.lineaAsignada = null;
        }

        public Vehiculo(Vehiculo v)
        {
            this.id = v.GetId();
            this.tipo = v.GetTipo();
            this.tiempo = v.GetTiempo();
            this.probAsignado = v.GetProbAsignado();
            this.lineaAsignada = v.GetLineaAsignada();
        }

        // GETTERS
        public int GetId() { return this.id; }

        public char GetTipo() { return this.tipo; }

        public int GetTiempo() { return this.tiempo; }

        public double GetProbAsignado() { return this.probAsignado; }

        public Linea GetLineaAsignada() { return this.lineaAsignada; }

        // SETTERS

        public void SetId(int pId) { this.id = pId; }

        public void SetTipo(char pTipo) { this.tipo = pTipo; }

        public void SetTiempo(int pTiempo) { this.tiempo = pTiempo; }

        public void SetProbAsignado(double pProb) { this.probAsignado = pProb; }

        public void SetLineaAsignada(Linea pLinea) { this.lineaAsignada = pLinea; }
    }
}

