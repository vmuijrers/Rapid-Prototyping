namespace Example
{
    public class AntState : State
    {
        protected AntUnit owner;
        public override void Setup()
        {
            owner = GetComponent<AntUnit>();
        }
    }

}


