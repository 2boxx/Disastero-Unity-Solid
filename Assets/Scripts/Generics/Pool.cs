using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool <Tpool>
{
    private List<PoolObject<Tpool>> _poolList;
    public delegate Tpool CallbackFactory();//Este es el metodo de mi factory con el que voy a crear los objetos.esta es la manera de pedir un metodo por parametro. Tipo T porque puede devolver cualquier cosa.

    private int _count;
    private bool _isDynamic = false;
    private PoolObject<Tpool>.PoolCallback _init; //Explicar! Estoy pidiendo un delegate del tipo que esta definido en PoolObject. Por que? Como funciona?
    private PoolObject<Tpool>.PoolCallback _finalize; //Esto es como una lista de metodos? O es siempre el mismo metodo, la que le paso distintos parametros?
    private CallbackFactory _factoryMethod;

    //Create un pool ;)
    public Pool(int initialStock, CallbackFactory factoryMethod, PoolObject<Tpool>.PoolCallback initialize, PoolObject<Tpool>.PoolCallback finalize, bool isDynamic)
    {
        //inicializo la lista
        _poolList = new List<PoolObject<Tpool>>();

        //ahora guardamos los punteros que necesitamos
        _factoryMethod = factoryMethod;
        _isDynamic = isDynamic;
        _count = initialStock;
        _init = initialize;
        _finalize = finalize;

        //generamos el stock inicial

        for (int i = 0; i < _count; i++)
        {
            _poolList.Add(new PoolObject<Tpool>(_factoryMethod(), _init, _finalize));
        }

		//new line: desactivo el stock inicial para que empiece desactivado

		foreach (PoolObject<Tpool> poolObj in _poolList)
		{
			poolObj.IsActive = false;
		}

	}

    //Dame un objeto cualquiera disponible del pool
    public PoolObject<Tpool> GetPoolObject()
    {
        for (int i = 0; i < _count; i++) //recorro todos los objetos del pool...
        {
            if(!_poolList[i].IsActive) //...hasta encontrar uno inactivo...
            {
                _poolList[i].IsActive = true; //...cuando lo encuentro, lo activo...
                return _poolList[i]; //...y lo devuelvo.
            }
        }
        if (_isDynamic) //Si no encontre ninguno, y soy dinamico...
        {
            PoolObject<Tpool> po = new PoolObject<Tpool>(_factoryMethod(), _init, _finalize); //...creo uno...
            po.IsActive = true; //... lo activo ...
            _poolList.Add(po); //... lo agrego a la lista...
            _count++; //...aumento mi contador...
            return po; //y lo devuelvo.
        }
        return null; // Si no puedo devolver nada, devuelvo null.
    }

    //Dame un objeto cualquiera disponible del pool
    public Tpool GetObject()
    {
        for (int i = 0; i < _count; i++) //recorro todos los objetos del pool...
        {
            if (!_poolList[i].IsActive) //...hasta encontrar uno inactivo...
            {
                _poolList[i].IsActive = true; //...cuando lo encuentro, lo activo...
                return _poolList[i].GetObj; //...y lo devuelvo.
            }
        }
        if (_isDynamic) //Si no encontre ninguno, y soy dinamico...
        {
            PoolObject<Tpool> po = new PoolObject<Tpool>(_factoryMethod(), _init, _finalize); //...creo uno...
            po.IsActive = true; //... lo activo ...
            _poolList.Add(po); //... lo agrego a la lista...
            _count++; //...aumento mi contador...
            return po.GetObj; //y lo devuelvo.
        }
        return default(Tpool); // Fancy return null for generics
    }

    //Desactivame un objeto especifico del pool
    public void DisablePoolObject (Tpool requestedObj) //DESACTIVA UN OBJETO (ej: bala) que recibe por parametro
    {
        foreach(PoolObject<Tpool> poolObj in _poolList)
        {
            if (poolObj.GetObj.Equals(requestedObj))
            {
                poolObj.IsActive = false;
                return;
            }
        }
    }

}
