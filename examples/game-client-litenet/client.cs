using LiteNetLib;

class Client
{
    static void Main(string[] args)
    {
        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager client = new NetManager(listener);
        Console.WriteLine("Starting LiteNetLib Client");
        client.Start();
        client.Connect("localhost" /* host ip or name */, 8888 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            Console.WriteLine("We got: {0}", dataReader.GetString(100 /* max length of string */));
            dataReader.Recycle();
        };

        while (!Console.KeyAvailable)
        {
            client.PollEvents();
            Thread.Sleep(15);
        }

        Console.WriteLine("Stopping LiteNetLib Client");
        client.Stop();
    }
}