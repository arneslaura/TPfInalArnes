using System;
using System.Collections.Generic;

namespace tp1
{
	public class Cola<T>
	{

		//atributos
		private List<T> datos = new List<T>();
		
		//metodos
		public void encolar(T elem)
		{
			this.datos.Add(elem);
		}
		
		public T desencolar()
		{
			T temp = this.datos[0];
			this.datos.RemoveAt(0);
			return temp;
		}
		
		public T tope()
		{
			return this.datos[0];
		}
		
		public bool esVacia()
		{
			return this.datos.Count == 0;
		}
		
		public int cantidadElementos()
		{
			return this.datos.Count;
		}
	}
}
