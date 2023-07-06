using System;
using System.Collections.Generic;

namespace tp1
{
	[Serializable]
	public class ArbolGeneral<T>
	{
		//atributos
		private T dato;
		private List<ArbolGeneral<T>> hijos = new List<ArbolGeneral<T>>();

		//constructor
		public ArbolGeneral(T dato)
		{
			this.dato = dato;
		}

		//propiedades
		public T getDatoRaiz()
		{
			return this.dato;
		}

		public List<ArbolGeneral<T>> getHijos()
		{
			return hijos;
		}

		//metodos
		public void agregarHijo(ArbolGeneral<T> hijo)
		{
			this.getHijos().Add(hijo);
		}

		public void eliminarHijo(ArbolGeneral<T> hijo)
		{
			this.getHijos().Remove(hijo);
		}


		public bool esHoja()
		{
			return this.getHijos().Count == 0;
		}

		public int altura()
		{
			return 0;
		}


		public int nivel(T dato)
		{
			return 0;
		}

	}
}
