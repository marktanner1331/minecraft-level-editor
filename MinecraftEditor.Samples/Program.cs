namespace MinecraftEditor.Samples
{
    static class Program
    {
        static void Main()
        {
            string source = @"C:\Users\Home\AppData\Roaming\.minecraft\saves\New World2023-11-09 12-03-22";
            string dest = @"C:\Users\Home\AppData\Roaming\.minecraft\saves\New World2023-11-09 12-16-33";

            PlayerPositionAndRotation.GetPlayerPositionAndRotation(source, out var position, out var rotation);
            Console.WriteLine(position.ToString());
            Console.WriteLine(rotation.ToString());

            PlayerPositionAndRotation.SetPlayerPositionAndRotation(dest, position, rotation);
        }
    }
}