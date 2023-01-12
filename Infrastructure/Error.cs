namespace WebAPIProject.Infrastructre
{
    public class Error
    {
        public Error(string ErrorMessage )
        {
            ErrorLogging(ErrorMessage);
        }
        public Error(Exception e)
        {
            ErrorLogging(e);
        }
        public static void ErrorLogging(string ErrorMessage)
        {
            string path = @"E:\ErrorLog\ErrorLog.txt";
            if(!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            using(StreamWriter sw =  File.AppendText(path))
            {
                sw.WriteLine("======================= Error Start ===================");
                sw.WriteLine(ErrorMessage);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("======================= Error End ===================");
            }
        }
        public static void ErrorLogging(Exception ErrorMessage)
        {
            string path = @"E:\ErrorLog\ErrorLog.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("======================= Error Start ===================");
                sw.WriteLine(ErrorMessage.Message);
                sw.WriteLine(ErrorMessage.StackTrace);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("======================= Error End ===================");
            }
        }
    }
}
