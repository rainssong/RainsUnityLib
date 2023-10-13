

    using System.Collections;
    using System;
    using UnityEngine;
    /**
     * EventObject 类作为创建 Event 对象的基类，当发生事件时，Event 对象将作为参数传递给事件侦听器。
     * @author 回眸笑苍生
     **/
    public class EventObject
    {
        public const String ACTIVATE = "activate";
        public const String ADDED = "added";
        public const String ADDED_TO_STAGE = "addedToStage";
        public const String CANCEL = "cancel";
        public const String CHANGE = "change";
        public const String CLEAR = "clear";
        public const String CLOSE = "close";
        public const String CLOSING = "closing";
        public const String COMPLETE = "complete";
        public const String CONNECT = "connect";
        public const String OPEN = "open";

        private EventDispatcher _target;
        private int _eventPhase;
        private EventDispatcher _currentTarget;
        private bool _bubbles;
        private bool _cancelable;
        private String _types;

        public EventObject (String types, bool bubbles = false, bool cancelable = false)
        {
            this._types = types;
            this._bubbles = bubbles;
            this._cancelable = cancelable;
        }

        public EventDispatcher target
        {
            get { return _target; }
        }

        internal EventDispatcher setTarget
        {
            set { _target = value; }
        }

        public EventDispatcher currentTarget
        {
            get { return _currentTarget; }
        }

        internal EventDispatcher setCurrentTarget
        {
            set { _currentTarget = value; }
        }

        public int eventPhase
        {
            get { return _eventPhase; }
        }

        internal int setEventPhase
        {
            set { _eventPhase = value; }
        }

        public bool bubbles
        {
            get { return _bubbles; }
        }

        public String types
        {
            get { return _types; }
        }

        public bool cancelable
        {
            get { return _cancelable; }
        }
    }