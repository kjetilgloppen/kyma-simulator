using System.IO.Ports;

namespace KymaSimulator
{
    internal class Simulator
    {
        private readonly SerialPort _serialPort;
        private readonly DataLoggerSimulator _dataLoggerSimulator;
        private int _counterFlag;

        public Simulator()
        {
            _dataLoggerSimulator = new DataLoggerSimulator();
            _serialPort = new SerialPort {PortName = "COM4"};
            _serialPort.DataReceived += DataReceived;
        }

        public void Start()
        {
            _dataLoggerSimulator.Start();
            _serialPort.Open();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Accept ST00I now, potentially SD00I, S80I/S90I
            var data = _serialPort.ReadLine();
            if (data.Length < 4) return;
            if (data[0] != 'S') return;
            var command = data[1];
            switch (command)
            {
                case 'T':
                    HandleCounterRequest(data);
                    break;
                default:
                    Log.Debug($"Invalid command received: {data}");
                    break;
            }
        }

        private void HandleCounterRequest(string request)
        {
            if (request.Length < 5) return;
            var requestId = request[2];

            var reply = $"STA{requestId},{_counterFlag},{_dataLoggerSimulator.GetCounterString()}";
            _serialPort.WriteLine(reply);
            // This is a flag that is 0 first time counter values are requested, and from then on 1 (or was it the other way around...)
            _counterFlag = 1;
        }

        public string GetCounterValues()
        {
            return _dataLoggerSimulator.GetCounterString();
        }
    }
}