using SocialMedia.Controller;
using SocialMedia.Manager;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new InitializeData().Initialize();
            ApplicationController.Instance.StartApplication();
        }
    }
}