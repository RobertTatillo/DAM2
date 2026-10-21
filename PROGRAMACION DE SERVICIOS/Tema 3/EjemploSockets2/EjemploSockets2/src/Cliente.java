
import java.io.*;
import java.net.*;
class Cliente {
	static final String HOST = "localhost";
	static final int PUERTO=5000;
public Cliente( ) {
	try{
		Socket skCliente = new Socket( HOST , PUERTO );
		InputStream aux = skCliente.getInputStream();
		DataInputStream flujo = new DataInputStream( aux );
		OutputStream salida = skCliente.getOutputStream();
		DataOutputStream flujoSalida= new DataOutputStream( salida );
		System.out.println( flujo.readUTF() );
		flujoSalida.writeInt(5);
		flujo.close();
		flujoSalida.close();
		skCliente.close();
	} catch( Exception e ) {
		System.out.println( e.getMessage() );
	}
}

	public static void main( String[] arg ) {
		new Cliente();
	}
}