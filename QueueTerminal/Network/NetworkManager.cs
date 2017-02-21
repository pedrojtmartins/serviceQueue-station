using QueueTerminal.Interfaces;
using QueueTerminal.Models;
using QueuServer.Managers;
using QueuTerminal.Models.Terminal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueuServer
{
    class ServerManager
    {
        string ip;

        IServerUpdateListener listener;

        TcpClient tcpClient;
        NetworkStream stream;
        Thread thread;

        public ServerManager(IServerUpdateListener listener, string ip)
        {
            this.listener = listener;
            this.ip = ip;
        }

        public bool Initialize()
        {
            tcpClient = new TcpClient();
            try { tcpClient.Connect(ip, Constants.IP_PORT); }
            catch (Exception e)
            {
                return false;
            }

            stream = tcpClient.GetStream();

            SendIdentification();

            thread = new Thread(() => ListenToServer(stream));
            thread.Start();
            return true;
        }

        private void SendIdentification()
        {
            // We will identify ourselves to the server so it can send us
            // only the information we need in the future.

            var id = new ConnectionIdentification();
            var json = SerializationManager<ConnectionIdentification>.Serialize(id);
            var task = SendDataToServerAsync(json);
        }

        private void ListenToServer(NetworkStream stream)
        {
            if (stream == null)
                return;

            while (true)
            {
                var buffer = new byte[Constants.bufferSize];

                try
                {
                    int size = stream.Read(buffer, 0, buffer.Length);
                    if (size > 0)
                        ComputeServerRequest(buffer, size);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }

        internal void CloseConnection()
        {
            if (stream != null)
                stream.Close();

            if (thread != null && thread.IsAlive)
                thread.Abort();
        }

        private void ComputeServerRequest(byte[] buffer, int size)
        {
            var bArray = new byte[size];
            Array.Copy(buffer, bArray, size);

            listener.NewListReceived(SerializationManager<ServerUpdate>.Desserialize(bArray));
        }

        public async Task SendDataToServerAsync(string data)
        {
            if (stream == null || !stream.CanWrite || data == null || data.Length == 0)
                return;

            var buffer = Encoding.ASCII.GetBytes(data);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
