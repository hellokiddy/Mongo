using SimpleJson;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Mango.Common.Net
{
    /// <summary>
    /// network state enum
    /// </summary>
    public enum NetWorkState
    {
        [Description("initial state")]
        CLOSED,

        [Description("connecting server")]
        CONNECTING,

        [Description("server connected")]
        CONNECTED,

        [Description("disconnected with server")]
        DISCONNECTED,

        [Description("connect timeout")]
        TIMEOUT,

        [Description("netwrok error")]
        ERROR
    }

    public class MangoClient : IDisposable
    {
        /// <summary>
        /// netwrok changed event
        /// </summary>
        public event Action<NetWorkState> NetWorkStateChangedEvent;

		//current network state
        private NetWorkState netWorkState = NetWorkState.CLOSED;   

		private Socket mSocket;
        private Protocol protocol;
        private bool disposed = false;
        private uint reqId = 1;

        private ManualResetEvent timeoutEvent = new ManualResetEvent(false);
        private int timeoutMSec = 8000;    //connect timeout count in millisecond

        public MangoClient()
        {
        }

        /// <summary>
        /// initialize mango client
        /// </summary>
        /// <param name="host">server name or server ip (www.xxx.com/127.0.0.1/::1/localhost etc.)</param>
        /// <param name="port">server port</param>
        public void InitClient(string host, int port)
        {
            timeoutEvent.Reset();
            NetWorkChanged(NetWorkState.CONNECTING);

            IPAddress ipAddress = null;

            try
            {
                IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
                foreach (var item in addresses)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = item;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                NetWorkChanged(NetWorkState.ERROR);
                return;
            }

            if (ipAddress == null)
            {
                throw new Exception("can not parse host : " + host);
            }

            this.mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ie = new IPEndPoint(ipAddress, port);

            mSocket.BeginConnect(ie, new AsyncCallback((result) =>
            {
                try
                {
                    this.mSocket.EndConnect(result);
                    this.protocol = new Protocol(this, this.mSocket);
                    NetWorkChanged(NetWorkState.CONNECTED);
                }
                catch (SocketException e)
                {
                    if (netWorkState != NetWorkState.TIMEOUT)
                    {
                        NetWorkChanged(NetWorkState.ERROR);
                    }
                    Dispose();
                }
                finally
                {
                    timeoutEvent.Set();
                }
            }), this.mSocket);

            if (timeoutEvent.WaitOne(timeoutMSec, false))
            {
                if (netWorkState != NetWorkState.CONNECTED && netWorkState != NetWorkState.ERROR)
                {
                    NetWorkChanged(NetWorkState.TIMEOUT);
                    Dispose();
                }
            }
        }

        /// <summary>
        /// 网络状态变化
        /// </summary>
        /// <param name="state"></param>
        private void NetWorkChanged(NetWorkState state)
        {
            netWorkState = state;

            if (NetWorkStateChangedEvent != null)
            {
                NetWorkStateChangedEvent(state);
            }
        }
			

        public void connect(Action<JsonObject> handshakeCallback)
        {
            connect(null, handshakeCallback);
        }

        public bool connect(JsonObject user, Action<JsonObject> handshakeCallback)
        {
            try
            {
                protocol.start(user, handshakeCallback);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private JsonObject emptyMsg = new JsonObject();
        public void request(string route, Action<JsonObject> action)
        {
            this.request(route, emptyMsg, action);
        }

        public void request(string route, JsonObject msg, Action<JsonObject> action)
        {           
            protocol.send(route, reqId, msg);
            reqId++;
        }
			

        internal void processMessage(Message msg)
        {
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "resp";
                //eventManager.InvokeCallBack(msg.id, msg.data);
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "push";
                //eventManager.InvokeOnEvent(msg.route, msg.data);
            }
        }

        public void Disconnect()
        {
            Dispose();
            NetWorkChanged(NetWorkState.DISCONNECTED);
        }

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //clean-up
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                // free managed resources
                if (this.protocol != null)
                {
                    this.protocol.close();
                }
					
                try
                {
                    this.mSocket.Shutdown(SocketShutdown.Both);
                    this.mSocket.Close();
                    this.mSocket = null;
                }
                catch (Exception)
                {
                    //todo : 有待确定这里是否会出现异常，这里是参考之前官方github上pull request。emptyMsg
                }
                this.disposed = true;
            }
        }
    }
}