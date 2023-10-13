
    using System.Collections;
    using UnityEngine;
    /**
     * EventPhase 类可为 Event 类的 eventPhase 属性提供值。
     * @author 回眸笑苍生
     **/
    public class EventPhase
    {

        public const int CAPTURING_PHASE = 1;

        public const int AT_TARGET = 2;

        public const int BUBBLING_PHASE = 3;
    }