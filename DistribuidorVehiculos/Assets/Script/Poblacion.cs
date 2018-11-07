using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistribucionVehiculosGeneticos
{
    public class Poblacion
    {
        private List<Individuo> individuos;
        private int numIndividuos = 100;
        private List<Vehiculo> listaEsperaOriginal;
        private List<Individuo> posiblesPadres;
        private List<Linea> lineasOriginales;

        private float bestFitness;
        private Individuo bestIndividuo;
        private float porcentajeMutacion = 0.05f;
        private Random rnd = new Random();

        public Poblacion(List<Vehiculo> pListaEspera, List<Linea> pLineas)
        {
            this.listaEsperaOriginal = pListaEspera;
            this.lineasOriginales = pLineas;
            this.individuos = new List<Individuo>();
            this.posiblesPadres = new List<Individuo>();
            GenerarPoblacionInicial();
        }

        public Poblacion()
        {
            this.listaEsperaOriginal = new List<Vehiculo>();
            this.lineasOriginales = new List<Linea>();

            this.individuos = new List<Individuo>(); 
        }

        /**************************************************************************
      Genera una población apartir de la lista de espera y la lista de lineas   *
      **************************************************************************/
        public void GenerarPoblacionInicial()
        {
            for (int i = 0; i < this.numIndividuos; i++)
            {
                this.individuos.Add(generarIndividuo());
            }
            this.bestFitness = this.individuos.ElementAt(0).getFitness();
            this.bestIndividuo = this.individuos.ElementAt(0);
        }

        private Individuo generarIndividuo()
        {
            Individuo individuo;
            // Asigna el valor inicial a cada linea 
           
            individuo = new Individuo(this.listaEsperaOriginal, this.lineasOriginales);
            return individuo;
        }

        public void calcFitness()
        {

            foreach (Individuo i in this.individuos)
            {
                //i.PrintIndividuo();
                i.Fitness();
                if (i.getFitness() < bestFitness)
                {
                    this.bestFitness = i.getFitness();
                    this.bestIndividuo = i;
                }

            }
        }


        public void seleccionNatural()
        {
            this.posiblesPadres.Clear();


            float worstFitness = 0f;
            foreach (Individuo i in this.individuos)
            {
                if (i.getFitness() > worstFitness)
                    worstFitness = i.getFitness();
            }

            foreach (Individuo i in this.individuos)
            {
                float fitness = Math.Abs(map(i.getFitness(), worstFitness, 0, 0, 1));
                //Console.WriteLine("\n" + map(i.getFitness(), worstFitness, 0, 0, 1).ToString("n2"));

                //Console.WriteLine(0 + "+" + "(" + 1 + "-" + 0 +")*(("+i.getFitness()+" - "+ worstFitness +") / ( " + 0 + " - " + 1+"))");
                //String fin = Console.ReadLine();
                int n = (int) Math.Round((fitness * 100)) + 1;

                for (int j = 0; j < n; j++)
                {
                    
                    this.posiblesPadres.Add(i);
                }
            }

        }

        public void cruce()
        {
            
            List<Individuo> nuevosIndividuos = new List<Individuo>();
            foreach (Individuo i in this.individuos)
            {
                //Console.WriteLine("\n\n=====" + this.posiblesPadres.Count + "\n");
                Individuo padre = this.posiblesPadres.ElementAt(rnd.Next(this.posiblesPadres.Count));

                Individuo madre = this.posiblesPadres.ElementAt(rnd.Next(this.posiblesPadres.Count));

                Individuo nuevoIndividuo = padre.crossover(madre);
                nuevoIndividuo.mutar(this.porcentajeMutacion);
                nuevosIndividuos.Add(nuevoIndividuo);
            }
            this.individuos.Clear();
            this.individuos = new List<Individuo>(nuevosIndividuos);
            
        }



        /****************************************************
         mapea el valor que es en rango orginMin-originMax
         a nuevo rango de targetMin-max*
        ****************************************************/
        public static float map(float value, float originMin, float originMax, float targetMin, float targetMax)
        {
            return targetMin + (targetMax - targetMin) * ((value - originMin)/(originMax - originMin));
        }


        /****************************************
         Agrega una linea a la lista de lineas  *
        ****************************************/
        public void AgregarLinea(int pId, int pTiempoAtencion, List<char> pTiposVehiculos, bool pActiva, int pTiempoRestante)
        {
            this.lineasOriginales.Add(new Linea(pId, pTiempoAtencion, pTiposVehiculos, pActiva, pTiempoRestante));
        }


        /******************************************
         Agrega un vehiculo a la lista de espera  *
        ******************************************/
        public void AgregarVehiculoEnEspera(int pId, char pTipo, int pTiempo)
        {
            this.listaEsperaOriginal.Add(new Vehiculo(pId, pTipo, pTiempo));
        }

        public Individuo getBestIndividuo()
        {
            return this.bestIndividuo;
        }
        public List<Individuo> GetIndividuos()
        {
            return this.individuos;
        }

    }
}
