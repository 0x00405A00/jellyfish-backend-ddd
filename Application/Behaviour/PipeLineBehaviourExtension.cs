namespace Application.Behaviour
{
    public static class PipeLineBehaviourExtension
    {

        public static bool IsNoCommand<TRequest>(TRequest request)
        {
            return !request.GetType().Name.EndsWith("Command");
        }
    }

}
