using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistribucionVehiculosGeneticos
{
    public class AlgoritmoGenetico
    {
        private int numGeneraciones = 500;
        private Poblacion poblacion;
        private Random rnd = new Random();


        /***************
         * Constructor
         **************/
        public AlgoritmoGenetico(List<Vehiculo> pListaEspera, List<Linea> pLineas)
        {
            this.poblacion = new Poblacion(pListaEspera, pLineas);
        }
        public AlgoritmoGenetico()
        {
            this.poblacion = new Poblacion();
        }

  
        
        /******************************************************
       Inicia el algoritmo genetico con base en la poblacion *
       ******************************************************/
        public Individuo IniciarGenetico()
        {
            Console.WriteLine("genetico iniciado");

            // Si no se ha generado la población 
            if (this.poblacion == null)
            {
                Console.WriteLine("genetico NO iniciado, no hay una población");
                return null;
            }

            // Si la población fue generada correctamente
            for (int i = 0; i < this.numGeneraciones; i++)
            {
                Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
                Console.WriteLine("GENERACION: " + i);
                this.poblacion.seleccionNatural();
                this.poblacion.cruce();
                this.poblacion.calcFitness();

                if (this.poblacion.getBestIndividuo().getFitness() < 50f)
                {
                    //Console.WriteLine("He ENCONTRADO EL MEJOR\n\n");
                    return this.poblacion.getBestIndividuo();
                }


                // Genera una nueva población 
                //this.AplicarOperadoresGeneticos();
            }
            Console.WriteLine("genetico terminado");
            return this.poblacion.getBestIndividuo();
        }

    }
}
