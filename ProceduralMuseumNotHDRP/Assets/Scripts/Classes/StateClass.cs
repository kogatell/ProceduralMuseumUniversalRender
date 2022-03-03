using System.Collections;


namespace Assets.Scripts
{
    public abstract class StateClass
    {
        public virtual IEnumerator Start()
        {
            yield break;
        }
        public virtual IEnumerator MoveNormal()
        {
            yield break;
        }
        public virtual IEnumerator MoveHead()
        {
            yield break;
        }
        public virtual IEnumerator ComebackHeadToNormal()
        {
            yield break;
        }
    }
}
