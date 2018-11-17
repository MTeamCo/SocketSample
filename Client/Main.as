package
{
	import contents.alert.Alert;
	
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.events.ProgressEvent;
	import flash.net.Socket;
	
	public class Main extends MovieClip
	{
		private var socketListener:Socket ;
		
		const serverIp:String = "185.83.208.175" ;
		
		const port:uint = 8412 ;
		
		public function Main()
		{
			super();
			
			Alert.setScreenDebugger(stage);
			
			Alert.show("Click stage");
			stage.addEventListener(MouseEvent.CLICK,connectSocket);
		}
		
		protected function connectSocket(event:MouseEvent):void
		{
			stage.removeEventListener(MouseEvent.CLICK,connectSocket);
			
			socketListener = new Socket();
			
			socketListener.addEventListener(ProgressEvent.SOCKET_DATA,socketDataRecevied);
			socketListener.addEventListener(Event.CONNECT,socketConnected);
			socketListener.addEventListener(IOErrorEvent.IO_ERROR,noConnectionAvailable);
			
			reconect();
		}
		
		private function reconect(e=null):void
		{
			stage.removeEventListener(MouseEvent.CLICK,reconect);
			Alert.show("Try to connect again");
			socketListener.connect(serverIp,port);
		}
		
		protected function noConnectionAvailable(event:IOErrorEvent):void
		{
			Alert.show("Connection faild. click to reconnect");
			stage.removeEventListener(MouseEvent.CLICK,sendSampleData);
			stage.addEventListener(MouseEvent.CLICK,reconect);
		}
		
		protected function socketConnected(event:Event):void
		{
			Alert.show("Socket connected");	
			
			stage.addEventListener(MouseEvent.CLICK,sendSampleData);
		}
		
		protected function sendSampleData(event:MouseEvent):void
		{
			var samplePacket:String = "Hello from kheshti "+Math.random() ;
			Alert.show("This data sent : "+samplePacket);
			socketListener.writeUTFBytes(samplePacket);
			socketListener.flush();
		}
		
		protected function socketDataRecevied(event:ProgressEvent):void
		{
			var recevedData:String = '' ;
			if(socketListener.bytesAvailable>0)
			{							
				recevedData = socketListener.readUTFBytes(socketListener.bytesAvailable);
			}
			Alert.show("Receved data is : "+recevedData);			
		}
	}
}