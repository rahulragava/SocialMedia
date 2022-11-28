using SocialMedia;
using SocialMedia.Controller;
using SocialMedia.Manager;
using System;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new InitializeData().Initialize();
            new ApplicationController().StartApplication();
        }
    }
}