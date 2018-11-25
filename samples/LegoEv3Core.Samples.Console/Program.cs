using System.Threading.Tasks;
using LegoEv3Core.Communication;

namespace LegoEv3Core.Samples.Console
{
    public class Program
    {
        Brick _brick;

        public static async Task Main(string[] args)
        {
            var program = new Program();
            await program.Test();

            System.Console.ReadLine();
        }

        public async Task Test()
        {
            _brick = new Brick(new UsbCommunication(), true);
            //_brick = new Brick(new BluetoothCommunication("COM5"));
            //_brick = new Brick(new NetworkCommunication("192.168.2.237"));

            _brick.BrickChanged += _brick_BrickChanged;

            System.Console.WriteLine("Connecting...");
            await _brick.ConnectAsync();

            System.Console.WriteLine("Connected...Turning motor...");
            await _brick.DirectCommand.TurnMotorAtSpeedForTimeAsync(OutputPort.A, 0x50, 1000, false);

            System.Console.WriteLine("Motor turned...beeping...");
            await _brick.DirectCommand.PlayToneAsync(0x50, 5000, 3000);

            System.Console.WriteLine("Beeped...done!");
        }

        private static void _brick_BrickChanged(object sender, BrickChangedEventArgs e)
        {
            System.Console.WriteLine(e.Ports[InputPort.One].SIValue);
        }
    }
}
