using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject <Tobj>
{
    private bool _isActive = true;
    private Tobj _obj; //El objeto que envuelvo. Esta variable va a adoptar el tipo bullet una vez instanciada la clase
    public delegate void PoolCallback(Tobj obj); //un delegate con los metodos para iniciar y finalizar. Recibe por parametro un bullet
    private PoolCallback _initializationCallback;
    private PoolCallback _finalizationCallback;

    public PoolObject(Tobj obj, PoolCallback initialization, PoolCallback finalization)
    {
        _obj = obj;
        _initializationCallback = initialization;
        _finalizationCallback = finalization;
        IsActive = false;
    }

    public Tobj GetObj //Tobj = bullet
    {
        get
        {
            return _obj;
        }
    }

    public bool IsActive
    {
        get
        {
            return _isActive;
        }

        set
        {
            if (_isActive == !value)
            {
                _isActive = value;
                if (_isActive)
                {
                    if (_initializationCallback != null)
                        _initializationCallback(_obj);
                }
                else
                {
                    if (_finalizationCallback != null)
                        _finalizationCallback(_obj);
                }
            }
        }
    }
}
