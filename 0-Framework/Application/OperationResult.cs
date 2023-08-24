namespace _0_Framework.Application
{
    public class  OperationResult
    {
        public static  bool IsSuccedded { get; set; }
        public static string Message { get; set; }


        public OperationResult()
        {
            IsSuccedded = false;

        }
        
        public bool  Succedded(string message)
        {
            IsSuccedded = true;
            Message = message;
            return IsSuccedded;

        }

        public bool   Failed(string message)
        {
            Message = message;
            IsSuccedded = false;
            return IsSuccedded ;
            
        }
        
    }
}
