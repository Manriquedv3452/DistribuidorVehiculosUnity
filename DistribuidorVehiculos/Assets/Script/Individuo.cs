using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistribucionVehiculosGeneticos
{
    public class Individuo
    {
        private List<Vehiculo> vehiculos;
        private List<Linea> lineas;
        private Random rnd;
        private float fitness;
        private float punishment = 1000;

        public Individuo(List <Vehiculo> pVehiculos, List<Linea> pLineas)
        {
            this.lineas = new List<Linea>();
            this.vehiculos = new List<Vehiculo>();

            foreach (Linea l in pLineas)
            {
                this.lineas.Add(new Linea(l));
            }
            foreach (Vehiculo v in pVehiculos)
            {
                this.vehiculos.Add(new Vehiculo(v));
            }


            this.rnd = new Random();
            for (int i = 0; i < this.vehiculos.Count; i++)
            {
                double prob = GetProbVehiculo(this.vehiculos.ElementAt(i), this.lineas);
                this.vehiculos.ElementAt(i).SetProbAsignado(prob);
            }
            this.fitness = 10000;
            AsignarVehiculosALineas();
        }

        public Individuo(Individuo i)
        {
            this.lineas = new List<Linea>();
            this.vehiculos = new List<Vehiculo>();

            foreach (Linea l in i.GetLineas())
            {
                this.lineas.Add(new Linea(l));
            }
            foreach (Vehiculo v in i.GetVehiculos())
            {
                this.vehiculos.Add(new Vehiculo(v));
            }

            this.fitness = i.getFitness();
            this.rnd = new Random();
        }

        public Individuo crossover(Individuo partner)
        {
            List<Vehiculo> vehiculos_hijo = new List<Vehiculo>();
            List<Linea> lineas_hijo = new List<Linea>();

            IEnumerable<Vehiculo> sortedPadre = this.vehiculos.OrderBy(x => x.GetId());
            IEnumerable<Vehiculo> sortedMadre = partner.GetVehiculos().OrderBy(x => x.GetId());

            for (int i = 0; i < sortedMadre.Count(); i++)
            {
                if (i < sortedMadre.Count() / 2)
                    vehiculos_hijo.Add(sortedPadre.ElementAt(i));
                else
                    vehiculos_hijo.Add(sortedMadre.ElementAt(i));


            }

            return new Individuo(vehiculos_hijo, this.lineas);
        }

        public void mutar(float porcentajeMutacion)
        {
            foreach (Vehiculo v in this.vehiculos)
            {
                double random_mutate = (Double)(rnd.Next(100) / 100);
                if (random_mutate < porcentajeMutacion)
                {
                    double prob = GetProbVehiculo(v, this.lineas);
                    v.SetProbAsignado(prob);
                }
            }
            this.AsignarVehiculosALineas();
        }

        /**************************************************************************
        Calcula la probabilidad de un vehiculo de ser escogido con base en        *
        la cantidad de lineas que pueden atender ese vehiculo mas un # aleatorio  *
        entre -10 y 10                                                            *
        **************************************************************************/
        private double GetProbVehiculo(Vehiculo v, List<Linea> lineas)
        {
            int countOfLines = lineas.Count;
            double countOfCoincidences = 0.0;
            for (int i = 0; i < countOfLines; i++)
            {
                List<char> tiposVehiculos = lineas.ElementAt(i).getTiposVehiculos();
                for (int j = 0; j < tiposVehiculos.Count; j++)
                {
                    if (tiposVehiculos.ElementAt(j) == v.GetTipo())
                    {
                        countOfCoincidences++;
                    }
                }
            }
            // Aplica lo aleatorio
            int rand = rnd.Next(-30, 0);
            double res = 100 * (countOfCoincidences / countOfLines) + rand;
            return res;
        }

        /****************************************************
   Asigna los vehiculos de una poblacion a las lineas  *
   ****************************************************/
        public void AsignarVehiculosALineas()
        {

            // Asigna el valor inicial a cada linea 
            foreach (Linea l in this.lineas)
            {
                l.RestablecerTiemporestante();
                l.SetNumVehiculosAsignados(0);
            }
            foreach (Vehiculo v in this.vehiculos)
            {
                v.SetLineaAsignada(null);
            }

            // Ordena la lista de menor a mayor segun las probabilidades del vehiculo 
            IEnumerable<Vehiculo> sorted = this.vehiculos.OrderBy(x => x.GetProbAsignado());
            List<Vehiculo> tempListaEspera = new List<Vehiculo>();
            foreach (Vehiculo v in sorted)
            {
                tempListaEspera.Add(v);
            }

            for (int i = 0; i < tempListaEspera.Count; i++)
            {
                // Aleatorio segun la probabilidad del vehiculo
                int rand = rnd.Next(0, 101);
                if ((rand < tempListaEspera.ElementAt(i).GetProbAsignado()))
                {

                    Vehiculo tempVehiculo = tempListaEspera.ElementAt(i);

                    // Busco las lineas en que puede entrar
                    List<Linea> tempLineas = new List<Linea>();
                    for (int j = 0; j < this.lineas.Count; j++)
                    {
                        if (this.lineas.ElementAt(j).GetEstaActiva())
                        {
                            List<char> opciones = this.lineas.ElementAt(j).getTiposVehiculos();
                            if (opciones.Contains(tempVehiculo.GetTipo()))
                            {
                                if ((this.lineas.ElementAt(j).GetTiempoRestante() - tempVehiculo.GetTiempo()) >= 0)
                                {
                                   
                                    tempLineas.Add(this.lineas.ElementAt(j));
                                }
                            }
                        }
                    }

                    // Lista de lineas donde el vehiculo puede entrar
                    if (tempLineas.Count > 0)
                    {
                        // ver cual está vacia
                        Linea tempMenorLinea = tempLineas.ElementAt(0);
                        bool asignado = false;
                        foreach (Linea l in tempLineas)
                        {
                            if (l.GetTiempoAtencion() == l.GetTiempoRestante())
                            {
                                tempVehiculo.SetLineaAsignada(l);

                                l.RestarTiempo(tempVehiculo.GetTiempo());
                                l.IncrementarVehiculos();
                                asignado = true;
                                break;
                            }
                            int tiempoAsignado = tempMenorLinea.GetTiempoAtencion() - tempMenorLinea.GetTiempoRestante();
                            if (tiempoAsignado > (l.GetTiempoAtencion() - l.GetTiempoRestante()))
                            {
                                tempMenorLinea = l;
                            }
                        }
                        // si no, ver la que tenga menor carga
                        if (asignado == false)
                        {
                            int futuroValorLinea = (tempMenorLinea.GetTiempoAtencion() - tempMenorLinea.GetTiempoRestante()) + tempVehiculo.GetTiempo();
                            int maximoConsumo = this.GetTiempoatencionLineaMasPequena() + 20;
                            if (futuroValorLinea <= maximoConsumo)
                            {
                                tempVehiculo.SetLineaAsignada(tempMenorLinea);
                                tempMenorLinea.RestarTiempo(tempVehiculo.GetTiempo());
                                tempMenorLinea.IncrementarVehiculos();
                            }
                        }
                    }

                    /*Console.WriteLine("El vehiculo: " + tempVehiculo.GetId());
                    Console.WriteLine("Puede entrar en las lineas: ");
                    foreach (Linea l in tempLineas)
                    {
                        Console.WriteLine("Tiempo Atención--> " + l.GetTiempoAtencion());
                        Console.WriteLine("Tiempo restante--> " + l.GetTiempoRestante());
                        Console.WriteLine("#");
                    }
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("--------------------------------------");*/


                }
            }

            // Busca la linea con menor capacidad
            Linea lineaConMenoCapacidad = this.GetLineaConMenorCapacidad();
            int maximoValorPorLinea = lineaConMenoCapacidad.GetTiempoAtencion();
            int rango = maximoValorPorLinea + 20;
            this.LineasEnRango(rango);

        }

        /****************************************************************
        Evalua si la población es buena o no                            *
        Retorna True si la población cumple con las caracteristicas     *
        Retorna False si la población no cumple con las caracteristicas *
        ****************************************************************/
        public void Fitness()
        {
            // Busca la linea con menor capacidad
            Linea lineaConMenoCapacidad = this.GetLineaConMenorCapacidad();
            int maximoValorPorLinea = lineaConMenoCapacidad.GetTiempoAtencion();


            // Busca si alguna linea se pasa del rango 
            int rango = maximoValorPorLinea + 20;
            if (this.LineasFueraDeRango(rango) == false)
            {
                this.fitness += punishment * 2;
                return;
            }

            // Verifico la equivalencia final 
            int mayorCargaPosible = lineaConMenoCapacidad.GetTiempoAtencion() - lineaConMenoCapacidad.GetTiempoRestante() + 20;
           // Console.WriteLine("La mayor carga es: " + mayorCargaPosible);
            if (this.VerificarEquivalencia(mayorCargaPosible))
            {
                this.fitness = 0;
                return;
            }
            else
            {
                this.fitness += this.punishment * 2;
            }
           
            this.fitness += punishment * 3;
        }

        private int GetTiempoatencionLineaMasPequena()
        {
            IEnumerable<Linea> sorted = this.lineas.OrderBy(x => x.GetTiempoAtencion());
            List<Linea> temp = new List<Linea>();
            return sorted.ElementAt(0).GetTiempoAtencion();
        }


        // Busca la linea con menor capacidad
        private Linea GetLineaConMenorCapacidad()
        {
            IEnumerable<Linea> sorted = this.GetLineas().OrderBy(x => (x.GetTiempoAtencion() - x.GetTiempoRestante()));
            List<Linea> temp = new List<Linea>();
            return sorted.ElementAt(0);
        }


        private bool LineasFueraDeRango(int rango)
        {
            foreach (Linea l in this.lineas)
            {
                // Si la linea se pasa del rango 
                int tiempoAsignado = l.GetTiempoAtencion() - l.GetTiempoRestante();
                if (tiempoAsignado > rango)
                {
                    // Hay que buscar el porqué
                    if (l.GetNumVehiculosAsignados() > 1)
                    {
                        // No puede darse este caso, hay que poner la penalización 
                       // Console.WriteLine("La linea está muy llena y tiene mas de un vehiculo");
                        return false;
                    }
                }
            }
            return true;
        }


        private void LineasEnRango(int rango)
        {
            List<char> opciones;
            foreach (Linea l in this.lineas)
            {
                foreach (Vehiculo v in this.vehiculos)
                {
                    // Si el vehiculo no fue asignado
                    if (v.GetLineaAsignada() == null)
                    {
                        int nuevotiempoTotal = (l.GetTiempoAtencion() - l.GetTiempoRestante()) + v.GetTiempo();
                        if (nuevotiempoTotal <= rango)
                        {
                            // si no se sale del rango, hay que penalizar 
                            // Console.WriteLine("Se pudo haber ingresado el vehiculo: " + v.GetId());
                            //v.SetLineaAsignada(l);
                            //l.RestarTiempo(v.GetTiempo());
                            //l.SetNumVehiculosAsignados(l.GetNumVehiculosAsignados() + 1);
                            opciones = l.getTiposVehiculos();
                            if (l.GetTiempoRestante() - v.GetTiempo() >= 0 && opciones.Contains(v.GetTipo()))
                            {
                                v.SetLineaAsignada(l);

                                l.RestarTiempo(v.GetTiempo());
                                l.IncrementarVehiculos();
                            }
                        }
                    }
                }
            }
        }


        private bool VerificarEquivalencia(int mayorCargaPosible)
        {
            foreach (Linea l in this.lineas)
            {
                int cargaAsignada = (l.GetTiempoAtencion() - l.GetTiempoRestante());
                
                if (cargaAsignada > mayorCargaPosible || cargaAsignada == 0)
                {
                   // Console.WriteLine("La linea: " + l.GetTiempoAtencion() + " se pasó de la mayor carga posible que era: " + mayorCargaPosible);
                    return false;
                }
            }
            return true;
        }


        public float getFitness()
        {
            return this.fitness;
        }


        public List<Vehiculo> GetVehiculos()
        {
            return this.vehiculos;
        }
        public List<Linea> GetLineas()
        {
            return this.lineas;
        }

        public void SetVehiculos(List<Vehiculo> list)
        {
            this.vehiculos = list;
        }

        // IMPRIME UNA SOLUCION en consola
        public void PrintIndividuo()
        {
            List<Vehiculo> listaEsperaTemp = this.vehiculos;
            List<Linea> lineasTemp = this.lineas;
            Console.WriteLine("##############################################################");
            Console.WriteLine("##############################################################");
            Console.WriteLine("LINEAS");
            for (int i = 0; i < lineasTemp.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Linea: " + i);
                Console.WriteLine("Tiempo de atencion: " + lineasTemp.ElementAt(i).GetTiempoAtencion());
                Console.WriteLine("Tiempo restante: " + lineasTemp.ElementAt(i).GetTiempoRestante());
                Console.WriteLine("¿Está activa?: " + lineasTemp.ElementAt(i).GetEstaActiva());
                Console.WriteLine("---------------------------------------------------------------");
            }

            Console.WriteLine(""); Console.WriteLine("");
            Console.WriteLine("VEHICULOS");
            for (int i = 0; i < listaEsperaTemp.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Vehiculo: " + listaEsperaTemp.ElementAt(i).GetId());
                Console.WriteLine("Tipo: " + listaEsperaTemp.ElementAt(i).GetTipo());
                Console.WriteLine("Tiempo total: " + listaEsperaTemp.ElementAt(i).GetTiempo());
                Console.WriteLine("Probabilidad: " + listaEsperaTemp.ElementAt(i).GetProbAsignado());
                Linea linea = listaEsperaTemp.ElementAt(i).GetLineaAsignada();
                if (linea != null)
                {
                    Console.WriteLine("Tiempo restante de linea asignada: " + linea.GetTiempoRestante());
                }
                Console.WriteLine("---------------------------------------------------------------");
            }
            Console.WriteLine("FITNESS: " + this.fitness);

           Console.WriteLine("##############################################################");
            Console.WriteLine("##############################################################");
        }

        public String PrintStringIndividuo()
        {
            String str = "";

            List<Vehiculo> listaEsperaTemp = this.vehiculos;
            List<Linea> lineasTemp = this.lineas;
            str += "##############################################################\n";
            str += "##############################################################\n";
            str += "LINEAS\n";
            for (int i = 0; i < lineasTemp.Count; i++)
            {
                str += "---------------------------------------------------------------\n";
                str += "Linea: " + i + "\n";
                str += "Tiempo de atencion: " + lineasTemp.ElementAt(i).GetTiempoAtencion() + "\n";
                str += "Tiempo restante: " + lineasTemp.ElementAt(i).GetTiempoRestante() + "\n";
                str += "¿Está activa?: " + lineasTemp.ElementAt(i).GetEstaActiva() + "\n";
                str += "---------------------------------------------------------------\n";
            }

            str += "\n"; str += "\n";

            str += "VEHICULOS\n";
            for (int i = 0; i < listaEsperaTemp.Count; i++)
            {
                str += "---------------------------------------------------------------\n";
                str += "Vehiculo: " + listaEsperaTemp.ElementAt(i).GetId() + "\n";
                str += "Tipo: " + listaEsperaTemp.ElementAt(i).GetTipo() + "\n";
                str += "Tiempo total: " + listaEsperaTemp.ElementAt(i).GetTiempo() + "\n";
                str += "Probabilidad: " + listaEsperaTemp.ElementAt(i).GetProbAsignado() + "\n";
                Linea linea = listaEsperaTemp.ElementAt(i).GetLineaAsignada();
                if (linea != null)
                {
                    str += "Tiempo restante de linea asignada: " + linea.GetTiempoRestante() + "\n";
                }
                str += "---------------------------------------------------------------\n";
            }
            str += "FITNESS: " + this.fitness + "\n";

            str += "##############################################################\n";
            str += "##############################################################\n";

            return str;
        }

    }
}
