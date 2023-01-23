using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * EventDispatcher 类是可调度事件的所有类的基类
 * rainssong注：可以向父级对象发送事件
 * @author 回眸笑苍生
 **/


public abstract class EventDispatcher : MonoBehaviour
{
    //定义委托
    public delegate void EventDelegate(EventObject evt);

    protected Dictionary<String, List<EventDelegate>> captureListeners = null;

    protected Dictionary<String, List<EventDelegate>> bubbleListeners = null;

    protected EventDispatcher _parent;

    public void addEventListener(String types, EventDelegate listener, bool useCapture = false, int priority = 0, bool useWeakReference = false)
    {
        Dictionary<String, List<EventDelegate>> listeners = null;

        if (listener == null)
        {
            throw new ArgumentNullException("Parameter listener must be non-null.");
        }
        if (useCapture)
        {
            if (captureListeners == null) captureListeners = new Dictionary<string, List<EventDelegate>>();
            listeners = captureListeners;
        }
        else
        {
            if (bubbleListeners == null) bubbleListeners = new Dictionary<string, List<EventDelegate>>();
            listeners = bubbleListeners;
        }
        List<EventDelegate> vector = null;
        if (listeners.ContainsKey(types))
        {
            vector = listeners[types];
        }
        if (vector == null)
        {
            vector = new List<EventDelegate>();
            listeners.Add(types, vector);
        }
        if (vector.IndexOf(listener) < 0)
        {
            vector.Add(listener);
        }
    }

    public void removeEventListener(String types, EventDelegate listener, bool useCapture = false)
    {
        if (listener == null)
        {
            throw new ArgumentNullException("Parameter listener must be non-null.");
        }
        Dictionary<string, List<EventDelegate>> listeners = useCapture ? captureListeners : bubbleListeners;
        if (listeners != null)
        {
            List<EventDelegate> vector = listeners[types];
            if (vector != null)
            {
                int i = vector.IndexOf(listener);
                if (i >= 0)
                {
                    int length = vector.Count;
                    for (int j = i + 1; j < length; j++, i++)
                    {
                        vector[i] = vector[j];
                    }

                    listeners.Remove(types);

                    foreach (KeyValuePair<String, List<EventDelegate>> o in listeners)
                    {
                        if (o.Key == null)
                        {
                            if (listeners == captureListeners)
                            {
                                captureListeners = null;
                            }
                            else
                            {
                                bubbleListeners = null;
                            }
                        }
                    }
                }
            }
        }

    }

    public bool hasEventListener(String types)
    {
        return (captureListeners != null && captureListeners.ContainsKey(types)) || (bubbleListeners != null && bubbleListeners.ContainsKey(types));
    }

    public bool willTrigger(String types)
    {
        for (EventDispatcher _object = this; _object != null; _object = _object._parent)
        {
            if ((_object.captureListeners != null && _object.captureListeners.ContainsKey(types)) || (_object.bubbleListeners != null && _object.bubbleListeners.ContainsKey(types)))
                return true;
        }
        return false;
    }

    public bool dispatchEvent(EventObject evt)
    {
        if (evt == null)
        {
            throw new ArgumentNullException("Parameter EventObject must be non-null.");
        }
        EventObject event3D = evt;
        if (event3D != null)
        {
            event3D.setTarget = this;
        }
        List<EventDispatcher> branch = new List<EventDispatcher>();
        int branchLength = 0;
        EventDispatcher _object;
        int i, j = 0;
        int length;
        List<EventDelegate> vector;
        List<EventDelegate> functions;
        for (_object = this; _object != null; _object = _object._parent)
        {
            branch.Add(_object);
            branchLength++;
        }
        for (i = branchLength - 1; i > 0; i--)
        {
            _object = branch[i];
            if (event3D != null)
            {
                event3D.setCurrentTarget = _object;
                event3D.setEventPhase = EventPhase.CAPTURING_PHASE;
            }
            if (_object.captureListeners != null)
            {
                vector = _object.captureListeners[evt.types];
                if (vector != null)
                {
                    length = vector.Count;
                    functions = new List<EventDelegate>();
                    for (j = 0; j < length; j++) functions[j] = vector[j];
                    for (j = 0; j < length; j++) (functions[j] as EventDelegate)(evt);
                }
            }
        }
        if (event3D != null)
        {
            event3D.setEventPhase = EventPhase.AT_TARGET;
        }
        for (i = 0; i < branchLength; i++)
        {
            _object = branch[i];
            if (event3D != null)
            {
                event3D.setCurrentTarget = _object;
                if (i > 0)
                {
                    event3D.setEventPhase = EventPhase.BUBBLING_PHASE;
                }
            }
            if (_object.bubbleListeners != null)
            {
                vector = _object.bubbleListeners[evt.types];
                if (vector != null)
                {
                    length = vector.Count;
                    functions = new List<EventDelegate>();
                    for (j = 0; j < length; j++) functions.Add(vector[j]);
                    for (j = 0; j < length; j++) (functions[j] as EventDelegate)(evt);
                }
            }
            if (!event3D.bubbles) break;
        }
        return true;
    }

}
