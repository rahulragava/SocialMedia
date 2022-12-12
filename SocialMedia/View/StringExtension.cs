
namespace SocialMedia.View
{
    public static class StringExtension
    {
        public static void Print(this string message)
        {
            Console.Write(message);
        }

        public static void PrintLine(this string message)
        {
            Console.WriteLine(message);
        }
    }
}
