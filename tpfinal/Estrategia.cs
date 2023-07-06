
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using tp1;

namespace tpfinal
{

	public class Estrategia
	{
		//metodos
		private int CalcularDistancia(string str1, string str2)
		{
			// using the method
			String[] strlist1 = str1.ToLower().Split(' ');
			String[] strlist2 = str2.ToLower().Split(' ');
			int distance = 1000;
			foreach (String s1 in strlist1)
			{
				foreach (String s2 in strlist2)
				{
					distance = Math.Min(distance, Utils.calculateLevenshteinDistance(s1, s2));
				}
			}

			return distance;
		}

		public String Consulta1(ArbolGeneral<DatoDistancia> arbol)//retorna las hojas 
		{
			string resutl = "";
            if (arbol.esHoja()) //si es hoja
            {
                return arbol.getDatoRaiz().texto + " \n"; //agrego el dato al resultado
            }
            else //si no es hoja
            {
                foreach (ArbolGeneral<DatoDistancia> hijo in arbol.getHijos()) //recorro lista de hijos
                {
                    resutl += Consulta1(hijo); //llamada recursiva
                }
            }
            return resutl;
		}


		public String Consulta2(ArbolGeneral<DatoDistancia> arbol)
		{
			string result = "";
            ArrayList caminos = new ArrayList(); //creacion de arraylist donde se guardaran los caminos
            ArrayList nodos = new ArrayList(); //creacion de arraylist donde se guardaran los nodos de un camino
            _Consulta2(arbol, caminos, nodos); //llamado al metodo interno que buscara los caminos
            foreach (ArrayList a in caminos) //recorre la lista de caminos
            {
                foreach (string s in a)//recorre cada camino
                {

                    result += s + " - ";//agrega cada nodo del camino
                }
                result += "\n";
            }
            return result;
        }

        private void _Consulta2(ArbolGeneral<DatoDistancia> arbol, ArrayList caminos, ArrayList nodos)
        {
            ArrayList aux = new ArrayList(); //crea arraylist auxiliar
            if (arbol.esHoja())//si es hoja
            {
                nodos.Add(arbol.getDatoRaiz().texto);//agrega al camino
                foreach (string ele in nodos)//recorre el camino
                {
                    aux.Add(ele);//crea un camino
                }
                caminos.Add(aux);//agrega el camino a la lista de caminos
            }
            else //si no es hoja
            {
                nodos.Add(arbol.getDatoRaiz().texto);//agrega al camino
                foreach (ArbolGeneral<DatoDistancia> hijo in arbol.getHijos())//recorre los hijos
                {
                    _Consulta2(hijo, caminos, nodos);//llamada recursiva
                    nodos.RemoveAt(nodos.Count - 1);//elimina al nodo
                }
            }
        }

        public String Consulta3(ArbolGeneral<DatoDistancia> arbol) //datos por nivel
		{
			string result = "";
            int nivel = 0; //contador de nivel
            Cola<ArbolGeneral<DatoDistancia>> c = new Cola<ArbolGeneral<DatoDistancia>>();  //creacion de cola de arboles generales
            ArbolGeneral<DatoDistancia> arbaux; //creacion de arbol auxiliar
            c.encolar(arbol);//encolo arbol
            c.encolar(null);//encolo separador
            result += "\n" + "Nivel " + nivel + ": "; //sumo al resultado
            while (!c.esVacia())
            {//mientras la cola no este vacia
                arbaux = c.desencolar(); //desencolo en el arbol auxiliar
                if (arbaux == null)//si desencole un separador
                {
                    nivel++; //aumento el nivel
                    if (!c.esVacia()) //si la cola no esta vacia encolo un separador
                    {
                        result += "\n"+"Nivel " + nivel + ": "; //sumo al resultado
                        c.encolar(null);//encolo separador
                    }
                }
                else
                { //si no desencole un separador
                    result += arbaux.getDatoRaiz().texto + " "; //sumo dato al resultado
                    foreach (var hijo in arbaux.getHijos())//recorro lista de hijos
                    { 
                        c.encolar(hijo); //encolo los hijos
                    }
                }
            }
            return result;
		}

		public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato) //agrega dato en determinada posicion
		{
            int distancia = CalcularDistancia(arbol.getDatoRaiz().texto, dato.texto); //calculo de distancia entre raiz actual y dato a agregar
            bool flag = false; //false si no hay hijo con la misma distancia, true si hay
			foreach(var hijo in arbol.getHijos())//recorro los hijos de la raiz actual
			{
				if (hijo.getDatoRaiz().distancia == distancia)//si ya hay un hijo con esa distancia
				{
					flag = true;
                    AgregarDato(hijo, dato);//llamada recursiva
					break; //deja de buscar
                }
			}
            if(!flag)//si no hay hijo con esa distancia
			{
                DatoDistancia da = new DatoDistancia(distancia, dato.texto, dato.descripcion); //creo dato distancia
                arbol.agregarHijo(new ArbolGeneral<DatoDistancia>(da)); //agrego el dato distancia como hijo de la raiz actual
			}
        }

         public void Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> collected)
         {
             int distancia = CalcularDistancia(arbol.getDatoRaiz().texto, elementoABuscar); //calculo distancia entre raiz actual y elemento a buscar
             if (distancia <= umbral) //si la distancia es menor al umbral
             {
                 collected.Add(arbol.getDatoRaiz());//agrego dato 
             }
             foreach (ArbolGeneral<DatoDistancia> hijo in arbol.getHijos()) //recorro lista de hijos
             {
                 Buscar(hijo, elementoABuscar, umbral, collected);//llamada recursiva
             }
         }
    }
}