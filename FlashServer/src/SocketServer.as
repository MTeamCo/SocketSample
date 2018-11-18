package
{
	import contents.alert.Alert;
	
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import flash.events.ProgressEvent;
	import flash.events.ServerSocketConnectEvent;
	import flash.net.ServerSocket;
	import flash.net.Socket;
	
	public class SocketServer extends Sprite
	{
		private var socketS:ServerSocket ;

		private var socket:Socket;
		
		public function SocketServer()
		{
			super();
			Alert.setScreenDebugger(stage);
			Alert.show("Click to create server");
			
			stage.addEventListener(MouseEvent.CLICK,createServer);
		}
		
		protected function createServer(event:MouseEvent):void
		{
			Alert.show("Try to bind server");
			socketS = new ServerSocket();
			socketS.bind(10500);
			socketS.addEventListener(ServerSocketConnectEvent.CONNECT, socketConnected);
			socketS.listen();
		}
		
		protected function socketConnected(event:ServerSocketConnectEvent):void
		{
			Alert.show("Server binded");
			
			socket = event.socket;
			socket.addEventListener(ProgressEvent.SOCKET_DATA, socketDataHandler);
		}
		
		private function socketDataHandler(e:ProgressEvent):void {
			var data:String = socket.readUTFBytes(socket.bytesAvailable);
			Alert.show("Data received:", data);
			socket.writeUTFBytes("I got you!! : "+data);
			socket.flush();
		}
	}
}